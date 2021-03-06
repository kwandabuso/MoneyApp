﻿using MoneyApp.Classes;
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
            try
            {
                if (String.IsNullOrEmpty(Item.Text) || String.IsNullOrEmpty(Amount.Text))
                {
                    await DisplayAlert("Alert", "Please enter all fields? ", "OK");
                }
                else
                {

                    
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
                       
                    }
                }
                Item.Text = "";
                Amount.Text = "";
                OnAppearing();
            }
            catch (Exception ex)
            {
                await DisplayAlert("Alert", ex.ToString(), "OK");
            }
        }

        protected override async void OnAppearing()
        {
            try
            {

                base.OnAppearing();

                using (SQLiteConnection conn = new SQLiteConnection(App.filePath))
                {
                    conn.CreateTable<BudgetCls>();
                    var salarie = conn.Table<BudgetCls>().ToList();
                    MyListView.ItemsSource = salarie;

                }

                total.Text = getBudgetTotal().ToString();
            }
            catch (Exception ex)
            {
                await DisplayAlert("Alert", ex.ToString(), "OK");
            }
        }

        private async void ButtonEdit_Clicked(object sender, EventArgs e)
        {
            try
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


                        }
                    }
                    Item.Text = "";
                    Amount.Text = "";
                    OnAppearing();
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Alert", ex.ToString(), "OK");
            }

        }

        private async void ButtonDelete_Clicked(object sender, EventArgs e)
        {
            try
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

                           
                        }
                    }
                    Item.Text = "";
                    Amount.Text = "";
                    OnAppearing();
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Alert", ex.ToString(), "OK");
            }

        }

        private async void MyListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            try
            {
                var obj = (BudgetCls)e.SelectedItem;
                ide = Convert.ToInt32(obj.id);

                Item.Text = obj.item;
                Amount.Text = obj.amount.ToString();
            }
            catch (Exception ex)
            {
                await DisplayAlert("Alert", ex.ToString(), "OK");
            }

        }

        private double getBudgetTotal()
        {
            List<BudgetCls> intList = new List<BudgetCls>();
            var Fkey = 0.0;

            try
            {
                
                using (SQLiteConnection conn = new SQLiteConnection(App.filePath))
                {
                    conn.CreateTable<BudgetCls>();

                    var foreign = conn.Query<BudgetCls>("SELECT amount FROM Budget");

                    foreach (var fK in foreign)
                    {
                        if (!string.IsNullOrEmpty(fK.amount.ToString()))
                        {
                            Fkey += fK.amount;
                        }

                    }

                }
            }
            catch (Exception ex)
            {
                
            }

            return Fkey;
        }

        private  double UpdateAmountOnDeleteAsync()
        {
            double updateAmount = 0.0;

            try
            {
                var newAmount = double.Parse(Amount.Text);
                

                global = new globals();
                updateAmount = global.budgetMinusOnTotal(newAmount);
            }
            catch (Exception ex)
            {
                
            }


            return updateAmount;
        }

        private async void StartBudget_Clicked(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                await DisplayAlert("Alert", ex.ToString(), "OK");
            }
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

        private async void clearAll_Clicked(object sender, EventArgs e)
        {
            var result =
             await DisplayAlert("Confirmation",
             "Are you sure?",
             "OK", "Cancel");

            using (SQLiteConnection conn = new SQLiteConnection(App.filePath))
            {

                conn.CreateTable<BudgetCls>();
                var updateMarks = conn.ExecuteScalar<BudgetCls>("DELETE FROM Budget", ide);


            }
            Item.Text = "";
            Amount.Text = "";
            OnAppearing();
        }
    }
}
    
