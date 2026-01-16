namespace HomeBudgetProject.Classes
{
    public abstract class BudgetItem
    {
        public string Name;
        public float Value;

        public BudgetItem(string name, float value)
        {
            Name = name;
            Value = value;
        }
        
        public abstract float GetValue();

        public override string ToString()
        {
            return $"Element: {Name} Koszt: {Value}";
        }
    }
}
