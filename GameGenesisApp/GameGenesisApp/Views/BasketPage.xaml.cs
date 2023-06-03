using GameGenesisApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GameGenesisApp.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class BasketPage : ContentPage
	{
        BasketViewModel _viewModel;

        public BasketPage ()
		{
			InitializeComponent ();
            BindingContext = _viewModel = new BasketViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }
    }
}