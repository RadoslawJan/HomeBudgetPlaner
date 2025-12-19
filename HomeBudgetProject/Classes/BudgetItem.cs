using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HomeBudgetProject.Enums;

namespace HomeBudgetProject.Classes
{
    public abstract class BudgetItem
    {
        public string Name;
        public float Value;
        public CategoryType Category;

        public abstract float GetValue();
    }
}
