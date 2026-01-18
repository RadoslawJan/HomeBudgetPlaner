using HomeBudgetProject.Classes;
using HomeBudgetProject.Enums;

namespace HomeBudgetProject
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Display display = new Display();
            User guest = new User("guest", "guest", StatusLevel.Guest);
            Logger logger = Logger.GetInstance();

            HomeBudgetPlanner planner = new HomeBudgetPlanner();
            HomeBudgetPlannerProxy plannerProxy = new HomeBudgetPlannerProxy(guest, planner);

            
            plannerProxy.AddExpense(new Expense("Wengiel", 300));
            display.ShowPlan(planner);


            BudgetGroup group = new BudgetGroup("Damian", "Rozrywka");
            BudgetGroup group2 = new BudgetGroup("1.", " Piwko");
            group.Add(new Expense("Wyjście z dziewczyną", 75));
            group2.Add(new Expense("Piwo z chłopakami", 50));
            group2.Add(new Expense("Drugie piwo z chłopakami", 70));
            group.Add(group2);
            plannerProxy.AddGroup(group);

            display.ShowPlan(planner);

            User twujStary = new User("twujStary", "haslo123", StatusLevel.NormalUser);
            HomeBudgetPlanner planerTwojegoStarego = new HomeBudgetPlanner();
            HomeBudgetPlannerProxy twujStaryProxy = new HomeBudgetPlannerProxy(twujStary, planerTwojegoStarego);
            twujStaryProxy.AddExpense(new Expense("Kupno nowego laptopa", 2000));
            twujStaryProxy.AddExpense(new Expense("Kupno nowego telefonu", 1500));
            twujStaryProxy.AddIncome(new Income("Pensja", 5000));
            display.ShowPlan(planerTwojegoStarego);
            
            planerTwojegoStarego.SetStrategy(new CSVRaportStrategy());
            planerTwojegoStarego.GenerateRaport();

           planerTwojegoStarego.SetStrategy(new PDFRaportStrategy());
           planerTwojegoStarego.GenerateRaport();

           logger.GetLogsForAdmin();

        }
    }
}
