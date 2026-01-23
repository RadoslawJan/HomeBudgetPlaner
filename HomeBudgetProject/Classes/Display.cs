using System.Data;
using HomeBudgetProject.Classes;
using HomeBudgetProject.Enums;
using HomeBudgetProject.Interfaces;

namespace HomeBudgetProject.Classes
{
    internal class Display : IBudgetObserver
    {
        Logger logger = Logger.GetInstance();

        public void Update(HomeBudgetPlanner planner)
        {
            Console.WriteLine("[POWIADOMIENIE] Dokonano zmian w budżecie.");
        }

        // EKRAN LOGOWANIA
        public User ShowLoginScreen(UserManager userManager)
        {
            User? selectedUser = null;

            while (selectedUser == null)
            {
                Console.Clear();
                Console.WriteLine("\t\tAPLIKACJA BUDŻETU DOMOWEGO\n");
                
                ShowMenu(new List<MenuOption>
                {
                    new MenuOption("Wypróbuj jako gość", () => {
                        selectedUser = new User("guest", "guest", StatusLevel.Guest);
                    }),
                    new MenuOption("Zaloguj się", () => {
                        selectedUser = Login(userManager);
                    }),
                    new MenuOption("Zarejestruj się", () => {
                        Register(userManager);
                    }),
                    new MenuOption("Zakończ", () => Environment.Exit(0))
                });
            }

            return selectedUser;
        }

        // LOGOWANIE 
        public User? Login(UserManager userManager)
        {
            Console.Clear();
            Console.WriteLine("[LOGOWANIE]");
            Console.Write("Podaj nazwę: ");
            string login = Console.ReadLine() ?? "";
            Console.Write("Podaj hasło: ");
            string password = Console.ReadLine() ?? "";

            User? authenticatedUser = userManager.Authenticate(login, password);

            if (authenticatedUser != null)
            {
                return authenticatedUser;
            }
            else
            {
                Console.WriteLine("Błędny login lub hasło! Naciśnij dowolny klawisz.");
                Console.ReadKey();
                return null;
            }
        }

        // REJESTRACJA
        public void Register(UserManager userManager)
        {
            Console.Clear();
            Console.WriteLine("[REJESTRACJA]");
            
            string login = "";
            while (login.Length < 3)
            {
                Console.Write("Nowa nazwa (min. 3 znaki): ");
                login = Console.ReadLine() ?? "";
            }

            string password = "";
            while (password.Length < 6)
            {
                Console.Write("Nowe hasło (min. 6 znaków): ");
                password = Console.ReadLine() ?? "";
            }

            Console.WriteLine("Wybierz poziom użytkownika:");
            Console.WriteLine("1. NormalUser");
            Console.WriteLine("2. VIP");
            Console.WriteLine("3. Admin");
            Console.Write("Twój wybór: ");
            string choice = Console.ReadLine() ?? "";

            StatusLevel level = StatusLevel.NormalUser;
            string? adminKey = null;

            switch (choice)
            {
                case "1":
                    level = StatusLevel.NormalUser;
                    break;
                case "2":
                    Console.Write("Wpisz 'Kupuję VIP' aby potwierdzić: ");
                    if (Console.ReadLine() == "Kupuję VIP")
                    {
                        level = StatusLevel.VIP;
                    }
                    else
                    {
                        Console.WriteLine("Anulowano. Naciśnij klawisz.");
                        Console.ReadKey();
                        return;
                    }
                    break;
                case "3":
                    level = StatusLevel.Admin;
                    Console.Write("Podaj hasło autoryzacyjne obecnego administratora: ");
                    adminKey = Console.ReadLine();
                    break;
                default:
                    Console.WriteLine("Nieprawidłowy wybór.");
                    Console.ReadKey();
                    return;
            }

            bool success = userManager.RegisterUser(login, password, level, adminKey);

            if (success)
            {
                Console.WriteLine("Zarejestrowano pomyślnie! Możesz się teraz zalogować.");
            }
            else
            {
                Console.WriteLine("Błąd rejestracji (login zajęty lub złe hasło admina).");
            }
            Console.ReadKey();
        }

        // MENU GŁÓWNE
        public void MainMenu(User user, HomeBudgetPlannerProxy proxy)
        {

            bool running = true;
            while (running)
            {
                Console.Clear();
                Console.WriteLine("\t\tAPLIKACJA BUDŻETU DOMOWEGO\n");
                Console.WriteLine($"\nWitaj! {user.Nickname} [{user.Status}]\n");

                ShowMenu(new List<MenuOption>
                {
                    new MenuOption("Dodaj Wydatek", () => AddExpenseMenu(proxy)),
                    new MenuOption("Dodaj Przychód", () => AddIncomeMenu(proxy)),
                    new MenuOption("Dodaj grupę budzetową", () => AddGroupMenu(proxy)),
                    new MenuOption("Pokaż budżet", () => ShowPlan(proxy)),
                    new MenuOption("Usuń element", () => RemoveItemMenu(proxy)),
                    new MenuOption("Zapisz raport do pliku", () => proxy.GenerateRaport()),
                    new MenuOption("Zmień strategię zapisu", () => ChangeStrategyMenu(proxy)),
                    new MenuOption("Wyloguj", () => {running = false;})
                });
            }
        }

        // DODAWANIE WYDATKÓW
        private void AddExpenseMenu(HomeBudgetPlannerProxy proxy)
        {
            Console.Clear();
            Console.WriteLine("\t\tAPLIKACJA BUDŻETU DOMOWEGO\n");
            Console.WriteLine("[DODAWANIE WYDATKU]\n");

            Console.Write("Podaj nazwę wydatku: ");
            string name = Console.ReadLine()!;

            Console.Write("Podaj kwote: ");
            string text = Console.ReadLine()!;
            if(int.TryParse(text, out int value))
            {
                proxy.AddExpense(new Expense(name, value));
                Update(proxy.GetRealService());
                Console.WriteLine("Dodano wydatek");
            }
            else
            {
                Console.WriteLine("Błąd");
                return;
            }
        }

        // DODAWANIE WYDATKÓW
        private void AddIncomeMenu(HomeBudgetPlannerProxy proxy)
        {
            Console.Clear();
            Console.WriteLine("\t\tAPLIKACJA BUDŻETU DOMOWEGO\n");
            Console.WriteLine("[DODAWANIE PRZYCHODU]\n");

            Console.Write("Podaj nazwę przychodu: ");
            string name = Console.ReadLine()!;

            Console.Write("Podaj kwote: ");
            string text = Console.ReadLine()!;
            if(int.TryParse(text, out int value))
            {
                proxy.AddIncome(new Income(name, value));
                Console.WriteLine("Dodano przychód");
            }
            else
            {
                Console.WriteLine("Błąd");
                return;
            }
        }

        // DODAWANIE GRUP
        private void AddGroupMenu(HomeBudgetPlannerProxy proxy)
        {
            Console.Clear();
            Console.WriteLine("\t\tAPLIKACJA BUDŻETU DOMOWEGO\n");
            Console.WriteLine("[KREOWANIE GRUPY]\n");

            Console.Write("Podaj nazwę grupy: ");
            string name = Console.ReadLine()!;

            Console.Write("Określ kategorię: ");
            string category = Console.ReadLine()!;

            BudgetGroup group = new BudgetGroup(name, category);
            
            bool running = true;

            while (running)
            {
                ShowMenu(new List<MenuOption>
                {
                    new MenuOption("Dodaj Wydatek", () => AddExpenseToGroup(group)),
                    new MenuOption("Dodaj Przychód", () => AddIncomeToGroup(group)),
                    new MenuOption("Dodaj Podgrupę", () => AddGroupMenu(proxy)),
                    new MenuOption("Zakończ", () => {proxy.AddGroup(group); running = false;})
                });
            }
        }

        // DODAWANIE WYDATKU DO GRUPY
        private void AddExpenseToGroup(BudgetGroup group)
        {
            Console.Clear();
            Console.Write("Podaj nazwę wydatku: ");
            string name = Console.ReadLine()!;

            Console.Write("Podaj kwote: ");
            string text = Console.ReadLine()!;
            if(int.TryParse(text, out int value))
            {
                group.Add(new Expense(name, value));
                Console.WriteLine("Dodano wydatek");
            }
            else
            {
                Console.WriteLine("Błąd");
                return;
            }
        }
        // DODAWANIE PRZYCHODU DO GRUPY
        private void AddIncomeToGroup(BudgetGroup group)
        {
            Console.Clear();
            Console.Write("Podaj nazwę przychodu: ");
            string name = Console.ReadLine()!;

            Console.Write("Podaj kwote: ");
            string text = Console.ReadLine()!;
            if(int.TryParse(text, out int value))
            {
                group.Add(new Income(name, value));
                Console.WriteLine("Dodano przychód");
            }
            else
            {
                Console.WriteLine("Błąd");
                return;
            }
        }

        // USUWANIE ELEMNTU
        private void RemoveItemMenu(HomeBudgetPlannerProxy proxy)
        {
            Console.Clear();
            Console.WriteLine("[USUWANIE ELEMENTU]\n");

            var items = proxy.GetBudgetItems();

            if (items.Count == 0)
            {
                Console.WriteLine("Brak elementów do usunięcia.");
                Console.ReadKey();
                return;
            }

            for (int i = 0; i < items.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {items[i]}");
            }

            Console.Write("\nWybierz numer elementu do usunięcia: ");
            if (!int.TryParse(Console.ReadLine(), out int choice) ||
                choice < 1 || choice > items.Count)
            {
                Console.WriteLine("Nieprawidłowy wybór.");
                Console.ReadKey();
                return;
            }

            var item = items[choice - 1];
            proxy.RemoveItem(item);
            Console.WriteLine("Element usunięty.");



            Console.ReadKey();
        }



        

        // MENU BUDŻETU
        public void ShowPlan(HomeBudgetPlannerProxy proxy)
        {
                Console.Clear();
                Console.WriteLine("\t\tAPLIKACJA BUDŻETU DOMOWEGO\n");
                Console.WriteLine("[STAN BUDŻETU]\n");
                float totalExpenses = proxy.GetTotalExpense();
                float totalIncome = proxy.GetTotalIncome();
                float balance = proxy.GetBalance();

                Console.WriteLine($"\t\tObecny balans: {balance} zł\n");

                List<BudgetItem> list = proxy.GetBudgetItems();

                foreach(var item in list)
                {
                    Console.WriteLine(item);
                }

                ShowMenu(new List<MenuOption>
            {
                new MenuOption($"Pokaż wydatki  (Łącznie: {totalExpenses} zł)", () => ShowExpensesList(proxy)),
                new MenuOption($"Pokaż przychody (Suma: {totalIncome} zł)", () => ShowIncomesList(proxy)),
                new MenuOption("Wróć", () => { })

            });

        }

        // WYŚWIETLANIE WYDATKÓW
        private void ShowExpensesList(HomeBudgetPlannerProxy proxy)
        {
            Console.Clear();
            Console.WriteLine("[SZCZEGÓŁY WYDATKÓW]\n");

            bool empty = true;
            foreach (var item in proxy.GetBudgetItems())
            {
                if (!(item is Income))
                {
                    Console.WriteLine(item);
                    empty = false;
                }
            }

            if (empty)
            {
                Console.WriteLine("Brak wydatków.");
            }

            Console.WriteLine("\nNaciśnij dowolny przycisk aby wrócić.");
            Console.ReadKey();
        }

        // WYŚWIETLANIE PRZYCHODÓW
        private void ShowIncomesList(HomeBudgetPlannerProxy proxy)
        {
            Console.Clear();
            Console.WriteLine("[SZCZEGÓŁY PRZYCHODÓW]\n");

            bool empty = true;
            foreach (var item in proxy.GetBudgetItems())
            {
                if (item is Income)
                {
                    Console.WriteLine(item);
                    empty = false;
                }
            }

            if (empty)
            {
                Console.WriteLine("Brak przychodów.");
            }

            Console.WriteLine("\nNaciśnij dowolny przycisk aby wrócić.");
            Console.ReadKey();
        }



        // WYBÓR FORMATU PLIKU
        private void ChangeStrategyMenu(HomeBudgetPlannerProxy proxy)
        {
            Console.Clear();
            Console.WriteLine("\t\tAPLIKACJA BUDŻETU DOMOWEGO\n");
            Console.WriteLine("[ZMIANA STRATEGII ZAPISU]\n");
            Console.WriteLine("Wybierz format pliku:");
            ShowMenu(new List<MenuOption>
            {
                new MenuOption("Zapisuj jako .PDF", () =>
                {
                    proxy.SetStrategy(new PDFRaportStrategy());
                    Console.WriteLine("\nNaciśnij dowolny klawisz.");
                    Console.ReadKey();
                }),
                new MenuOption("Zapisuj jako .CSV", () =>
                {
                    proxy.SetStrategy(new CSVRaportStrategy());
                    Console.WriteLine("\nNaciśnij dowolny klawisz.");
                    Console.ReadKey();
                }),
                new MenuOption("Wróć", () => { }) 
            });

        }

        // METODA POMOCNICZA DO WYŚWIETLANIA MENU
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

