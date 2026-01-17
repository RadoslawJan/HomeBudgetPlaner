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
            budgetItemsList.Add(item);
            Notify();
        }

        public void AddIncome(Income item)
        {
            budgetItemsList.Add(item);
            Notify();
        }

        public void AddGroup(BudgetGroup group)
        {
            BudgetGroup exist = null;

            foreach (var item in budgetItemsList)
            {
                if (item is BudgetGroup cat && cat.Name == group.Name)
                {
                    exist = cat;
                    break;
                }
            }

            if (exist != null)
            {
                foreach (var Item in group.budgetItemList)
                {
                    exist.Add(Item);
                }
            }
            else
            {
                budgetItemsList.Add(group);
            }
            Notify();
        }

        public void Attach(IBudgetObserver observer)
        {
            if (!observers.Contains(observer))
            {
                observers.Add(observer);
            }
        }

        public void Detach(IBudgetObserver observer)
        {
            observers.Remove(observer);
        }

        public void GenerateRaport()
        {
            if (raportStrategy == null)
            {
                throw new InvalidOperationException("Nie ustawiono strategii raportowania!");
            }
            raportStrategy.GenerateRaport(this);
        }

        public void Notify()
        {
            foreach (var observer in observers)
            {
                observer.Update(this);
            }
        }

        public void SetStrategy(IRaportStrategy strategy)
        {
            this.raportStrategy = strategy;
        }
    }
}