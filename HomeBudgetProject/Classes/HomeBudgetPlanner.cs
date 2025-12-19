using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HomeBudgetProject.Interfaces;

namespace HomeBudgetProject.Classes
{
    public class HomeBudgetPlanner : IHomeBudgetPlanner
    {
        private List<IBudgetObserver> observers = new List<IBudgetObserver>();
        public List<BudgetItem> budgetItemsList = new List<BudgetItem>();
        public IRaportStrategy raportStrategy;
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