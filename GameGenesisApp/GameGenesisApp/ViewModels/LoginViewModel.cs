using GameGenesisApp.Services;
using GameGenesisApp.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace GameGenesisApp.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        ApiServices _apiServices = new ApiServices();
        public string Email { get; set; }
        public string Password { get; set; }
        public string Message { get; set; }
        public ICommand LoginCommand
        {
            get
            {
                return new Command(async () =>
                {
                    IsBusy = true;
                    var response = await _apiServices.LoginAsync(Email, Password);

                    if (!response.success)
                    {
                        if (response.message is null)
                        {
                            response.message = "The combination between user and password not found.";
                        }
                        await App.Current.MainPage.DisplayAlert("Error", response.message, "Ok");
                        IsBusy = false;

                    }
                    else
                    {
                        await Shell.Current.GoToAsync($"{nameof(MainPage)}");
                        IsBusy = false;

                    }
                });
            }
        }

        public ICommand RegisterCommand
        {
            get
            {
                return new Command(async () =>
                {
                    await Shell.Current.GoToAsync($"{nameof(RegisterPage)}");
                });
            }
        }
    }
}