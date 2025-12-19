using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HomeBudgetProject.Interfaces;

namespace HomeBudgetProject.Classes
{
    internal class Display : IBudgetObserver
    {
        public void Update(HomeBudgetPlanner planner)
        {
            Console.WriteLine("WZmiana w budżecie");
        }

        public void ShowLoginScreen()
        {
            Console.WriteLine("Ekran logowania: ");
        }

        public void ShowMenu()
        {
            Console.WriteLine("Menu");
        }

        public void ShowCategories(HomeBudgetPlanner planner)
        {
            Console.WriteLine("Lista elementów w budżecie:");
        }

        public void ShowCategoryDetails(BudgetGroup group)
        {
            Console.WriteLine($"Szczegóły grupy {group.Name}:");
        }
    }
}
