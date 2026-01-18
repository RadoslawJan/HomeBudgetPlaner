using HomeBudgetProject.Interfaces;
using System.Text;

namespace HomeBudgetProject.Classes
{
    public class CSVRaportStrategy : IRaportStrategy
    {
        public void GenerateRaport(HomeBudgetPlanner planner)
        {
            var sb = new StringBuilder();
            sb.AppendLine("Type,Name,Value");

            foreach (var item in planner.budgetItemsList)
            {
                AppendItem(sb, item);
            }

            File.WriteAllText("budget_report.csv", sb.ToString());
            Console.WriteLine("Raport CSV zapisany: budget_report.csv");
        }

        private void AppendItem(StringBuilder sb, BudgetItem item)
        {
            if (item is BudgetGroup group)
            {
                sb.AppendLine($"Group,{group.Name},{group.GetValue()}");

                foreach (var subItem in group.budgetItemList)
                {
                    AppendItem(sb, subItem);
                }
            }
            else
            {
                sb.AppendLine($"{item.GetType().Name},{item.Name},{item.GetValue()}");
            }
        }
    }
}
