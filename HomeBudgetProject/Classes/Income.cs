using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HomeBudgetProject.Enums;

namespace HomeBudgetProject.Classes
{
    public class Income : BudgetItem
    {
        public Income(string name, float value)
        {
            this.Name = name;
            this.Value = value;
            this.Category=CategoryType.Przychody;

        }
        public override float GetValue()
        {
            return Value;
        }

    }
}
