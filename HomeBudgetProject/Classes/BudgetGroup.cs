using System.Text;

namespace HomeBudgetProject.Classes
{
    public class BudgetGroup : BudgetItem
    {
        public List<BudgetItem> budgetItemList { get; set; } = new List<BudgetItem>();
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
            StringBuilder sb = new StringBuilder();

            sb.Append($"[{Name}] - £¹cznie: {GetValue()} z³");

            foreach (BudgetItem item in budgetItemList)
            {
                string childText = item.ToString();
                string indentedChild = "\n\t" + childText.Replace("\n", "\n\t");
                sb.Append(indentedChild);
            }

            return sb.ToString();
        }


    }
}
