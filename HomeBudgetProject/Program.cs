using HomeBudgetProject.Classes;
using HomeBudgetProject.Enums;

namespace HomeBudgetProject
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Display display = new Display();
            UserManager userManager = new UserManager();
            
            while (true)
            {
                User loggedUser = display.ShowLoginScreen(userManager);

                HomeBudgetPlanner planner = new HomeBudgetPlanner();
                planner.Attach(display);
                planner.Attach(new AutoRaportUpdater());

                HomeBudgetPlannerProxy proxy = new HomeBudgetPlannerProxy(loggedUser, planner);

                display.MainMenu(loggedUser, proxy);
            }
            
        }
    }
}
