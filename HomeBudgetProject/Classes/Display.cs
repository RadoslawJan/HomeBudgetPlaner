using HomeBudgetProject.Classes;
using HomeBudgetProject.Enums;
using HomeBudgetProject.Interfaces;

namespace HomeBudgetProject.Classes
{
    internal class Display : IBudgetObserver
    {
        private IHomeBudgetPlanner _planner;
        private UserManager userManager = new UserManager();
        private User currentUser;
        public void Update(HomeBudgetPlanner planner)
        {
            Console.WriteLine("Dokonano zmian w budżecie");
        }


        //początkowy ekran
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
        //menu po wejściu w stan budżetu
        public void ShowPlan(HomeBudgetPlanner planner)
        {
                Console.Clear();
                Console.WriteLine("\t\tAPLIKACJA BUDŻETU DOMOWEGO\n");
                Console.WriteLine("[STAN BUDŻETU]\n");
                float totalExpenses = planner.GetTotalExpenses();
                float totalIncome = planner.GetTotalIncome();
                float balance = planner.GetBalance();

                Console.WriteLine($"\t\tObecny balans: {balance} zł\n");

                ShowMenu(new List<MenuOption>
            {
                new MenuOption($"Pokaż wydatki  (Łącznie: {totalExpenses} zł)", () => ShowExpensesList(planner)),
                new MenuOption($"Pokaż przychody (Suma: {totalIncome} zł)", () => ShowIncomesList(planner)),
                new MenuOption("Wróć", () => { })

            });

        }
        //pomocnicza metoda do wyswietlania wydatków
        private void ShowExpensesList(HomeBudgetPlanner planner)
        {
            Console.Clear();
            Console.WriteLine("[SZCZEGÓŁY WYDATKÓW]\n");

            bool empty = true;
            foreach (var item in planner.budgetItemsList)
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

        //pomocnicza metoda do wyświetlania przychodów
        private void ShowIncomesList(HomeBudgetPlanner planner)
        {
            Console.Clear();
            Console.WriteLine("[SZCZEGÓŁY PRZYCHODÓW]\n");

            bool empty = true;
            foreach (var item in planner.budgetItemsList)
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

        //metoda do logowania
        public void Login()
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
                AddUser(authenticatedUser.Nickname, authenticatedUser.Password, authenticatedUser.Status);
                MainMenu();
            }
            else
            {
                Console.WriteLine("Błędny login lub hasło! Naciśnij dowolny klawisz.");
                Console.ReadKey();
            }

            //Console.Write("Podaj nick do zalogowania: ");
            //string loginNick = Console.ReadLine() ?? "";
            //Console.Write("Podaj hasło: ");
            //string loginPass = Console.ReadLine() ?? "";

            //User? loggedUser = userManager.Authenticate(loginNick, loginPass);

            //if (loggedUser != null)
            //{
            //    Console.WriteLine($"\nZALOGOWANO POMYŚLNIE jako {loggedUser.Nickname} [{loggedUser.Status}]");
            //}
            //else
            //{
            //    Console.WriteLine("\nBŁĄD: Nieprawidłowy login lub hasło. Nie można przetestować uprawnień.");
            //}

            
        }
        //metoda do rejestracji
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

                if (userManager.RegisterUser(login, password, level, adminKey))
                {
                    Console.WriteLine("Zarejestrowano pomyślnie!");
                }
                else
                {
                    Console.WriteLine("Nazwa zajęta lub błędne hasło administratora.");
                }
            }
            Console.ReadKey();

            //Console.WriteLine("\n[NOWY UŻYTKOWNIK]");

            //Console.Write("Podaj nick: ");
            //string nickname = Console.ReadLine() ?? "";

            //Console.Write("Podaj hasło: ");
            //string password = Console.ReadLine() ?? "";

            //Console.WriteLine("Wybierz poziom (0-Guest, 1-NormalUser, 2-VIP, 3-Admin):");
            //if (!Enum.TryParse(Console.ReadLine(), out StatusLevel level))
            //{
            //    Console.WriteLine("Błąd: Nieprawidłowy poziom!");
            //    return;
            //}

            //string? adminPass = null;
            //if (level == StatusLevel.VIP || level == StatusLevel.Admin)
            //{
            //    Console.Write("Wymagane hasło administratora: ");
            //    adminPass = Console.ReadLine();
            //}

            //bool success = userManager.RegisterUser(nickname, password, level, adminPass);

            //if (success)
            //{
            //    Console.WriteLine($"\nSUKCES: Użytkownik {nickname} został zapisany do users.json!");
            //}
            //else
            //{
            //    Console.WriteLine("\nBŁĄD: Nie można dodać użytkownika (zajęty nick lub brak uprawnień admina).");
            //}
        }

        //główne menu po zalogowaniu sie
        public void MainMenu()
        {

            bool run = true;
            while (run)
            {
                Console.Clear();
                Console.WriteLine("\t\tAPLIKACJA BUDŻETU DOMOWEGO\n");
                Console.WriteLine($"\nWitaj! {currentUser.Nickname} [{currentUser.Status}]\n");

                ShowMenu(new List<MenuOption>
                {
                    new MenuOption("Dodaj Wydatek", () => AddExpenseMenu()),
                    new MenuOption("Dodaj Przychód", () => AddIncomeMenu()),
                    new MenuOption("Pokaż Budżet", () => ShowPlan((HomeBudgetPlanner)((HomeBudgetPlannerProxy)_planner).GetRealService())),
                    new MenuOption("Zapisz raport do pliku", () => GenerateReportMenu()),
                    new MenuOption("Wyloguj", () => { run = false; ShowLoginScreen(); })
                });
            }
        }

        //dodawanie wydatków
        private void AddExpenseMenu()
        {
            Console.Clear();
            Console.WriteLine("\t\tAPLIKACJA BUDŻETU DOMOWEGO\n");
            Console.WriteLine("[DODAWANIE WYDATKU]\n");

            Console.Write("Podaj nazwę kategorii: ");
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
                Console.WriteLine("Naciśnij dowolny przycisk aby przejść dalej");
                Console.ReadKey();
            }
        }
        //dodawanie przychodów
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
                Console.WriteLine("Naciśnij dowolny przycisk aby przejść dalej");
                Console.ReadKey();
            }
        }
        //ekran z wyborem formatu pliku
        private void GenerateReportMenu()
        {
            Console.Clear();
            Console.WriteLine("\t\tAPLIKACJA BUDŻETU DOMOWEGO\n");
            Console.WriteLine("[GENEROWANIE RAPORTU DO PLIKU]\n");
            Console.WriteLine("Wybierz format pliku:");
            ShowMenu(new List<MenuOption>
    {
        new MenuOption("Zapisz jako .PDF", () =>
        {
            _planner.SetStrategy(new PDFRaportStrategy());
            _planner.GenerateRaport();
            Console.WriteLine("\nNaciśnij dowolny klawisz.");
            Console.ReadKey();
        }),
        new MenuOption("Zapisz jako .CSV", () =>
        {
            _planner.SetStrategy(new CSVRaportStrategy());
            _planner.GenerateRaport();
            Console.WriteLine("\nNaciśnij dowolny klawisz.");
            Console.ReadKey();
        }),
        new MenuOption("Wróć", () => { }) 
    });

        }

        //metoda odpowiedzialna za dodanie nowego użytkownika
        private void AddUser(string name, string pass, StatusLevel status)
        {
            currentUser = new User(name, pass, status);

            var realPlanner = new HomeBudgetPlanner();

            realPlanner.Attach(this);

            realPlanner.Attach(new AutoRaportUpdater());

            _planner = new HomeBudgetPlannerProxy(currentUser, realPlanner);
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

