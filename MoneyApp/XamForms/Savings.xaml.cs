using MoneyApp.Classes;
using System;
using SQLite;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Collections.Generic;


namespace MoneyApp.XamForms
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Savings : ContentPage
    {
        public Savings()
        {
            InitializeComponent();
        }

        private async void addSavings_Clicked(object sender, EventArgs e)
        {
            try
            {
                var fkey = getForeighKeyAsync();
                BudgetCls add = new BudgetCls()
                {
                    //amount = getSalary(),
                    addedAt = DateTime.Now.ToString()

                };

                using (SQLiteConnection conn = new SQLiteConnection(App.filePath))
                {
                    conn.CreateTable<BudgetCls>();
                    int rows = conn.Insert(add);

                    OnAppearing();
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Alert", ex.ToString(), "OK");
            }

        }

        public async System.Threading.Tasks.Task<string> getForeighKeyAsync()
        {
            var Fkey = "";
            try
            {
                List<addSalary> intList = new List<addSalary>();
                
                using (SQLiteConnection conn = new SQLiteConnection(App.filePath))
                {
                    var foreign = conn.Query<addSalary>("SELECT id FROM Money WHERE isActive = true");

                    foreach (var fK in foreign)
                    {

                        Fkey = fK.id.ToString();

                    }

                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Alert", ex.ToString(), "OK");
            }
            

            return Fkey;
        }

        public async System.Threading.Tasks.Task<string> getSalaryAsync()
        {
            List<addSalary> intList = new List<addSalary>();
            var Fkey = "";
            try
            {

                using (SQLiteConnection conn = new SQLiteConnection(App.filePath))
                {

                    conn.CreateTable<addSalary>();
                    var salarie = conn.Table<addSalary>().ToList();

                    MyListView.ItemsSource = salarie;


                    var foreign = conn.Query<addSalary>("SELECT mySalary FROM Money WHERE isActive = true");

                    foreach (var fK in foreign)
                    {

                        Fkey = fK.id.ToString();

                    }

                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Alert", ex.ToString(), "OK");
            }

            return Fkey;
        }
    }
}