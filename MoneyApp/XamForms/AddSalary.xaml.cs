using MoneyApp.Classes;
using System;
using SQLite;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using System.IO;

namespace MoneyApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddSalary : ContentPage
    {
        int ide;
        globals global;
        double total;
        double oldAmount;
        DateTime dt = DateTime.Now;
        string dateString = "";
        public AddSalary()
        {
            InitializeComponent();
        }

        private async void ButtonSalary_Clicked(object sender, EventArgs e)
        {
            try
            {
                DateTime dt = DateTime.Now; // Or your date, as long as it is in DateTime format
                dateString = dt.ToString("yyyy-MM-dd HH:mm:ss.FFF");

                if (String.IsNullOrEmpty(Salary.Text) || String.IsNullOrEmpty(source.Text))
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
                            date = dateString
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

                                if (foreign.Count == 0)
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
            catch (FormatException)
            {
                await DisplayAlert("Alert", "please  enter a correct number", "OK");
            }

            catch (Exception ex)
            {
                await DisplayAlert("Alert", ex.ToString() , "OK");
            }
            
        }

        protected override async void OnAppearing()
        {
            try
            {
                base.OnAppearing();
                global = new globals();
                using (SQLiteConnection conn = new SQLiteConnection(App.filePath))
                {
                    conn.CreateTable<addSalary>();

                    //var now = DateTime.Now;
                    //var startOfMonth = new DateTime(now.Year, 08, 25);

                  //  var foreign = conn.Query<addSalary>("SELECT id, mySalary, mySource, date FROM Money ");
                    
                    MyListView.ItemsSource = global.GetMonthlyIncomeList(); ;
                    Total.Text = global.getTotal().ToString();
                    //}
                }
            }
            catch(Exception ex)
            {
                await DisplayAlert("Alert", ex.ToString(), "OK");
            }
            
           
        }

        private void MyListView_Focused(object sender, FocusEventArgs e)
        {
           
        }

        private async void StackLayout_Focused(object sender, FocusEventArgs e)
        {
            try
            {
                addSalary add = new addSalary()
                {
                    mySalary = Salary.Text,
                    mySource = source.Text,
                    date = dateString
                };
            }
            catch(Exception ex)
            {
                await DisplayAlert("Alert", ex.ToString(), "OK");
            }
            
        }

        private async void EvetClicked(object sender, SelectedItemChangedEventArgs e)
        {
            try
            {
                var obj = (addSalary)e.SelectedItem;
                ide = Convert.ToInt32(obj.id);
                oldAmount = double.Parse(obj.mySalary);
                Salary.Text = obj.mySalary;
                source.Text = obj.mySource;
            }
            catch(Exception ex)
            {
                await DisplayAlert("Alert", ex.ToString(), "OK");
            }
            
        }

        private async void ButtonEdit_Clicked(object sender, EventArgs e)
        {
              
            try
            {
                

                if (String.IsNullOrEmpty(Salary.Text) || String.IsNullOrEmpty(source.Text))
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
            catch (FormatException)
            {
                await DisplayAlert("Alert", "please  enter a correct number", "OK");
            }
            catch (Exception ex)
            {
                await DisplayAlert("Alert", ex.ToString(), "OK");
            }


            
                
        }

        private async void ButtonDelete_Clicked(object sender, EventArgs e)
        {
            try {


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
                            //conn.Execute("DELETE FROM Money");
                            var updateMarks = conn.ExecuteScalar<addSalary>("DELETE FROM Money WHERE id = ?", ide);




                            total = 0;
                            global = new globals();
                            total = UpdateAmountOnDelete();
                            conn.Execute("DELETE FROM ActiveMoney");
                            var updateMoney = conn.ExecuteScalar<ActiveMoney>("UPDATE ActiveMoney Set mySalary  = ?", total);
                            Salary.Text = "";
                            source.Text = "";
                        }
                    }
                    OnAppearing();
                }
            }
            catch(Exception ex)
            {
                await DisplayAlert("Alert", ex.ToString(), "OK");
            }
            

                
        }

        private double UpdateAmount()
        {
            var updateAmount = 0.0;
            try
            {
                var newAmount = double.Parse(Salary.Text);
                
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
                    difference = newAmount - oldAmount;
                    updateAmount = global.calculateTotal(difference);
                }


                
            }
            catch(Exception ex)
            {
               
            }
            return updateAmount;

        }

        private double UpdateAmountOnDelete()
        {
            var updateAmount = 0.0;
            try
            {
                var newAmount = double.Parse(Salary.Text);
               
                global = new globals();
                updateAmount = global.calculateMinusOnTotal(newAmount);
            }
            catch(Exception ex)
            {
                
            }

            

            return updateAmount;
        }

        private async void clearAll_Clicked(object sender, EventArgs e)
        {
            var result =
              await DisplayAlert("Confirmation",
              "Are you sure?",
              "OK", "Cancel");

            using (SQLiteConnection conn = new SQLiteConnection(App.filePath))
            {

                conn.CreateTable<addSalary>();
                var updateMarks = conn.ExecuteScalar<addSalary>("DELETE FROM Money", ide);

               var  deleteStuff = conn.ExecuteScalar<ActiveMoney>("DELETE FROM ActiveMoney", ide);
                
            }
            OnAppearing();
        }
    }
}