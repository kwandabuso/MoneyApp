using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace MoneyApp.Classes
{
    [Table("Budget")]
    class BudgetCls
    {
       
            [PrimaryKey, AutoIncrement]
            public int id
            {
                get;
                set;
            }

            [ForeignKey(typeof(addSalary))]
            public int FK { get; set; }

            public string item
            {
                get;
                set;
            }

            public string amount
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
        
    }
}
