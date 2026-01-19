namespace HomeBudgetProject.Classes
{
    public abstract class BudgetItem
    {
        public string Name;
        public float Value;
        public DateTime Date { get; set; }
        public BudgetItem(string name, float value)
        {
            Name = name;
            Value = value;
            Date = DateTime.Now;
        }
        
        public abstract float GetValue();

        public override string ToString()
        {
            return $"{Name}  {Value}\n";
        }
    }
}
