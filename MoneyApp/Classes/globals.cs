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
        public double getTotal()
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


            return Fkey;
        }
    }
}
