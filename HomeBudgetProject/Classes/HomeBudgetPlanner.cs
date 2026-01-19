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
                if (item is BudgetGroup groupToCheck)
                {
                    //ignorujemy spacje i wielkosc liter
                    string name1 = groupToCheck.Name.Trim().ToLower();
                    string name2 = group.Name.Trim().ToLower();

                    if (name1 == name2)
                    {
                        exist = groupToCheck; 
                        break; 
                    }
                }
            }

            if (exist != null)
            {
                MergeSubGroups(exist, group);
            }
            else
            {
                budgetItemsList.Add(group);
            }
            Notify();
        }

        //metoda potrzebna żeby łączyć podkategorie
        private void MergeSubGroups(BudgetGroup existingGroup, BudgetGroup newGroup)
        {
            foreach (var newItem in newGroup.budgetItemList)
            {
                if (newItem is BudgetGroup newSubGroup)
                {

                    BudgetGroup existingSubGroup = null; 

                    foreach (var existingItem in existingGroup.budgetItemList)
                    {
                        if (existingItem is BudgetGroup groupToCheck)
                        {
                            //ignorujemy spacje i wielkość liter 
                            string name1 = groupToCheck.Name.Trim().ToLower();
                            string name2 = newSubGroup.Name.Trim().ToLower();

                            if (name1 == name2)
                            {
                                existingSubGroup = groupToCheck;
                                break;
                            }
                        }
                    }

                    if (existingSubGroup != null)
                    {
                        MergeSubGroups(existingSubGroup, newSubGroup);
                    }
                    else
                    {
                        existingGroup.Add(newSubGroup);
                    }
                }
                else
                {
                    // Wydatki dodajemy bez łączenia
                    existingGroup.Add(newItem);
                }
            }
        }

        public float GetTotalIncome()
        {
            float total = 0;
            foreach (var item in budgetItemsList)
            {
                if (item is Income)
                {
                    total += item.GetValue();
                }
            }

            return total;
        }

        public float GetTotalExpenses()
        {
            float total = 0;

            foreach (var item in budgetItemsList)
            {
                if (!(item is Income))
                {
                    total += item.GetValue();
                }
            }

            return total;
        }

        public float GetBalance()
        {
            return GetTotalIncome() - GetTotalExpenses();
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