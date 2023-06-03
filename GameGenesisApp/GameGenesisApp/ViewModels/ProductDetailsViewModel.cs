using GameGenesisApp.Models;
using GameGenesisApp.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace GameGenesisApp.ViewModels
{
    [QueryProperty(nameof(ProductId), nameof(ProductId))]
    public class ProductDetailsViewModel : BaseViewModel
	{
		private int productId;
		RootProduct product;
        ApiServices _apiServices;
        public Command AddToBasketCommand { get; }
        public Command RemoveFromBasketCommand { get; }
        public ProductDetailsViewModel ()
		{
            AddToBasketCommand = new Command(AddToBasket);
            RemoveFromBasketCommand = new Command(RemoveFromBasket);
            _apiServices = new ApiServices();
        }

        async void IsProductInBasket()
        {
            var response = await _apiServices.GetProductFromBasket();
            if (response.Products.Count == 0)
            {
                IsAddButtonVisible = true;
                IsRemoveButtonVisible = false;
            }
            foreach (Product prod in response.Products)
            {
                if(prod.Id == productId)
                {
                    IsAddButtonVisible = false;
                    IsRemoveButtonVisible = true;
                    break;
                }
                else
                {
                    IsAddButtonVisible = true;
                    IsRemoveButtonVisible = false;
                }
            }
        }

        public int ProductId
		{
			get
			{
				return productId;
			}
			set
			{
				productId = value;
				LoadItemId();
                IsProductInBasket();
            }
        }

		public async Task LoadItemId()
		{
            try
            {
                ExecuteLoadPlayersCommand();
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Item");
            }
        }

		async Task ExecuteLoadPlayersCommand()
		{
            IsBusy = true;

            try
            {
                product = await _apiServices.GetProductById(productId);

                Name = product.Products.name;
                Description = product.Products.Description;
                Price = product.Products.Price;
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

        private string name;
        public string Name
        {
            get => name;
            set => SetProperty(ref name, value);
        }

        private string description;
        public string Description
        {
            get => description;
            set => SetProperty(ref description, value);
        }

        private float price;
        public float Price
        {
            get => price;
            set => SetProperty(ref price, value);
        }

        private bool _isAddButtonVisible;
        public bool IsAddButtonVisible
        {
            get => _isAddButtonVisible;
            set
            {
                _isAddButtonVisible = value;
                OnPropertyChanged();
            }
        }
        private bool _isRemoveButtonVisible;
        public bool IsRemoveButtonVisible
        {
            get => _isRemoveButtonVisible;
            set
            {
                _isRemoveButtonVisible = value;
                OnPropertyChanged();
            }
        }

        async void AddToBasket()
        {
            var basket = await _apiServices.GetBasket();
            var basketId = basket.Baskets[0].Id;
            await _apiServices.AddProductToBasket(ProductId, basketId);
            IsAddButtonVisible = false;
            IsRemoveButtonVisible = true;
        }

        async void RemoveFromBasket()
        {
            var basket = await _apiServices.GetBasket();
            var basketId = basket.Baskets[0].Id;
            await _apiServices.RemoveProductFromBasket(ProductId, basketId);
            IsAddButtonVisible = true;
            IsRemoveButtonVisible = false;
        }
    }
}