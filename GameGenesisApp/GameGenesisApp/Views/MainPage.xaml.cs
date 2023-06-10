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
	public partial class MainPage : ContentPage
	{
		MainViewModel _viewModel;
		public MainPage ()
		{
			InitializeComponent ();
            BindingContext = _viewModel = new MainViewModel();
            this.BackgroundColor = Color.Black;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }
    }
}