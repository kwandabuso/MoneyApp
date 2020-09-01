using MoneyApp.Classes;
using System;
using SQLite;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MoneyApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddSalary : ContentPage
    {
        public AddSalary()
        {
            InitializeComponent();
        }

        private void ButtonSalary_Clicked(object sender, EventArgs e)
        {
            addSalary add = new addSalary()
            {

                mySalary = Salary.Text,
                mySource = source.Text,
                isActive = true

            };

            using(SQLiteConnection conn = new SQLiteConnection(App.filePath))
            {
                conn.CreateTable<addSalary>();
                int rows = conn.Insert(add);
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            using (SQLiteConnection conn = new SQLiteConnection(App.filePath))
            {
                conn.CreateTable<addSalary>();
                var salarie = conn.Table<addSalary>().ToList();
                MyListView.ItemsSource = salarie;

            }
        }
    }
}