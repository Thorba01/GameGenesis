using GameGenesisApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Stripe;

namespace GameGenesisApp.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PayementPage : ContentPage
	{
        PayementViewModel _viewModel;
        public PayementPage ()
		{
			InitializeComponent ();
			BindingContext = _viewModel = new PayementViewModel();
        }
		
    }
}