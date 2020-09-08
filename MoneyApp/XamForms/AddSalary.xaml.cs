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
        globals global;
        double total;
        double oldAmount;
        public AddSalary()
        {
            InitializeComponent();
        }

        private async void ButtonSalary_Clicked(object sender, EventArgs e)
        {
            
            if(String.IsNullOrEmpty(Salary.Text)|| String.IsNullOrEmpty(source.Text))
            {
                await DisplayAlert("Alert", "Please enter all fields? ", "OK");
            }
            else
            {
                var result =
                  await DisplayAlert("Confirmation",
                  "Are you sure? ",
                  "Yes", "Cancel");
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


                    //
                    if (result == true && !Salary.Text.Equals("") && !source.Text.Equals(""))
                    {
                        using (SQLiteConnection conn = new SQLiteConnection(App.filePath))
                        {
                            conn.CreateTable<ActiveMoney>();
                            var foreign = conn.Query<ActiveMoney>("SELECT mySalary FROM ActiveMoney");

                            if(foreign.Count == 0)
                            {
                                ActiveMoney addFunds = new ActiveMoney()
                                {
                                    mySalary = Salary.Text,
                                };

                               
                                conn.CreateTable<ActiveMoney>();
                                int rows = conn.Insert(addFunds);
                               
                            }
                            else
                            {
                                global = new globals();
                                total = global.calculateTotal(double.Parse(Salary.Text));
                                var updateMarks = conn.ExecuteScalar<ActiveMoney>("UPDATE ActiveMoney Set mySalary  = ?", total);
                            }
                            
                            
                        }

                        Salary.Text = "";
                        source.Text = "";

                    }

                   
                    OnAppearing();
                }
            }
            
                
        }

        protected override void OnAppearing()
        {
            
            base.OnAppearing();
            global = new globals();
            
            using (SQLiteConnection conn = new SQLiteConnection(App.filePath))
            {
                conn.CreateTable<addSalary>();
                
                var salarie = conn.Table<addSalary>().ToList();
                
                MyListView.ItemsSource = salarie;
                Total.Text = global.getTotal().ToString();
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
            oldAmount = double.Parse(obj.mySalary);
            Salary.Text = obj.mySalary;
            source.Text = obj.mySource;
        }

        private async void ButtonEdit_Clicked(object sender, EventArgs e)
        {
            if(String.IsNullOrEmpty(Salary.Text)|| String.IsNullOrEmpty(source.Text))
            {
                await DisplayAlert("Alert", "Please enter all fields? ", "OK");
            }
            else
            {
                var result =
                  await DisplayAlert("Confirmation",
                  "Are you sure? ",
                  "OK", "Cancel");
                if (result == true && !Salary.Text.Equals("") && !source.Text.Equals(""))
                {
                    using (SQLiteConnection conn = new SQLiteConnection(App.filePath))
                    {
                        conn.CreateTable<addSalary>();
                        var updateMarks = conn.ExecuteScalar<addSalary>("UPDATE Money Set mySalary  = ? , mySource = ? WHERE id = ?", Salary.Text, source.Text, ide);

                        UpdateAmount();

                        total = 0;
                        global = new globals();
                        total = UpdateAmount();
                        var updateMoney = conn.ExecuteScalar<ActiveMoney>("UPDATE ActiveMoney Set mySalary  = ?", total);
                        Salary.Text = "";
                        source.Text = "";
                    }
                }
                OnAppearing();
            }
                
        }

        private async void ButtonDelete_Clicked(object sender, EventArgs e)
        {

            if (String.IsNullOrEmpty(Salary.Text) || String.IsNullOrEmpty(source.Text))
            {
                await DisplayAlert("Alert", "Please select record to delete? ", "OK");
            }
            else
            {
                var result =
                  await DisplayAlert("Confirmation",
                  "Are you sure?",
                  "OK", "Cancel");
                    if (result == true && !Salary.Text.Equals("") && !source.Text.Equals(""))
                    {
                        using (SQLiteConnection conn = new SQLiteConnection(App.filePath))
                        {

                            conn.CreateTable<addSalary>();
                            var updateMarks = conn.ExecuteScalar<addSalary>("DELETE FROM Money WHERE id = ?", ide);
                            
                            total = 0;
                            global = new globals();
                            total = UpdateAmountOnDelete();
                            var updateMoney = conn.ExecuteScalar<ActiveMoney>("UPDATE ActiveMoney Set mySalary  = ?", total);
                            Salary.Text = "";
                            source.Text = "";
                        }
                    }
                OnAppearing();
            }

                
        }

        private double UpdateAmount()
        {
            
            var newAmount = double.Parse(Salary.Text);
            var updateAmount = 0.0;
            var difference = 0.0;
            total = 0;
            global = new globals();
            

            if (newAmount < oldAmount)
            {
                difference = oldAmount - newAmount;
                updateAmount = global.calculateMinusOnTotal(difference);
              
            }
            else
            {
                difference =  newAmount - oldAmount;
                updateAmount = global.calculateTotal(difference);
            }


            return updateAmount;
        }

        private double UpdateAmountOnDelete()
        {

            var newAmount = double.Parse(Salary.Text);
            var updateAmount = 0.0;
            var difference = 0.0;

            global = new globals();
            updateAmount = global.calculateMinusOnTotal(newAmount);

            return updateAmount;
        }
    }
}