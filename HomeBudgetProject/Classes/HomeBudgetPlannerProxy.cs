using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HomeBudgetProject.Enums;
using HomeBudgetProject.Interfaces;

namespace HomeBudgetProject.Classes
{
    internal class HomeBudgetPlannerProxy : IHomeBudgetPlanner
    {
        private HomeBudgetPlanner service;
        private User user;

        public HomeBudgetPlannerProxy(User user)
        {
            this.user = user;
            service = new HomeBudgetPlanner();
        }
        private bool HasPermission()
        {
            if (user.Status == StatusLevel.Guest)
            {
                Console.WriteLine("Brak uprawnień");
                return false;
            }
            return true; // Narazie podstawowe sprawdzenie
        }
        public void AddExpense(Expense item)
        {
            throw new NotImplementedException();
        }

        public void AddIncome(Income item)
        {
            throw new NotImplementedException();
        }

        public void Attach(IBudgetObserver observer)
        {
            throw new NotImplementedException();
        }

        public void Detach(IBudgetObserver observer)
        {
            throw new NotImplementedException();
        }

        public void GenerateRaport()
        {
            throw new NotImplementedException();
        }

        public void Notify()
        {
            throw new NotImplementedException();
        }

        public void SetStrategy(IRaportStrategy strategy)
        {
            throw new NotImplementedException();
        }
    }
}
