using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace MoneyApp.Classes
{
    [Table("Money")]
    class addSalary
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

        public Boolean isActive
        {
            get;
            set;
        }
    }
}
