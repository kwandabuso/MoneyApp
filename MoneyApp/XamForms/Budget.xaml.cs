using MoneyApp.Classes;
using System;
using SQLite;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Collections.Generic;

namespace MoneyApp.XamForms
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Budget : ContentPage
    {
        int ide;
        globals global;
        double totalBudget;
        public Budget()
        {
            InitializeComponent();
        }
        public int Total { get; set; }

        private async void ButtonAddBudget_Clicked(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(Item.Text) || String.IsNullOrEmpty(Amount.Text))
            {
                await DisplayAlert("Alert", "Please enter all fields? ", "OK");
            }
            else
            {

                var fkey = getForeighKey();
                BudgetCls add = new BudgetCls()
                {
                    item = Item.Text,
                    amount = double.Parse(Amount.Text),
                    addedAt = DateTime.Now.ToString(),
                    updatedAt = DateTime.Now.ToString()

                };

                using (SQLiteConnection conn = new SQLiteConnection(App.filePath))
                {
                    conn.CreateTable<BudgetCls>();
                    int rows = conn.Insert(add);
                    Item.Text = "";
                    Amount.Text = "";
                    OnAppearing();
                }
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            using (SQLiteConnection conn = new SQLiteConnection(App.filePath))
            {
                conn.CreateTable<BudgetCls>();
                var salarie = conn.Table<BudgetCls>().ToList();
                MyListView.ItemsSource = salarie;

                //foreach (var mysalary in salarie)
                //{
                //    var sala = mysalary.amount;
                //    if (sala != null)
                //    {
                //        //Total += int.Parse(mysalary.amount);
                //    }

                //}

            }

            total.Text = getBudgetTotal().ToString();
        }

        public string getForeighKey()
        {
            List<addSalary> intList = new List<addSalary>();
            var Fkey = "";

            using (SQLiteConnection conn = new SQLiteConnection(App.filePath))
            {
                var foreign = conn.Query<addSalary>("SELECT id FROM Money WHERE isActive = true");

                //conn.Execute("UPDATE Money SET isActive = false WHERE id =1");

                var stocksStartingWithA = conn.Query<addSalary>("SELECT * FROM Money WHERE isActive = true");

                //stocksStartingWithA = conn.Query<addSalary>("SELECT * FROM Money WHERE isActive = false");

                foreach (var fK in foreign)
                {

                    Fkey = fK.id.ToString();

                }

            }

            return Fkey;
            /*
            using (SQLiteConnection conn = new SQLiteConnection(App.filePath))
            {
                conn.CreateTable<BudgetCls>();
                var salarie = conn.Table<BudgetCls>().ToList();
                MyListView.ItemsSource = salarie;

                foreach (var mysalary in salarie)
                {
                    Total += int.Parse(mysalary.amount);
                }

            }
            total.Text += Total.ToString();*/
        }

        private async void ButtonEdit_Clicked(object sender, EventArgs e)
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
                        conn.CreateTable<BudgetCls>();
                        var updateMarks = conn.ExecuteScalar<BudgetCls>("UPDATE Budget Set item  = ? , amount = ? WHERE id = ?", Item.Text, Amount.Text, ide);


                        Item.Text = "";
                        Amount.Text = "";
                    }
                }

                OnAppearing();
            }
        }

        private async void ButtonDelete_Clicked(object sender, EventArgs e)
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

                        conn.CreateTable<BudgetCls>();
                        var updateMarks = conn.ExecuteScalar<BudgetCls>("DELETE FROM Budget WHERE id = ?", ide);

                        Item.Text = "";
                        Amount.Text = "";
                    }
                }
                OnAppearing();
            }
        }

        private void MyListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var obj = (BudgetCls)e.SelectedItem;
            ide = Convert.ToInt32(obj.id);

            Item.Text = obj.item;
            Amount.Text = obj.amount.ToString();
        }

        private double getBudgetTotal()
        {

            List<BudgetCls> intList = new List<BudgetCls>();
            var Fkey = 0.0;

            using (SQLiteConnection conn = new SQLiteConnection(App.filePath))
            {
                conn.CreateTable<BudgetCls>();

                var foreign = conn.Query<BudgetCls>("SELECT amount FROM Budget");

                //conn.Execute("UPDATE Money SET isActive = false WHERE id =1");



                foreach (var fK in foreign)
                {
                    if (!string.IsNullOrEmpty(fK.amount.ToString()))
                    {
                        Fkey += fK.amount;
                    }

                }

            }

            return Fkey;
        }

        private double UpdateAmountOnDelete()
        {

            var newAmount = double.Parse(Amount.Text);
            double updateAmount = 0.0;

            global = new globals();
            updateAmount = global.budgetMinusOnTotal(newAmount);

            return updateAmount;
        }

        private async void StartBudget_Clicked(object sender, EventArgs e)
        {
            var result =
                 await DisplayAlert("Confirmation",
                 "Are you sure?",
                 "OK", "Cancel");
            if (result == true)
            {
                using (SQLiteConnection conn = new SQLiteConnection(App.filePath))
                {
                    conn.CreateTable<ActiveMoney>();
                    var foreign = conn.Query<ActiveMoney>("SELECT mySalary FROM ActiveMoney");

                    
                    global = new globals();
                    totalBudget = global.calculateMinusOnTotal(getBudgetTotal());
                    var updateMarks = conn.ExecuteScalar<ActiveMoney>("UPDATE ActiveMoney Set mySalary  = ?", totalBudget);
                    
                }
            }
        }
    }
}
    
