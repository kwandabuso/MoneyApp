using MoneyApp.Classes;
using System;
using SQLite;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Collections.Generic;


namespace MoneyApp.XamForms
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Spend : ContentPage
    {
        int ide;
        globals global;
        double total;
        public Spend()
        {
            InitializeComponent();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(Item.Text) || String.IsNullOrEmpty(Amount.Text))
            {
                await DisplayAlert("Alert", "Please enter all fields? ", "OK");
            }
            else
            {
                spendMoney add = new spendMoney()
                {
                    item = Item.Text,
                    amount = double.Parse(Amount.Text),
                    addedAt = DateTime.Now.ToString(),
                    updatedAt = DateTime.Now.ToString()

                };

                using (SQLiteConnection conn = new SQLiteConnection(App.filePath))
                {
                    conn.CreateTable<spendMoney>();
                    int rows = conn.Insert(add);
                    OnAppearing();
                }
            }
        }
      
        protected override void OnAppearing()
        {
            base.OnAppearing();

            using (SQLiteConnection conn = new SQLiteConnection(App.filePath))
            {
                global = new globals();
                conn.CreateTable<spendMoney>();
                var salarie = conn.Table<spendMoney>().ToList();
                MyListView.ItemsSource = salarie;

                TotalSpend.Text = global.getSavingsTotal().ToString();
            }
        }

        private async void Edit_Clicked(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(Item.Text) || String.IsNullOrEmpty(Amount.Text))
            {
                await DisplayAlert("Alert", "Please enter all fields? ", "OK");
            }
            else
            {
                var result =
                  await DisplayAlert("Confirmation",
                  "Are you sure? ",
                  "OK", "Cancel");
                if (result == true && !Item.Text.Equals("") && !Amount.Text.Equals(""))
                {
                    using (SQLiteConnection conn = new SQLiteConnection(App.filePath))
                    {
                        conn.CreateTable<spendMoney>();
                        var updateMarks = conn.ExecuteScalar<spendMoney>("UPDATE Spend Set item  = ? , amount = ? WHERE id = ?", Item.Text, Amount.Text, ide);

                        //UpdateAmount();

                        //total = 0;
                        //global = new globals();
                        //total = UpdateAmount();
                        //var updateMoney = conn.ExecuteScalar<ActiveMoney>("UPDATE ActiveMoney Set mySalary  = ?", total);
                        //Salary.Text = "";
                        //source.Text = "";
                    }
                }
                OnAppearing();
            }

        }

        private async void delete_Clicked(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(Item.Text) || String.IsNullOrEmpty(Amount.Text))
            {
                await DisplayAlert("Alert", "Please select record to delete? ", "OK");
            }
            else
            {
                var result =
                  await DisplayAlert("Confirmation",
                  "Are you sure?",
                  "OK", "Cancel");
                if (result == true && !Item.Text.Equals("") && !Amount.Text.Equals(""))
                {
                    using (SQLiteConnection conn = new SQLiteConnection(App.filePath))
                    {

                        conn.CreateTable<addSalary>();
                        var updateMarks = conn.ExecuteScalar<spendMoney>("DELETE FROM Spend WHERE id = ?", ide);

                        total = 0;
                        global = new globals();
                        total = UpdateAmountOnDelete();
                        var updateMoney = conn.ExecuteScalar<ActiveMoney>("UPDATE ActiveMoney Set mySalary  = ?", total);
                        Item.Text = "";
                        Amount.Text = "";
                    }
                }
                OnAppearing();
            }

        }

        private void EvetClicked(object sender, SelectedItemChangedEventArgs e)
        {
            var obj = (spendMoney)e.SelectedItem;
            ide = Convert.ToInt32(obj.id);
            Item.Text = obj.item;
            Amount.Text = obj.amount.ToString();
        }

        private double UpdateAmountOnDelete()
        {

           
            var updateAmount = 0.0;

            global = new globals();
            updateAmount = global.calculateMinusOnTotal(global.getSavingsTotalById(ide.ToString()));

            return updateAmount;
        }
    }
}