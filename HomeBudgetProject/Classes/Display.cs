using HomeBudgetProject.Interfaces;

namespace HomeBudgetProject.Classes
{
    internal class Display : IBudgetObserver
    {
        public void Update(HomeBudgetPlanner planner)
        {
            Console.WriteLine("WZmiana w budżecie");
        }

        public void ShowLoginScreen()
        {
            ShowMenu(new List<MenuOption>
            {
                new MenuOption("Wypróbuj jako gość", () => MainMenu()),
                new MenuOption("Zaloguje się", () => Login()),
                new MenuOption("Zarejestruj się", () => Register())
            });

        }

        public void ShowPlan(HomeBudgetPlanner planner)
        {
            foreach (BudgetItem item in planner.budgetItemsList)
            {
                Console.WriteLine(item);
            }
        }
        

        public void Login()
        {
            return;
        }

        public void Register()
        {
            return;
        }

        public void MainMenu()
        {

        }

        public void ShowCategories(HomeBudgetPlanner planner)
        {
            Console.WriteLine("Lista elementów w budżecie:");
        }

        public void ShowCategoryDetails(BudgetGroup group)
        {
            Console.WriteLine($"Szczegóły grupy {group.Name}:");
        }

        public void ShowMenu(List<MenuOption> options)
        {
            Console.WriteLine("Wybierz opcję:");
            for (int i = 0; i < options.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {options[i].Description}");
            }

            if (int.TryParse(Console.ReadLine(), out int choice) && choice > 0 && choice <= options.Count)
            {
                options[choice - 1].Action?.Invoke();   
            }
            else
            {
                Console.WriteLine("Nieprawidłowy wybór.");
            }
        }

        
    }


    public class MenuOption
    {
        public string? Description {get; set;}
        public Action? Action {get; set;}

        public MenuOption(string description, Action action)
        {
            Description = description;
            Action = action;
        }
    }
}
