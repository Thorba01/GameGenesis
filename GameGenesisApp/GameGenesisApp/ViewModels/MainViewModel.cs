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
using System.Timers;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Input;
using Xamarin.Forms;

namespace GameGenesisApp.ViewModels
{
    //[QueryProperty(nameof(ShopId), nameof(ShopId))]
    public class MainViewModel : BaseViewModel
	{
        ApiServices _apiServices = new ApiServices();
        private Product _selectedItem;
        public Command GoToLibrary { get; }
        public Command GoToBasket { get; }
        public Command GoToShop { get; }
        public Command<Product> ProductTapped { get; }

        //Properties for the displayFunction
        private ObservableCollection<Product> _displayedProducts;
        private ObservableCollection<Product> _allProducts;
        private int _currentIndex;
        private int _displayItemCount = 2;
        private Timer _productTimer;
        private static Random rng = new Random();

        public ObservableCollection<Product> DisplayedProducts
        {
            get { return _displayedProducts; }
            set { SetProperty(ref _displayedProducts, value); }
        }

        public MainViewModel ()
		{
            ProductTapped = new Command<Product>(OnItemSelected);

            GoToLibrary = new Command(OnGoToLibrary);
            GoToBasket = new Command(OnGoToBasket);
            GoToShop = new Command(OnGoToShop);

            _allProducts = new ObservableCollection<Product>();
            Title = "Shop";
            _displayedProducts = new ObservableCollection<Product>();
            ExecuteLoadProductsCommand();
        }

        //Logic for the displayFunction
        private async Task ExecuteLoadProductsCommand()
        {
            IsBusy = true;
            try
            {
                var root = await _apiServices.GetShopAsync();

                Device.BeginInvokeOnMainThread(() =>
                {
                    _allProducts.Clear();
                    foreach (Product prod in root.products)
                    {
                        _allProducts.Add(prod);
                    }

                    Shuffle(_allProducts);

                    if (_allProducts.Count < _displayItemCount)
                    {
                        _displayItemCount = _allProducts.Count;
                    }

                    _currentIndex = 0;
                    LoadProductsToDisplay();
                });
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }

            if (_productTimer != null)
            {
                _productTimer.Stop();
                _productTimer.Elapsed -= TimerElapsed;
            }

            _productTimer = new Timer(5000);
            _productTimer.Elapsed += TimerElapsed;
            _productTimer.Start();
        }

        private void TimerElapsed(object sender, ElapsedEventArgs e)
        {
            MoveNext();
        }

        public ICommand NextCommand => new Command(() =>
        {
            MoveNext();
            _productTimer.Stop();
            _productTimer.Start();
        });

        public ICommand PreviousCommand => new Command(() =>
        {
            MovePrevious();
            _productTimer.Stop();
            _productTimer.Start();
        });

        private void LoadProductsToDisplay()
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                DisplayedProducts.Clear();

                for (int i = _currentIndex; i < _currentIndex + _displayItemCount; i++)
                {
                    var product = _allProducts[i % _allProducts.Count];
                    DisplayedProducts.Add(product);
                    Debug.WriteLine($"Displayed product: {product.name}"); // Debug line
                }
            });
        }

        private void MoveNext()
        {
            _currentIndex = (_currentIndex + _displayItemCount) % _allProducts.Count;
            LoadProductsToDisplay();
        }

        private void MovePrevious()
        {
            _currentIndex -= _displayItemCount;
            if (_currentIndex < 0)
            {
                _currentIndex += _allProducts.Count;
            }
            LoadProductsToDisplay();
        }

        public static void Shuffle<T>(IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
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

        public async void OnAppearing()
        {
            IsBusy = true;
            SelectedItem = null;
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