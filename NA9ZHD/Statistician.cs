namespace NA9ZHD;
/// <summary>
/// Sztetösztisön
/// Statisztikus osztály melyben egy hónap, év bevételeit, kiadásait tekinthetjük meg, vagy hasonlíthatjuk össze
/// </summary>
public abstract class Statistician
{
    /// <summary>
    /// Kiíratjuk a tranzakciókat
    /// </summary>
    public abstract void ShowData();
    /// <summary>
    /// Kiszámoljuk a profitot
    /// </summary>
    public abstract int[] CalculateProfit();
    /// <summary>
    /// Kiíratjuk a profitot
    /// </summary>
    public abstract void ShowProfit();
    protected abstract byte ComparePrevious(Statistician previous);
    public abstract void CompareProfits(Statistician other);
}
internal class MonthStatistician : Statistician
{
    MonthlyLedger Ledger { get; set; }
    public MonthStatistician(MonthlyLedger ledger)
    {
        this.Ledger = ledger;
    }
    public override void ShowData()
    {
        UIManager.Print("\n");
        foreach (var transaction in Ledger.GetTransactions()) UIManager.PrintTransaction(transaction);
    }
    public override int[] CalculateProfit()
    {
        int expenses = 0, incomes = 0;
        foreach (var transaction in Ledger.GetTransactions())
        {
            if (transaction.isExpense) expenses += transaction.amount;
            else incomes += transaction.amount;
        }
        return [incomes, expenses, incomes - expenses];
    }
    public override void ShowProfit()
    {
        List<MonthlyLedger> allMonths = DataWarden.Instance.GetMonthlyLedgers();
        int index = allMonths.FindIndex(l => l.GetYear() == Ledger.GetYear() && l.GetMonth() == Ledger.GetMonth());
        if (index > 0)
        {
            MonthlyLedger previousLedger = allMonths[index - 1];
            Statistician previousStatistician = new MonthStatistician(previousLedger);
            UIManager.PrintProfit(CalculateProfit(), ComparePrevious(previousStatistician));
        }
        else if (index == 0) UIManager.PrintProfit(CalculateProfit(), 0);
    }
    protected override byte ComparePrevious(Statistician previousMonth)
    {
        if (previousMonth.GetType() != typeof(MonthStatistician)) return 0;
        int currentProfit = CalculateProfit()[2];
        int previousProfit = previousMonth.CalculateProfit()[2];
        if (currentProfit > 0 && currentProfit >= previousProfit) return 1;
        else if (currentProfit > 0 && currentProfit < previousProfit) return 2;
        else if (currentProfit < 0 && currentProfit >= previousProfit) return 3;
        else return 4;
    }
    public override void CompareProfits(Statistician other)
    {
        throw new NotImplementedException();
    }
}
internal class YearStatistician : Statistician
{
    private readonly List<MonthlyLedger> _months;
    public YearStatistician(List<MonthlyLedger> ledgers)
    {
        _months = ledgers;
    }

    public override void ShowData()
    {
        UIManager.Print("\n");
        foreach (var ledger in _months)
        {
            UIManager.CC(ConsoleColor.White);
            UIManager.Print($"\nHónap: ");
            UIManager.CC(ConsoleColor.DarkYellow);
            UIManager.Print($"{ledger.GetMonth()}\n");
            UIManager.CC(ConsoleColor.Green);
            foreach (var transaction in ledger.GetTransactions())
            {
                UIManager.PrintTransaction(transaction);
            }
        }
    }
    public override int[] CalculateProfit()
    {
        int totalIncome = 0, totalExpense = 0;
        foreach (var ledger in _months)
        {
            foreach (var transaction in ledger.GetTransactions())
            {
                if (transaction.isExpense) totalExpense += transaction.amount;
                else totalIncome += transaction.amount;
            }
        }
        return [totalIncome, totalExpense, totalIncome - totalExpense];
    }

    public override void ShowProfit()
    {
        int[] calc = CalculateProfit();
        UIManager.Print("\n\n");
        UIManager.CC(ConsoleColor.DarkGray);
        UIManager.Print("Éves összesítés:\n");
        UIManager.PrintProfit(calc, 0);
    }

    protected override byte ComparePrevious(Statistician previousYear)
    {
        // implementálás másik verzióban
        return 0;
    }

    public override void CompareProfits(Statistician other)
    {
        // implementálás másik verzióban
    }
}
