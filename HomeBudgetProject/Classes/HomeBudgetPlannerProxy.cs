using HomeBudgetProject.Enums;
using HomeBudgetProject.Interfaces;

namespace HomeBudgetProject.Classes
{
    internal class HomeBudgetPlannerProxy : IHomeBudgetPlanner
    {
        private HomeBudgetPlanner _realService;
        private User user;

        private Logger logger;

        public HomeBudgetPlannerProxy(User user, HomeBudgetPlanner service)
        {
            this.user = user;
            _realService = service;
            logger = Logger.GetInstance();
        }

public void AddExpense(Expense item)
{
    _realService.AddExpense(item);

    if (user.Status == StatusLevel.Guest)
    {
        logger.Log(LogType.SuccessfulOperation, user, $"[DEMO] Dodano wydatek: {item.Name}");
    }
    else
    {
        logger.Log(LogType.SuccessfulOperation, user, $"Dodano wydatek: {item.Name}");
    }
}


        public void AddIncome(Income item)
        {
            _realService.AddIncome(item);
            logger.Log(LogType.SuccessfulOperation, user, $"Dodano nowy przychód");
        }

        public void AddGroup(BudgetGroup group)
        {
            _realService.AddGroup(group);
            logger.Log(LogType.SuccessfulOperation, user, $"Dodano nową grupę");
        }

        public bool RemoveItem(BudgetItem item)
{
    if (user.Status == StatusLevel.Guest)
    {
        Console.WriteLine("Gość nie może usuwać elementów.");
        logger.Log(LogType.FailedOperation, user, "Próba usunięcia elementu przez gościa");
        return false;
    }

    bool result = _realService.RemoveItem(item);

    if (result)
    {
        logger.Log(LogType.SuccessfulOperation, user, $"Usunięto element: {item.Name}");
    }
    else
    {
        logger.Log(LogType.FailedOperation, user, $"Nie udało się usunąć elementu: {item.Name}");
    }

    return result;
}


        public float GetTotalIncome()
        {
            return _realService.GetTotalIncome();
        }

        public float GetTotalExpense()
        {
             return _realService.GetTotalExpense();
        }
        public float GetBalance()
        {
             return _realService.GetBalance();
        }
        public List<BudgetItem> GetBudgetItems()
        {
            return _realService.GetBudgetItems();
        }

        public void Attach(IBudgetObserver observer)
        {
            _realService.Attach(observer);
        }

        public void Detach(IBudgetObserver observer)
        {
            _realService.Detach(observer);
        }

        public void Notify()
        {
            _realService.Notify();
        }

        public void GenerateRaport()
        {
            if (!HasPermission(nameof(GenerateRaport)))
            {
                Console.WriteLine("Nie masz uprawnień do generowania raportu");
                logger.Log(LogType.FailedOperation, user, "Nie posiada uprawnień do generowania raportu");
                return;
            }
            _realService.GenerateRaport();
            logger.Log(LogType.SuccessfulOperation, user, $"Wygenerowano raport {_realService.raportStrategy}");
       
        }

        public void SetStrategy(IRaportStrategy strategy)
        {
            if (!HasPermission(nameof(SetStrategy)))
            {
                Console.WriteLine("Nie masz uprawnień do zmiany formatu raportu");
                logger.Log(LogType.FailedOperation, user, "Nie posiada uprawnień do zmiany formatu raportu");
                return;
            }
            _realService.SetStrategy(strategy);
            logger.Log(LogType.SuccessfulOperation, user, $"Ustawiono format raportu na {strategy}");
        }

        private bool HasPermission(string methodName)
        {
            switch (methodName)
            {
                case "GenerateRaport":
                    if (user.Status < StatusLevel.NormalUser)
                    {
                        return false;
                    }
                    break;
                case "SetStrategy":
                    if (user.Status < StatusLevel.VIP)
                    {
                        return false;
                    }
                    break;
            }
            return true;
        }

        public HomeBudgetPlanner GetRealService()
        {
            return _realService;
        }
    }
}
