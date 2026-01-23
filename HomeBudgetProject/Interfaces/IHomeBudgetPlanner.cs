using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HomeBudgetProject.Classes;

namespace HomeBudgetProject.Interfaces
{
    public interface IHomeBudgetPlanner
    {
        void AddIncome(Income item);
        void AddExpense(Expense item);
        void AddGroup(BudgetGroup group);
        bool RemoveItem(BudgetItem item);

        List<BudgetItem> GetBudgetItems();
        void GenerateRaport();
        void SetStrategy(IRaportStrategy strategy);
        void Attach(IBudgetObserver observer);
        void Detach(IBudgetObserver observer);
        void Notify();
    }
}
