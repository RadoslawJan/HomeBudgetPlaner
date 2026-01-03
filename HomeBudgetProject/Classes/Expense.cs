using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HomeBudgetProject.Enums;

namespace HomeBudgetProject.Classes
{
    public class Expense : BudgetItem
    {
        public Expense(string name, float value, CategoryType category)
        {
            this.Name = name;
            this.Value = value;
            this.Category = category;
        }
        public override float GetValue()
        {
            return Value;
        }

    }
}
