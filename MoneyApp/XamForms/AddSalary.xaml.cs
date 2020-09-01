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
        int ide;

        public AddSalary()
        {
            InitializeComponent();
        }

        private async void ButtonSalary_Clicked(object sender, EventArgs e)
        {
            var result =
          await DisplayAlert("Confirmation",
          "Are you sure? ",
          "OK", "Cancel");
            if (result == true)
            {
                addSalary add = new addSalary()
                {

                    mySalary = Salary.Text,
                    mySource = source.Text,
                    date = DateTime.Now.ToString()
                };

                using (SQLiteConnection conn = new SQLiteConnection(App.filePath))
                {
                    conn.CreateTable<addSalary>();
                    int rows = conn.Insert(add);
                }
                OnAppearing();
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

        private void MyListView_Focused(object sender, FocusEventArgs e)
        {
           
        }

        private void StackLayout_Focused(object sender, FocusEventArgs e)
        {
            addSalary add = new addSalary()
            {

                mySalary = Salary.Text,
                mySource = source.Text,
                date = DateTime.Now.ToString()
            };
        }

        private void EvetClicked(object sender, SelectedItemChangedEventArgs e)
        {
            var obj = (addSalary)e.SelectedItem;
            ide = Convert.ToInt32(obj.id);

            Salary.Text = obj.mySalary;
            source.Text = obj.mySource;
        }

        private async void ButtonEdit_Clicked(object sender, EventArgs e)
        {
            var result =
          await DisplayAlert("Confirmation",
          "Are you sure? ",
          "OK", "Cancel");
            if (result == true)
            {
                using (SQLiteConnection conn = new SQLiteConnection(App.filePath))
                {
                    conn.CreateTable<addSalary>();
                    var updateMarks = conn.ExecuteScalar<addSalary>("UPDATE Money Set mySalary  = ? , mySource = ? WHERE id = ?", Salary.Text, source.Text, ide);
                }
            }
            OnAppearing();
        }

        private async void ButtonDelete_Clicked(object sender, EventArgs e)
        {
            var result =
          await DisplayAlert("Confirmation",
          "Are you sure?",
          "OK", "Cancel");
            if (result == true)
            {
                using (SQLiteConnection conn = new SQLiteConnection(App.filePath))
                {
                    conn.CreateTable<addSalary>();
                    var updateMarks = conn.ExecuteScalar<addSalary>("DELETE FROM Money WHERE id = ?", ide);
                }
            }
            OnAppearing();
        }
    }
}