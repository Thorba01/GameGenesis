using GameGenesisApp.Models;
using GameGenesisApp.Services;
using GameGenesisApp.Views;
using Newtonsoft.Json;
using Stripe;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace GameGenesisApp.ViewModels
{
    [QueryProperty(nameof(Amount), nameof(Amount))]
    public class PayementViewModel : BaseViewModel
	{
        static string url = "https://localhost:7084/";
        private HttpClient client = new HttpClient();

        public ObservableCollection<Models.Product> Basket { get; }
        ApiServices _apiServices = new ApiServices();

        public PayementViewModel ()
		{
            StripeConfiguration.ApiKey = "pk_test_51NGhC2EJzwedTFPXrVzsYAy7buHx8yvykDVEAhis1DKJQ7p9Wwxe2UqSmrdvhNKK9rb3uRxSqhtPTTkfRM7YF3Kq00bRzKFjHC";
            SubmitPaymentCommand = new Command(async () => await SubmitPayment());
            client = new HttpClient();
            Basket = new ObservableCollection<Models.Product>();

        }

        public ICommand SubmitPaymentCommand { get; private set; }

        private string cardNumber;
        public string CardNumber
        {
            get { return cardNumber; }
            set
            {
                cardNumber = value;
                SetProperty(ref cardNumber, value);
            }
        }

        private string expiryDate;
        public string ExpiryDate
        {
            get { return expiryDate; }
            set
            {
                expiryDate = value;
                SetProperty(ref expiryDate, value);
            }
        }

        private string cvc;
        public string CVC
        {
            get { return cvc; }
            set
            {
                cvc = value;
                SetProperty(ref cvc, value);
            }
        }

        private long amount;
        public long Amount
        {
            get { return amount;}
            set
            {
                amount = value;
                SetProperty(ref amount, value);
            }
        }

        private async Task SubmitPayment()
        {
            string[] splitDate = ExpiryDate.Split('/');
            int expMonth = Convert.ToInt32(splitDate[0]);
            int expYear = Convert.ToInt32(splitDate[1]);
            
            await CreatePaymentMethod(CardNumber, expMonth, expYear, CVC);
        }

        public async Task CreatePaymentMethod(string cardNumber, long expMonth, long expYear, string cvc)
        {
            StripeConfiguration.ApiKey = "pk_test_51NGhC2EJzwedTFPXrVzsYAy7buHx8yvykDVEAhis1DKJQ7p9Wwxe2UqSmrdvhNKK9rb3uRxSqhtPTTkfRM7YF3Kq00bRzKFjHC";

            var options = new PaymentMethodCreateOptions
            {
                Type = "card",
                Card = new PaymentMethodCardOptions
                {
                    Number = cardNumber,
                    ExpMonth = expMonth,
                    ExpYear = expYear,
                    Cvc = cvc
                }
            };

            var service = new PaymentMethodService();
            PaymentMethod paymentMethod = await service.CreateAsync(options);

            await SendPaymentMethodToApi(paymentMethod.Id, Amount);
        }

        private async Task SendPaymentMethodToApi(string paymentMethodId, long amount)
        {
            var storedToken = await SecureStorage.GetAsync("jwt_token");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", storedToken);

            amount = amount * 100;

            var payement = new Payment
            {
                paymentMethodId = paymentMethodId,
                amount = amount
            };
            var content = new StringContent(JsonConvert.SerializeObject(payement), Encoding.UTF8, "application/json");

            var response = await client.PostAsync(url + "api/Payement", content);
            var responseContent = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                Basket.Clear();
                var root = await _apiServices.GetProductFromBasket();

                var basket = await _apiServices.GetBasket();
                var basketId = basket.Baskets[0].Id;

                var library = await _apiServices.GetLibrary();
                var libraryId = library.Libraries[0].Id;

                foreach (var product in root.Products)
                {
                    Basket.Add(product);

                    await _apiServices.AddProductToLibrary(product.Id, libraryId);
                    await _apiServices.RemoveProductFromBasket(product.Id, basketId);
                }
                await Shell.Current.GoToAsync(nameof(LibraryPage));
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Error", responseContent, "Ok");
            }
        }
    }
}