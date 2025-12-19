using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HomeBudgetProject.Interfaces;

namespace HomeBudgetProject.Classes
{
    internal class AutoRaportUpdater : IBudgetObserver
    {
        public void Update(HomeBudgetPlanner planer)
        {
            Console.WriteLine("Wygenerowano autoraport:");
        }
    }
}
