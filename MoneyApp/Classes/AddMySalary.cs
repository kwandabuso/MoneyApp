using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace MoneyApp.Classes
{
    [Table("Moneytest")]
    class AddMySalary
    {

      
        [PrimaryKey, AutoIncrement ]
        public int id
        {
            get;
            set;
        }

        public string mySalary
        {
            get;
            set;
        }

        public string mySource
        {
            get;
            set;
        }

        public DateTime date
        {
            get;
            set;
        }
    }
}
