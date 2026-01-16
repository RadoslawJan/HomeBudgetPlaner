namespace HomeBudgetProject.Classes
{
    public class BudgetGroup : BudgetItem
    {
        public List<BudgetItem> budgetItemList = new List<BudgetItem>();
        string Category;

        public BudgetGroup(string name, string category): base(name, 0)
        {
            Category = category;
        }

        public void Add(BudgetItem item)
        {
            budgetItemList.Add(item);
        }

        public void Remove(BudgetItem item)
        {
            budgetItemList.Remove(item);
        }
        public override float GetValue()
        {
            float total = 0;
            foreach (var item in budgetItemList)
            {
                total += item.GetValue();
            }
            return total;
        }

        public override string ToString()
        {
            string result = $"{Name}: {Category}\n";
            foreach (BudgetItem item in budgetItemList)
            {
                result += $"\t{item}\n";
            }
            result += $"{Value}";
            return result;
        }


    }
}
