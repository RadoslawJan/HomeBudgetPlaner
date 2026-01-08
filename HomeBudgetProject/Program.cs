using System.Data;
using System.Numerics;
using System.Runtime.CompilerServices;
using HomeBudgetProject.Classes;
using HomeBudgetProject.Enums;

namespace HomeBudgetProject
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string name, password;
            StatusLevel level;
            int userStatusLevel;

            Console.WriteLine("Home budget planer");

            Display display = new Display();
            display.ShowLoginScreen();
            Console.WriteLine("Podaj nick/imie:");
            name = Console.ReadLine();
            Console.WriteLine("Podaj hasło: ");
            password = Console.ReadLine();
            Console.WriteLine("podaj typ użytkownika: (1 - Gość, 2 - Zwykły użytkownik, 3 - VIP, 4 -admin)");
            userStatusLevel = Int32.Parse(Console.ReadLine());
            switch (userStatusLevel)
            {
                case 1:
                    level = StatusLevel.Guest;
                    break;
                case 2:
                    level = StatusLevel.NormalUser;
                    break;
                case 3:
                    level = StatusLevel.VIP;
                    break;
                case 4:
                    level = StatusLevel.Admin;
                    break;
                default:
                    level = StatusLevel.NormalUser;
                    break;
            }

            User currentUser = new User(name, password, level);

            Console.WriteLine($"Obecnie zalogowany użytkownik: {name} o statusie {level}");
            Console.WriteLine("Naciśnij ENTER, aby wejść do systemu...");
            Console.ReadLine();
            Console.Clear();

            HomeBudgetPlannerProxy proxy = new HomeBudgetPlannerProxy(currentUser);
            HomeBudgetPlanner planner = new HomeBudgetPlanner();

            Console.WriteLine("Wybierz format raportu: 1 - PDF, 2 - CSV");
            string choice = Console.ReadLine();

            if (choice == "1")
            {
                planner.SetStrategy(new PDFRaportStrategy());
            }
            else
            {
                planner.SetStrategy(new CSVRaportStrategy());
            }


            planner.GenerateRaport();
            Console.ReadLine();
            Console.Clear();

            display.ShowMenu();

            Console.WriteLine("\nKoniec testu. Naciśnij klawisz.");
            Console.ReadKey();
        }
    }
}
