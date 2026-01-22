using HomeBudgetProject.Classes;
using HomeBudgetProject.Enums;

namespace HomeBudgetProject
{
    internal class Program
    {
        static void Main(string[] args)
        {


            /*
            UserManager userManager = new UserManager();
            bool running = true;

            Console.WriteLine("RĘCZNY KREATOR UŻYTKOWNIKÓW");

            while (running)
            {
                Console.WriteLine("\n[NOWY UŻYTKOWNIK]");

                Console.Write("Podaj nick: ");
                string nickname = Console.ReadLine() ?? "";

                Console.Write("Podaj hasło: ");
                string password = Console.ReadLine() ?? "";

                Console.WriteLine("Wybierz poziom (0-Guest, 1-NormalUser, 2-VIP, 3-Admin):");
                if (!Enum.TryParse(Console.ReadLine(), out StatusLevel level))
                {
                    Console.WriteLine("Błąd: Nieprawidłowy poziom!");
                    continue;
                }

                string? adminPass = null;
                if (level == StatusLevel.VIP || level == StatusLevel.Admin)
                {
                    Console.Write("Wymagane hasło administratora: ");
                    adminPass = Console.ReadLine();
                }

                bool success = userManager.RegisterUser(nickname, password, level, adminPass);

                if (success)
                {
                    Console.WriteLine($"\nSUKCES: Użytkownik {nickname} został zapisany do users.json!");
                }
                else
                {
                    Console.WriteLine("\nBŁĄD: Nie można dodać użytkownika (zajęty nick lub brak uprawnień admina).");
                }

                Console.Write("\nCzy chcesz dodać kolejnego? (t/n): ");
                if (Console.ReadLine()?.ToLower() != "t")
                {
                    running = false;
                }
            }

            Console.WriteLine("\nZamykanie kreatora. Sprawdź plik JSON w folderze projektu.");

            Console.WriteLine("Kliknij enter żeby kontynuować");
            Console.ReadLine();
            Console.Clear();

            Console.WriteLine("\nTest logowania");
            Console.Write("Podaj nick do zalogowania: ");
            string loginNick = Console.ReadLine() ?? "";
            Console.Write("Podaj hasło: ");
            string loginPass = Console.ReadLine() ?? "";

            User? loggedUser = userManager.Authenticate(loginNick, loginPass);

            if (loggedUser != null)
            {
                Console.WriteLine($"\nZALOGOWANO POMYŚLNIE jako {loggedUser.Nickname} [{loggedUser.Status}]");
                Logger logger = Logger.GetInstance();
                logger.Log(LogType.LogIn, loggedUser, $"Użytkownik {loggedUser.Nickname} wszedł do systemu");
            }
            else
            {
                Console.WriteLine("\nBŁĄD: Nieprawidłowy login lub hasło. Nie można przetestować uprawnień.");
            }

            Console.WriteLine("\nKoniec programu. Naciśnij dowolny klawisz...");
            Console.ReadKey();

            */
            
            Display display = new Display();
            display.ShowLoginScreen();
            //User guest = new User("guest", "guest", StatusLevel.Guest);
            /*
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

           logger.GetLogsForAdmin(twujStary);
            */
        }
    }
}
