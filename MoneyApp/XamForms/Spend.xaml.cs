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
        public Spend()
        {
            InitializeComponent();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            var fkey = getForeighKey();
            spendMoney add = new spendMoney()
            {
                item = Item.Text,
                amount = Amount.Text,
                FK = int.Parse(fkey),
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
        public string getForeighKey()
        {
            List<addSalary> intList = new List<addSalary>();
            var Fkey = "";

            using (SQLiteConnection conn = new SQLiteConnection(App.filePath))
            {
                var foreign = conn.Query<addSalary>("SELECT id FROM Money WHERE isActive = true");

                //conn.Execute("UPDATE Money SET isActive = false WHERE id =1");

               // var stocksStartingWithA = conn.Query<addSalary>("SELECT * FROM Money WHERE isActive = true");

                //stocksStartingWithA = conn.Query<addSalary>("SELECT * FROM Money WHERE isActive = false");

                foreach (var fK in foreign)
                {

                    Fkey = fK.id.ToString();

                }

            }

            return Fkey;

        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            using (SQLiteConnection conn = new SQLiteConnection(App.filePath))
            {
                conn.CreateTable<spendMoney>();
                var salarie = conn.Table<spendMoney>().ToList();
                MyListView.ItemsSource = salarie;

            }
        }

    }
}