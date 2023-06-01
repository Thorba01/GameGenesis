using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace GameGenesisApp.ViewModels
{
	public class ProductDetailsViewModel : BaseViewModel
	{
		private int productId;
		public ProductDetailsViewModel ()
		{
			
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

		}

    }
}