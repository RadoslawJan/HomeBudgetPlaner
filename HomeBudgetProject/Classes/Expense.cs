namespace HomeBudgetProject.Classes
{
    public class Expense : BudgetItem
    {
        public Expense(string name, float value): base(name, value) {}
        public override float GetValue()
        {
            return Value;
        }
        public override string ToString()
        {
            return $"{Name} - {Value} z�  [{Date:dd-MM-yyyy}]";
        }

    }
}
