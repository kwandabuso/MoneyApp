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

        public Budget()
        {
            InitializeComponent();
        }
        public int Total { get; set; }
        private void ButtonAddBudget_Clicked(object sender, EventArgs e)
        {
            var fkey = getForeighKey();
            BudgetCls add = new BudgetCls()
            {
                item = Item.Text,
                amount = Amount.Text,
                FK = int.Parse(fkey),
                addedAt = DateTime.Now.ToString(),
                updatedAt = DateTime.Now.ToString()
                
            };

            using (SQLiteConnection conn = new SQLiteConnection(App.filePath))
            {
                conn.CreateTable<BudgetCls>();
                int rows = conn.Insert(add);

                OnAppearing();
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

                foreach(var mysalary in salarie)
                {
                    var sala = mysalary.amount;
                    if (sala != null)
                    {
                        Total += int.Parse(mysalary.amount);
                    }
                    
                }

            }
            total.Text += Total.ToString();
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

                foreach(var fK in foreign)
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



    }
}