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
        int spendItemId;
        globals global;
        Boolean isLoadFirstTime = true; 
        double total;
        double totalBudget;
        double oldAmount;
        string dateString = ""; 

        public Spend()
        {
            InitializeComponent();
            DateTime dt = DateTime.Now;
            dateString = dt.ToString("yyyy-MM-dd HH:mm:ss.FFF");
          
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (drpBudgetItems.SelectedIndex ==-1    || String.IsNullOrEmpty(Amount.Text))
                {
                    await DisplayAlert("Alert", "Please enter all fields? ", "OK");
                }
                else
                {
                    spendMoney add = new spendMoney()
                    {
                        item = drpBudgetItems.SelectedItem.ToString(),
                        amount = double.Parse(Amount.Text),
                        addedAt = dateString.ToString(),
                        updatedAt = dateString.ToString()

                    };

                    using (SQLiteConnection conn = new SQLiteConnection(App.filePath))
                    {
                        conn.CreateTable<spendMoney>();
                        int rows = conn.Insert(add);
                        var result =
                 await DisplayAlert("Confirmation",
                 "Are you sure?",
                 "OK", "Cancel");
                        if (result == true)
                        {

                            deductFromTotal();
                        }

                    }
                   // drpBudgetItems.SelectedIndex = -1;
                    Amount.Text = "";
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
      
        protected override void OnAppearing()
        {
            base.OnAppearing();

            using (SQLiteConnection conn = new SQLiteConnection(App.filePath))
            {
                global = new globals();
                conn.CreateTable<spendMoney>();
                var salarie = conn.Table<spendMoney>().ToList();
                
            }
            if (isLoadFirstTime)
            {
                drpBudgetItems.ItemsSource = global.getMonthlySpendItems();
                isLoadFirstTime = false;
            }
            else {
                var selectedItm = drpBudgetItems.Items[drpBudgetItems.SelectedIndex];
                MyListView.ItemsSource = global.getMonthlyItems(selectedItm);
                TotalSpend.Text = global.getOutstandingAmountperBudgetItem(drpBudgetItems.Items[drpBudgetItems.SelectedIndex]).ToString();
            }
            
        }

        private async void Edit_Clicked(object sender, EventArgs e)
        {
            if (drpBudgetItems.SelectedIndex == -1 || String.IsNullOrEmpty(Amount.Text))
            {
                await DisplayAlert("Alert", "Please enter all fields? ", "OK");
            }
            else
            {
                var result =
                  await DisplayAlert("Confirmation",
                  "Are you sure? ",
                  "OK", "Cancel");
                if (result == true && drpBudgetItems.SelectedIndex != -1 && !Amount.Text.Equals(""))
                {
                    

                    using (SQLiteConnection conn = new SQLiteConnection(App.filePath))
                    {
                        conn.CreateTable<spendMoney>();
                        var sql = "UPDATE Spend SET amount = '"+ Amount.Text +"' , updatedAt ='"+ dateString.ToString() + "' WHERE  id ='"+spendItemId+"'";
                       var updateMarks = conn.ExecuteScalar<spendMoney>(sql);

                        //total = 0;
                        global = new globals();
                        total = global.calculateDifferenceOnTotal(oldAmount,double.Parse(Amount.Text));

                        //var updateMoney = conn.ExecuteScalar<ActiveMoney>("UPDATE ActiveMoney Set mySalary  = ?", total);
                        //drpBudgetItems.SelectedIndex = -1;

                        global.updateBudgetByItemName(total.ToString(), drpBudgetItems.SelectedItem.ToString());


                        OnAppearing();
                        Amount.Text = "";
                    }

                    
                }
            }

        }

        private async void delete_Clicked(object sender, EventArgs e)
        {
            if (drpBudgetItems.SelectedIndex == -1 || String.IsNullOrEmpty(Amount.Text))
            {
                await DisplayAlert("Alert", "Please select record to delete? ", "OK");
            }
            else
            {
                var result =
                  await DisplayAlert("Confirmation",
                  "Are you sure?",
                  "OK", "Cancel");
                if (result == true && drpBudgetItems.SelectedIndex != -1 && !Amount.Text.Equals(""))
                {
                    using (SQLiteConnection conn = new SQLiteConnection(App.filePath))
                    {

                        conn.CreateTable<addSalary>();
                        var updateMarks = conn.ExecuteScalar<spendMoney>("DELETE FROM Spend WHERE id = ?", spendItemId);

                        total = 0;
                        global = new globals();
                        total = global.calculateTotal(oldAmount);
                        var updateMoney = conn.ExecuteScalar<ActiveMoney>("UPDATE ActiveMoney Set mySalary  = ?", total);
                        drpBudgetItems.SelectedIndex = -1;
                        Amount.Text = "";
                    }
                }
                OnAppearing();
            }

        }

        private void EvetClicked(object sender, SelectedItemChangedEventArgs e)
        {
            var obj = (spendMoney)e.SelectedItem;
            spendItemId = obj.id;
            drpBudgetItems.SelectedItem = obj.item;
            Amount.Text = obj.amount.ToString();

            oldAmount =double.Parse(Amount.Text);
        }

        private double UpdateAmountOnDelete()
        {

           
            var updateAmount = 0.0;

            global = new globals();
            updateAmount = global.calculateTotal(global.getSavingsTotalById(spendItemId.ToString()));

            return updateAmount;
        }

        private void deductFromTotal()
        {
            
                
            global = new globals();
            var selectedItm = drpBudgetItems.Items[drpBudgetItems.SelectedIndex];
            var boughtItemPrice = global.getOutstandingAmountperBudgetItem(drpBudgetItems.Items[drpBudgetItems.SelectedIndex]) - Double.Parse(Amount.Text);
            global.updateBudget(boughtItemPrice.ToString(), selectedItm);

                
            
        }

        private async void clearall_Clicked(object sender, EventArgs e)
        {
           
                var result =
                  await DisplayAlert("Confirmation",
                  "Are you sure?",
                  "OK", "Cancel");
                
                    using (SQLiteConnection conn = new SQLiteConnection(App.filePath))
                    {

                        conn.CreateTable<spendMoney>();
                        var updateMarks = conn.ExecuteScalar<spendMoney>("DELETE FROM Spend", spendItemId);

                        
                    }
            drpBudgetItems.SelectedIndex = -1;
            Amount.Text = "";
            OnAppearing();
            
        }

        private void drpBudgetItems_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            var selectedItm = drpBudgetItems.Items[drpBudgetItems.SelectedIndex];
            TotalSpend.Text = global.getOutstandingAmountperBudgetItem(drpBudgetItems.Items[drpBudgetItems.SelectedIndex]).ToString();

            MyListView.ItemsSource = global.getMonthlyItems(selectedItm);
            //TotalOutstanding.Text = global.getMonthlySpendAmountperItem(drpBudgetItems.Items[drpBudgetItems.SelectedIndex]).ToString();
            //OnAppearing();

        }
    }
}