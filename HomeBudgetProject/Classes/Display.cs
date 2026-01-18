using HomeBudgetProject.Classes;
using HomeBudgetProject.Enums;
using HomeBudgetProject.Interfaces;

namespace HomeBudgetProject.Classes
{
    internal class Display : IBudgetObserver
    {
        private IHomeBudgetPlanner _planner;
        private UserManager _userManager = new UserManager();
        private User _currentUser;
        public void Update(HomeBudgetPlanner planner)
        {
            Console.WriteLine("Dokonano zmian w budżecie");
        }

        public void ShowLoginScreen()
        {
            Console.Clear();
            Console.WriteLine("\t\tAPLIKACJA BUDŻETU DOMOWEGO\n");
            ShowMenu(new List<MenuOption>
            {
                new MenuOption("Wypróbuj jako gość", () => {AddUser("guest", "guest", StatusLevel.Guest); MainMenu(); }),
                new MenuOption("Zaloguje się", () => Login()),
                new MenuOption("Zarejestruj się", () => Register()),
                new MenuOption("Zakończ", () => Environment.Exit(0))
            });

        }

        public void ShowPlan(HomeBudgetPlanner planner)
        {
            Console.Clear();
            Console.WriteLine("\t\tAPLIKACJA BUDŻETU DOMOWEGO\n");
            Console.WriteLine("[STAN BUDŻETU]\n");
            if (planner == null || planner.budgetItemsList.Count == 0)
            {
                Console.WriteLine("Nie ma tu żadnych wydatków/przychodów");
                return;
            }
            foreach (BudgetItem item in planner.budgetItemsList)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine("Naciśnij dowolny przyciska aby przejść dalej");
            Console.ReadKey();
        }


        public void Login()
        {
            Console.Clear();
            Console.WriteLine("[LOGOWANIE]");
            Console.Write("Podaj nazwę: ");
            string login = Console.ReadLine() ?? "";
            Console.Write("Podaj hasło: ");
            string password = Console.ReadLine() ?? "";

            User? authenticatedUser = _userManager.Authenticate(login, password);

            if (authenticatedUser != null)
            {
                AddUser(authenticatedUser.Nickname, authenticatedUser.Password, authenticatedUser.Status);
                MainMenu();
            }
            else
            {
                Console.WriteLine("Błędny login lub hasło! Naciśnij klawisz...");
                Console.ReadKey();
            }
        }

        public void Register()
        {
            Console.Clear();
            Console.WriteLine("[REJESTRACJA]");
            Console.Write("Nowa nazwa: ");
            string login = Console.ReadLine() ?? "";
            Console.Write("Nowe hasło: ");
            string password = Console.ReadLine() ?? "";

            Console.WriteLine("Wybierz poziom (0-Guest, 1-NormalUser, 2-VIP, 3-Admin):");
            if (Enum.TryParse(Console.ReadLine(), out StatusLevel level))
            {
                string? adminKey = null;
                if (level >= StatusLevel.VIP)
                {
                    Console.Write("Podaj hasło administratora, aby nadać te uprawnienia: ");
                    adminKey = Console.ReadLine();
                }

                if (_userManager.RegisterUser(login, password, level, adminKey))
                {
                    Console.WriteLine("Zarejestrowano pomyślnie!");
                }
                else
                {
                    Console.WriteLine("Nazwa zajęta lub błędne hasło administratora.");
                }
            }
            Console.ReadKey();
        }

        public void MainMenu()
        {

            bool run = true;
            while (run)
            {
                Console.Clear();
                Console.WriteLine("\t\tAPLIKACJA BUDŻETU DOMOWEGO\n");
                Console.WriteLine($"\nWitaj! {_currentUser.Nickname} [{_currentUser.Status}]\n");

                ShowMenu(new List<MenuOption>
                {
                    new MenuOption("Dodaj Wydatek", () => AddExpenseMenu()),
                    new MenuOption("Dodaj Przychód", () => AddIncomeMenu()),
                    new MenuOption("Pokaż Budżet", () => ShowPlan((HomeBudgetPlanner)((HomeBudgetPlannerProxy)_planner).GetRealService())),               
                    new MenuOption("Wyloguj", () => { run = false; ShowLoginScreen(); })
                });
            }
        }

        private void AddExpenseMenu()
        {
            Console.Clear();
            Console.WriteLine("\t\tAPLIKACJA BUDŻETU DOMOWEGO\n");
            Console.WriteLine("[DODAWANIE WYDATKU]\n");

            Console.WriteLine("Podaj nazwę kategorii: ");
            string mainCategory = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(mainCategory)) mainCategory = "Ogólne";

            BudgetGroup mainGroup = new BudgetGroup(mainCategory, "Główna Kategoria");

            Console.Write("Podaj nazwę podkategorii: ");
            string subCategory = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(subCategory)) subCategory = "Inne";

            BudgetGroup subGroup = new BudgetGroup(subCategory, "Podkategoria");

            mainGroup.Add(subGroup);

            Console.Write("Podaj nazwę wydatku: ");
            string name = Console.ReadLine();

            Console.Write("Podaj kwote: ");
            if (float.TryParse(Console.ReadLine(), out float value) && value>0)
            {
                Expense expense = new Expense(name, value);
                subGroup.Add(expense);
                _planner.AddGroup(mainGroup);

            }
            else
            {
                Console.WriteLine("Podana wartość jest nieprawidłowa!");
                Console.WriteLine("Naciśnij dowolny przyciska aby przejść dalej");
                Console.ReadKey();
            }
        }

        private void AddIncomeMenu()
        {
            Console.Clear();
            Console.WriteLine("\t\tAPLIKACJA BUDŻETU DOMOWEGO\n");
            Console.WriteLine("[DODAWANIE PRZYCHODU]\n");
            Console.Write("Podaj nazwe przychodu: ");
            string name = Console.ReadLine();
            Console.Write("Podaj kwote: ");
            if (float.TryParse(Console.ReadLine(), out float value) && value>0)
            {
                _planner.AddIncome(new Income(name, value));
            }
            else
            {
                Console.WriteLine("Podana wartość jest nieprawidłowa!");
                Console.WriteLine("Naciśnij dowolny przyciska aby przejść dalej");
                Console.ReadKey();
            }
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

        private void AddUser(string name, string pass, StatusLevel status)
        {
            _currentUser = new User(name, pass, status);

            var realPlanner = new HomeBudgetPlanner();

            realPlanner.Attach(this);

            _planner = new HomeBudgetPlannerProxy(_currentUser, realPlanner);
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

