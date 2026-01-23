using HomeBudgetProject.Interfaces;
using System.Text.Json;
using System.IO;
using System.Text;

namespace HomeBudgetProject.Classes
{
    public class HomeBudgetPlanner : IHomeBudgetPlanner
    {
        public bool is_raport_generated = false;
        private readonly string filePath;
        private List<IBudgetObserver> observers = new List<IBudgetObserver>();
        public List<BudgetItem> budgetItemsList = new List<BudgetItem>();
        public IRaportStrategy? raportStrategy;
        public HomeBudgetPlanner(string nickname)
        {
            filePath = $"budget_{nickname}.json";
            raportStrategy = new CSVRaportStrategy();
            LoadBudget();
        }

        public void AddExpense(Expense item)
        {
            budgetItemsList.Add(item);
            Notify();
            SaveBudget();
        }

        public void AddIncome(Income item)
        {
            budgetItemsList.Add(item);
            Notify();
            SaveBudget();
        }

        
        public void AddGroup(BudgetGroup group)
        {
            budgetItemsList.Add(group);
            Notify();
            SaveBudget();
        }

        public void RemoveItem(BudgetItem item)
        {
            budgetItemsList.Remove(item);
            Notify();
            SaveBudget();
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
        else if (item is BudgetGroup group)
        {
            total += group.GetIncome(); // 🔥
        }
    }

    return total;
}

public float GetTotalExpense()
{
    float total = 0;

    foreach (var item in budgetItemsList)
    {
        if (item is Expense)
        {
            total += item.GetValue();
        }
        else if (item is BudgetGroup group)
        {
            total += group.GetExpense(); // 🔥
        }
    }

    return total;
}

        public float GetBalance()
        {
            return GetTotalIncome() - GetTotalExpense();
        }

        public List<BudgetItem> GetBudgetItems()
        {
            return budgetItemsList;
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
            is_raport_generated = true;
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


        private void LoadBudget()
        {
            if (File.Exists(filePath) == false) { return; }
            try
            {
                string json = File.ReadAllText(filePath, Encoding.UTF8);
                List<BudgetItem> lista = JsonSerializer.Deserialize<List<BudgetItem>>(json)!;
                if (lista != null)
                {
                    budgetItemsList = lista;
                }
            }
            catch
            {
                budgetItemsList = new List<BudgetItem>();
            }
        }
        private void SaveBudget()
        {
            JsonSerializerOptions option = new JsonSerializerOptions();
            option.WriteIndented = true;
            option.Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping;

            string content = JsonSerializer.Serialize(budgetItemsList, option);
            File.WriteAllText(filePath, content, Encoding.UTF8);
        }
    }
}