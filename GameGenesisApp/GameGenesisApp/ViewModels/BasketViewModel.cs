using GameGenesisApp.Models;
using GameGenesisApp.Services;
using GameGenesisApp.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace GameGenesisApp.ViewModels
{
	public class BasketViewModel : BaseViewModel
	{
        private Product _selectedItem;
        public ObservableCollection<Product> Basket { get; }
        ApiServices _apiServices = new ApiServices();
        public Command GoToLibrary { get; }
        public Command GoToBasket { get; }
        public Command GoToShop { get; }

        public Command<Product> ProductTapped { get; }

        public BasketViewModel()
        {
            ProductTapped = new Command<Product>(OnItemSelected);

            GoToLibrary = new Command(OnGoToLibrary);
            GoToBasket = new Command(OnGoToBasket);
            GoToShop = new Command(OnGoToShop);

            Basket = new ObservableCollection<Product>();
        }

        private async void OnGoToLibrary(object obj)
        {
            await Shell.Current.GoToAsync(nameof(LibraryPage));
        }

        private async void OnGoToBasket(object obj)
        {
            await Shell.Current.GoToAsync(nameof(BasketPage));
        }

        private async void OnGoToShop(object obj)
        {
            await Shell.Current.GoToAsync(nameof(MainPage));
        }

        async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;

            try
            {
                Basket.Clear();
                var root = await _apiServices.GetProductFromBasket();
                foreach (var product in root.Products)
                {
                    Basket.Add(product);
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        public async void OnAppearing()
        {
            IsBusy = true;
            SelectedItem = null;
            await ExecuteLoadItemsCommand();
        }

        public Product SelectedItem
        {
            get => _selectedItem;
            set
            {
                SetProperty(ref _selectedItem, value);
                OnItemSelected(value);
            }
        }

        async void OnItemSelected(Product product)
        {
            if (product == null)
                return;

            await Shell.Current.GoToAsync($"{nameof(ProductDetailsPage)}?{nameof(ProductDetailsViewModel.ProductId)}={product.Id}");
        }
    }
}