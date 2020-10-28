using SQLite;
using System.Collections.Generic;

namespace MoneyApp.Classes
{
    class globals
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
    }
}
