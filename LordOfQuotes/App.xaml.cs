﻿using System;
using LordOfQuotes.Services;
using LordOfQuotes.Services.DataServices;
using LordOfQuotes.ViewModels;
using LordOfQuotes.Views;
using Microsoft.Extensions.DependencyInjection;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LordOfQuotes
{
    public partial class App : Application
    {
        public static ServiceProvider ServiceProvider { get; set; }

        public App()
        {
            InitializeComponent();
            SetupServices();

            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }

        private void SetupServices()
        {
            // API KEYS HERE
            Xamarin.Essentials.SecureStorage.SetAsync("LORAPI", "ArKxecgybWqdt767qKpB");

            var services = new ServiceCollection();

            // VIEW MODELS
            services.AddTransient<BaseViewModel>();
            services.AddTransient<DashboardViewModel>();

            // SERVICES

            // SINGLETON SERVICES
            services.AddSingleton<IHttpHandlerService>();
            services.AddSingleton<IRequestService, RequestService>();
            services.AddSingleton<IHttpService, HttpService>();

            ServiceProvider = services.BuildServiceProvider();
        }
    }
}