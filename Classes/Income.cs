namespace HomeBudgetProject.Classes
{
    public class Income : BudgetItem
    {
        public Income(string name, float value): base(name, value) {}
        public override float GetValue()
        {
            return Value;
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
