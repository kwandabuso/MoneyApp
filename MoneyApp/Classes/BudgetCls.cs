using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace MoneyApp.Classes
{
    [Table("Budget")]
    public class BudgetCls
    {
       
            [PrimaryKey, AutoIncrement]
            public int id
            {
                get;
                set;
            }

            public string item
            {
                get;
                set;
            }

            public double amount
            {
                get;
                set;
            }

            public string addedAt
            {
                get;
                set;
            }
            public string updatedAt
            {
                get;
                set;
            }

        public bool isActive
        {
            get;
            set;
        }

        
    }
}
