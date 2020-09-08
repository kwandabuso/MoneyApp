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

        private void addSavings_Clicked(object sender, EventArgs e)
        {
            var fkey = getForeighKey();
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

        public string getForeighKey()
        {
            List<addSalary> intList = new List<addSalary>();
            var Fkey = "";

            using (SQLiteConnection conn = new SQLiteConnection(App.filePath))
            {
                var foreign = conn.Query<addSalary>("SELECT id FROM Money WHERE isActive = true");

                //conn.Execute("UPDATE Money SET isActive = false WHERE id =1");

                //var stocksStartingWithA = conn.Query<addSalary>("SELECT * FROM Money WHERE isActive = true");

                //stocksStartingWithA = conn.Query<addSalary>("SELECT * FROM Money WHERE isActive = false");

                foreach (var fK in foreign)
                {

                    Fkey = fK.id.ToString();

                }

            }

            return Fkey;
        }

        public string getSalary()
        {
            List<addSalary> intList = new List<addSalary>();
            var Fkey = "";

            using (SQLiteConnection conn = new SQLiteConnection(App.filePath))
            {

                //var apple = from s in conn.Table<addSalary>()
                //            where s.isActive.Equals("true")
                //            select s;


                


                conn.CreateTable<addSalary>();
                var salarie = conn.Table<addSalary>().ToList() ;




                MyListView.ItemsSource = salarie;


                var foreign = conn.Query<addSalary>("SELECT mySalary FROM Money WHERE isActive = true");

                foreach (var fK in foreign)
                {

                    Fkey = fK.id.ToString();

                }

            }

            return Fkey;
        }
    }
}