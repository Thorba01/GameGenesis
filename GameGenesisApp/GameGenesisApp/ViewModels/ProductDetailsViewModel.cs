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
using System.Windows.Input;
using Xamarin.Forms;

namespace GameGenesisApp.ViewModels
{
    [QueryProperty(nameof(ProductId), nameof(ProductId))]
    public class ProductDetailsViewModel : BaseViewModel
	{
		private int productId;
		RootProduct product;
        ApiServices _apiServices;
        List<Category> categories;
        ObservableCollection<Models.Image> images;

        public Command GoToLibrary { get; }
        public Command GoToBasket { get; }
        public Command GoToShop { get; }
        public Command AddToBasketCommand { get; }
        public Command RemoveFromBasketCommand { get; }

        public ProductDetailsViewModel ()
		{
            AddToBasketCommand = new Command(AddToBasket);
            RemoveFromBasketCommand = new Command(RemoveFromBasket);
            _apiServices = new ApiServices();

            GoToLibrary = new Command(OnGoToLibrary);
            GoToBasket = new Command(OnGoToBasket);
            GoToShop = new Command(OnGoToShop);
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
            var libraryResponse = await _apiServices.GetProductFromLibrary();
            foreach(Product prod in libraryResponse.Products)
            {
                if( prod.Id == productId)
                {
                    IsAddButtonVisible = false;
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
                ExecuteLoadPlayersCommand();
            }
        }

		async Task ExecuteLoadPlayersCommand()
		{
            IsBusy = true;

            try
            {
                product = await _apiServices.GetProductById(productId);

                Name = product.Products.name;
                Title = Name;
                Description = product.Products.Description;
                Price = product.Products.Price;
                Categories = product.Products.Categories;
                Images = product.Products.Images;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsProductInBasket();
                IsBusy = false;
            }
        }

        public List<Category> Categories
        {
            get => categories;
            set => SetProperty(ref categories, value);
        }

        public ObservableCollection<Models.Image> Images
        {
            get => images;
            set => SetProperty(ref images, value);
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