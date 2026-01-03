using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HomeBudgetProject.Enums;

namespace HomeBudgetProject.Classes
{
    public class BudgetGroup : BudgetItem
    {
        public List<BudgetItem> budgetItemList = new List<BudgetItem>();

        public BudgetGroup(CategoryType category) {
            this.Category = category;
            this.Name = category.ToString();
            this.Value = 0;
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
    }
}
