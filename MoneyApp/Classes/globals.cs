using SQLite;
using System;
using System.Collections.Generic;

namespace MoneyApp.Classes
{
    public class globals
    {

        public double calculateTotal(double oldAmount)
        {
            List<ActiveMoney> intList = new List<ActiveMoney>();
            var Fkey = 0.0;

            using (SQLiteConnection conn = new SQLiteConnection(App.filePath))
            {
                conn.CreateTable<ActiveMoney>();
                var foreign = conn.Query<ActiveMoney>("SELECT mySalary FROM ActiveMoney");

                //conn.Execute("UPDATE Money SET isActive = false WHERE id =1");



                foreach (var fK in foreign)
                {
                    if (!string.IsNullOrEmpty(fK.mySalary.ToString()))
                    {
                        Fkey = double.Parse(fK.mySalary);
                    }


                }

            }


            return Fkey + oldAmount;
        }

        public double calculateMinusOnTotal(double oldAmount)
        {
            List<ActiveMoney> intList = new List<ActiveMoney>();
            var Fkey = 0.0;

            using (SQLiteConnection conn = new SQLiteConnection(App.filePath))
            {
                conn.CreateTable<ActiveMoney>();
                var foreign = conn.Query<ActiveMoney>("SELECT mySalary FROM ActiveMoney");

                //conn.Execute("UPDATE Money SET isActive = false WHERE id =1");



                foreach (var fK in foreign)
                {
                    if (!string.IsNullOrEmpty(fK.mySalary.ToString()))
                    {
                        Fkey = double.Parse(fK.mySalary);
                    }


                }

            }


            return Fkey - oldAmount;
        }

        public double budgetMinusOnTotal(double oldAmount)
        {
            List<ActiveMoney> intList = new List<ActiveMoney>();
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
                        Fkey = fK.amount;
                    }


                }

            }


            return Fkey - oldAmount;
        }

        public List<BudgetCls> getMonthlyBudgetItems()
        {
            List<BudgetCls> MonthlybudgetItemsList = new List<BudgetCls>();
            var Fkey = 0.0;

            using (SQLiteConnection conn = new SQLiteConnection(App.filePath))
            {
                conn.CreateTable<BudgetCls>();
                MonthlybudgetItemsList = conn.Query<BudgetCls>("SELECT item, amount FROM Budget WHERE isActive = 1");

                //conn.Execute("UPDATE Money SET isActive = false WHERE id =1");



                //foreach (var fK in MonthlybudgetItemsList)
                //{
                //    if (!string.IsNullOrEmpty(fK.amount.ToString()))
                //    {
                //        Fkey = fK.amount;
                //    }


                //}

            }


            return MonthlybudgetItemsList;
        }

        public List<spendMoney> getMonthlyItems()
        {

            List<spendMoney> monthlyIncomeList = new List<spendMoney>();

            try
            {

                using (SQLiteConnection conn = new SQLiteConnection(App.filePath))
                {
                    conn.CreateTable<spendMoney>();
                    var sql = "";
                    var now = DateTime.Now;
                    int prevMonth = now.AddMonths(-1).Month;
                    int prevYear = now.AddYears(-1).Year;
                    var startOfMonth = new DateTime(now.Year, now.Month, 1);
                    var DaysInMonth = DateTime.DaysInMonth(now.Year, now.Month);
                    var endOfTheMonth = new DateTime(now.Year, now.Month, DaysInMonth);
                    var endOfMonthDecember = new DateTime(prevYear, prevMonth, 28);


                    string startDate = startOfMonth.ToString("yyyy/MM/dd HH:mm:ss.FFF");
                    string endDate = endOfTheMonth.ToString("yyyy/MM/dd HH:mm:ss.FFF");
                    string startDateDecember = endOfMonthDecember.ToString("yyyy/MM/dd HH:mm:ss.FFF");
                    sql = "SELECT id, mySalary, mySource, date FROM Money";
                    monthlyIncomeList = conn.Query<spendMoney>(sql);

                    if ((now.Day >= 28) && (now.Day <= DaysInMonth))
                    {
                        sql = "SELECT item, Amount, addedAt FROM Spend WHERE addedAt BETWEEN '" + startDate + "' AND '" + endDate + "'";
                    }
                    else if (now.Month == 1)
                    {
                        sql = "SELECT item, Amount, addedAt FROM Spend WHERE addedAt BETWEEN  '" + startDateDecember + "' AND '" + endDate + "'";
                    }
                    else
                    {
                        sql = "SELECT item, Amount, addedAt FROM Spend WHERE addedAt BETWEEN  '" + startDate + "' AND '" + endDate + "'";
                    }

                    return monthlyIncomeList = conn.Query<spendMoney>(sql);

                }
            }
            catch (Exception ex)
            {
            }

            return monthlyIncomeList;
        }

        public List<string> getMonthlySpendItems()
        {
            List<string> MonthlybudgetItemsList = new List<string>();
            var Fkey = 0.0;

            using (SQLiteConnection conn = new SQLiteConnection(App.filePath))
            {
                conn.CreateTable<BudgetCls>();
                var executeSql = conn.Query<BudgetCls>("SELECT item FROM Budget WHERE isActive = 1");

                //conn.Execute("UPDATE Money SET isActive = false WHERE id =1");



                foreach (var fK in executeSql)
                {
                    if (!string.IsNullOrEmpty(fK.amount.ToString()))
                    {
                        MonthlybudgetItemsList.Add(fK.item);
                    }


                }

            }


            return MonthlybudgetItemsList;
        }

        public double getSavingsTotalById(string id)
        {
           
            var Fkey = 0.0;

            using (SQLiteConnection conn = new SQLiteConnection(App.filePath))
            {
                conn.CreateTable<spendMoney>();
                var foreign = conn.Query<spendMoney>("SELECT Amount FROM Spend WHERE id = ?", id);

                var fforeign = conn.Query<spendMoney>("SELECT Amount FROM Spend");
                //conn.Execute("UPDATE Money SET isActive = false WHERE id =1");



                foreach (var fK in foreign)
                {
                    if (!string.IsNullOrEmpty(fK.amount.ToString()))
                    {
                        Fkey = fK.amount;
                    }


                }

            }


            return Fkey;
        }

        public double getSavingsTotal()
        {

            var Fkey = 0.0;

            using (SQLiteConnection conn = new SQLiteConnection(App.filePath))
            {
                conn.CreateTable<spendMoney>();
                var foreign = conn.Query<spendMoney>("SELECT Amount FROM Spend");

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

        public double getTotal()
        {
            List<ActiveMoney> intList = new List<ActiveMoney>();
            var Fkey = 0.0;

            using (SQLiteConnection conn = new SQLiteConnection(App.filePath))
            {
                conn.CreateTable<ActiveMoney>();
                var foreign = conn.Query<ActiveMoney>("SELECT mySalary FROM ActiveMoney");

                foreach (var fK in foreign)
                {
                    if (!string.IsNullOrEmpty(fK.mySalary.ToString()))
                    {
                        Fkey = double.Parse(fK.mySalary);
                    }


                }

            }


            return Fkey;
        }

        public double calculateDifferenceOnTotal(double oldAmount, double newAmount)
        {
            double diff = 0.0;
            double total = 0.0;
            if (oldAmount > newAmount)
            {
                diff = oldAmount - newAmount;
                total = calculateTotal(diff);
            }
            else
            {
                diff =  newAmount - oldAmount;
                total = calculateMinusOnTotal(diff);
            }
                
            return total;
        }

        public List<addSalary> GetMonthlyIncomeList()
        {
            List<addSalary> monthlyIncomeList = new List<addSalary>();

             try
            {
                
                using (SQLiteConnection conn = new SQLiteConnection(App.filePath))
                {
                    conn.CreateTable<addSalary>();
                    var sql = "";
                    var now = DateTime.Now;
                    int prevMonth = now.AddMonths(-1).Month;
                    int prevYear = now.AddYears(-1).Year;
                    var startOfMonth = new DateTime(now.Year, now.Month,1);
                    var DaysInMonth = DateTime.DaysInMonth(now.Year, now.Month);
                    var endOfTheMonth = new DateTime(now.Year, now.Month, DaysInMonth);
                    var endOfMonthDecember = new DateTime(prevYear, prevMonth, 28);


                    string startDate = startOfMonth.ToString("yyyy/MM/dd HH:mm:ss.FFF");
                    string endDate = endOfTheMonth.ToString("yyyy/MM/dd HH:mm:ss.FFF");
                    string startDateDecember = endOfMonthDecember.ToString("yyyy/MM/dd HH:mm:ss.FFF");
                    sql = "SELECT id, mySalary, mySource, date FROM Money";
                    monthlyIncomeList = conn.Query<addSalary>(sql);

                    if ((now.Day >= 28) && (now.Day <= DaysInMonth))
                    {
                        sql = "SELECT id, mySalary, mySource, date FROM Money WHERE date BETWEEN '" + startDate + "' AND '" + endDate + "'";
                    }
                    else if (now.Month == 1)
                    {
                        sql = "SELECT id, mySalary, mySource, date FROM Money WHERE date BETWEEN '" + startDateDecember + "' AND '" + endDate + "'";
                    }
                    else 
                    {
                        sql = "SELECT id, mySalary, mySource, date FROM Money WHERE date BETWEEN '" + startDate + "' AND '" + endDate + "'";
                    }

                    return monthlyIncomeList = conn.Query<addSalary>(sql);

                }
            }
            catch (Exception ex)
            {
            }
            return monthlyIncomeList;
        }

    }
}
