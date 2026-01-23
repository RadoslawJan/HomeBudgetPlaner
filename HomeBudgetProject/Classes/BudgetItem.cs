using System.Text.Json.Serialization;
namespace HomeBudgetProject.Classes
{
    [JsonDerivedType(typeof(Expense), "Expense")]
    [JsonDerivedType(typeof(Income), "Income")]
    [JsonDerivedType(typeof(BudgetGroup), "Group expense")]
    public abstract class BudgetItem
    {
        
        public string Name { get; set; }
        public float Value { get; set; }
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
            return $"{Name} {Value}zł\n";
        }
    }
}