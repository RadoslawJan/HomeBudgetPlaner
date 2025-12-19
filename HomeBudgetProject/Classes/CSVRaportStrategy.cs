using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HomeBudgetProject.Interfaces;

namespace HomeBudgetProject.Classes
{
    internal class CSVRaportStrategy : IRaportStrategy
    {
        public void GenerateRaport(HomeBudgetPlanner planner)
        {
            Console.WriteLine("Generowanie raportu CSV....");
        }

    }
}
