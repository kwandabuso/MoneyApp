using MoneyApp.XamForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MoneyApp
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void Add_Clicked(object sender, EventArgs e)
        {
            var newPage = new AddSalary();
            await Navigation.PushModalAsync(newPage);
        }

        private async void budget_Clicked(object sender, EventArgs e)
        {
            var newPage = new Budget();
            await Navigation.PushModalAsync(newPage);
        }

        private async void spend_Clicked(object sender, EventArgs e)
        {
            var newPage = new Spend();
            await Navigation.PushModalAsync(newPage);
        }

        private async void savings_Clicked(object sender, EventArgs e)
        {
            var newPage = new Savings();
            await Navigation.PushModalAsync(newPage);
        }
    }
}
