using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MoneyApp
{
    public partial class App : Application
    {
        public static string filePath;
        public App()
        {
            InitializeComponent();

            MainPage = new MainPage();
        }

        public App(string filepath)
        {
            InitializeComponent();

            MainPage = new MainPage();
            filePath = filepath;
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
    }
}
