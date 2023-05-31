﻿using GameGenesisApp.ViewModels;
using GameGenesisApp.Views;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace GameGenesisApp
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(RegisterPage), typeof(RegisterPage));
            Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));
        }

    }
}
