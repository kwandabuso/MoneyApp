using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MoneyApp.helpers
{
    class HelperMethods
    {
        string path;
        public string setPathToConfigfile()
        {
           // string projectDir = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\.."));

            return "";
        }
        public void writeToFile(params string[] items)
        {  
            var pathToConfigfile = @"C:\MoneyApp\MoneyApp\MoneyApp\ImportantInfo.txt";

            for (int i = 0; i < items.Length; i++)
            {
                //write to html file 
                System.IO.File.AppendAllText(pathToConfigfile, items[i]);
            }
        }

        
    }
}
