using HomeBudgetProject.Interfaces;

namespace HomeBudgetProject.Classes
{
    internal class AutoRaportUpdater : IBudgetObserver
    {
        public void Update(HomeBudgetPlanner planer)
        {
            Console.WriteLine("System: Wykryto zmiany, aktualizuję raport automatyczny...");
            var autoStrategy = new CSVRaportStrategy();
            autoStrategy.GenerateRaport(planer);
        }
    }
}
