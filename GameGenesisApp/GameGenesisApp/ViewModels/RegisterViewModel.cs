using GameGenesisApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace GameGenesisApp.ViewModels
{
    public class RegisterViewModel : BaseViewModel
    {
        ApiServices _apiServices = new ApiServices();
        public string Email { get; set; }
        public string Password { get; set; }
        public string PasswordTwo { get; set; }
        public string Message { get; set; }
        public DateTime BirthDate { get; set; }

        public ICommand RegisterCommand
        {
            get
            {
                return new Command(async () =>
                {
                    var isSuccess = await _apiServices.RegisterAsync(Email, Password, PasswordTwo, BirthDate);

                    if (isSuccess.success)
                    {
                        await App.Current.MainPage.DisplayAlert("Success !", isSuccess.message, "ok");
                        await Shell.Current.GoToAsync("..");
                    }
                    else
                    {
                        await App.Current.MainPage.DisplayAlert("Error !", isSuccess.message, "ok");
                    }
                });
            }
        }
    }
}