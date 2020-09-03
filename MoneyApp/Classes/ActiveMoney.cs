using SQLite;

namespace MoneyApp.Classes
{
    [Table("ActiveMoney")]
    class ActiveMoney
    {
        [PrimaryKey, AutoIncrement]
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

    }
}
