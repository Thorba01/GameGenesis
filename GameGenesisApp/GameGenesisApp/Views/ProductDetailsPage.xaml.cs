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
	public partial class ProductDetailsPage : ContentPage
	{
        ProductDetailsViewModel _viewModel;

        public ProductDetailsPage ()
		{
			InitializeComponent ();
            BindingContext = _viewModel = new ProductDetailsViewModel();
            this.BackgroundColor = Color.Black;
        }
        //protected override void OnAppearing()
        //{
        //    base.OnAppearing();
        //    _viewModel.OnAppearing();
        //}
    }
}