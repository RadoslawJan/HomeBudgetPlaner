using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeBudgetProject.Classes
{
    internal class Logger
    {
        private static Logger _instance;
        private List<string> logEntries;

        public static Logger GetInstance()
        {
            return _instance;
        }
        public void Log(string message)
        {
            Console.WriteLine($"[LOG]: ");
        }

        public List<string> GetLogsForAdmin()
        {
            return new List<string>(logEntries);
        }
    }
}
