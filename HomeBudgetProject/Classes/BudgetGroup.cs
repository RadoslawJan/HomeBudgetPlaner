using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeBudgetProject.Classes
{
    public class BudgetGroup : BudgetItem
    {
        public List<BudgetItem> budgetItemList = new List<BudgetItem>();
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
            throw new NotImplementedException();
            // Tu dodać sumowanie z listy np przez linq
        }
    }
}
