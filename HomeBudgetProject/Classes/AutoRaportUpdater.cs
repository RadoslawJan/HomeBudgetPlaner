using HomeBudgetProject.Interfaces;

namespace HomeBudgetProject.Classes
{
    internal class AutoRaportUpdater : IBudgetObserver
    {
        public void Update(HomeBudgetPlanner planner)
        {
            if (planner.is_raport_generated == true)
            {
                var autoStrategy = new CSVRaportStrategy();
                autoStrategy.GenerateRaport(planner);
            }
        }
    }
}
