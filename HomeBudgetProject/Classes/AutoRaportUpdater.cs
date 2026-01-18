using HomeBudgetProject.Interfaces;

namespace HomeBudgetProject.Classes
{
    internal class AutoRaportUpdater : IBudgetObserver
    {
        public void Update(HomeBudgetPlanner planer)
        {
            Console.WriteLine("Wygenerowano autoraport:");
        }
    }
}
