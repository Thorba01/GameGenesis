using GameGenesisApp.Models;
using GameGenesisApp.Services;
using GameGenesisApp.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace GameGenesisApp.ViewModels
{
    //[QueryProperty(nameof(ShopId), nameof(ShopId))]
    public class MainViewModel : BaseViewModel
	{
        private Product _selectedItem;
        public ObservableCollection<Product> Shop { get; }
        ApiServices _apiServices = new ApiServices();
        public Command GoToLibrary { get; }
        public Command GoToBasket { get; }
        public Command<Product> ProductTapped { get; }

        public MainViewModel ()
		{
            ProductTapped = new Command<Product>(OnItemSelected);

            GoToLibrary = new Command(OnGoToLibrary);
            GoToBasket = new Command(OnGoToBasket);
            Shop = new ObservableCollection<Product>();
        }

        private async void OnGoToLibrary(object obj)
        {
            await Shell.Current.GoToAsync(nameof(LibraryPage));
        }

        private async void OnGoToBasket(object obj)
        {
            await Shell.Current.GoToAsync(nameof(BasketPage));
        }

        async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;

            try
            {
                Shop.Clear();
                var root = await _apiServices.GetShopAsync();
                foreach (var product in root.products)
                {
                    Shop.Add(product);
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
            Debug.WriteLine("ici ////////////////////////////////////////////");
            if (product == null)
                return;
            Debug.WriteLine("pas null ////////////////////////////////////////////");

            // This will push the ItemDetailPage onto the navigation stack

            await Shell.Current.GoToAsync($"{nameof(ProductDetailsPage)}?{nameof(ProductDetailsViewModel.ProductId)}={product.Id}");
        }
    }
}