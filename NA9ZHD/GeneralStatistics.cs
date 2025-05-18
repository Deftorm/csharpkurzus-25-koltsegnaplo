using System;
namespace NA9ZHD;
/// <summary>
/// Általános statisztikát kiszámoló statikus (áú, túl sok már) osztály. LINQ-t használva
/// </summary>
public static class GeneralStatistics
{
    private static List<MonthlyLedger> ledgers => DataWarden.Instance.GetMonthlyLedgers();
    private static List<Transaction> transactions => ledgers.SelectMany(l => l.GetTransactions()).ToList();

    public static void ShowGeneralStats()
    {
        if (transactions.Count == 0)
        {
            UIManager.PrintError(-1, 1);
            return;
        }
        Console.Title = "Általános statisztikák";
        UIManager.Print($"\n--- Általános statisztikák ---\n\n");
        TotalIncome();
        TotalExpense();
        TransactionCount();
        AverageTransaction();
        AverageMonthlyIncome();
        AverageMonthlyExpense();
        MaxIncomeTransaction();
        MaxExpenseTransaction();
        MostProfitableMonth();
        MostProfitableYear();
    }

    private static void TotalIncome()
    {
        int totalIncome = transactions.Where(transaction => !transaction.isExpense).Sum(transaction => transaction.amount);
        UIManager.Print($"Összes bevétel:\t\t\t{totalIncome}\n");
    }
    /// <summary>
    /// Unit teszthez.
    /// </summary>
    /// <param name="transactions">tranzakciók lisátja</param>
    /// <returns></returns>
    public static int GetTotalIncome(List<Transaction> transactions)
    {
        return transactions.Where(t => !t.isExpense).Sum(t => t.amount);
    }

    private static void TotalExpense()
    {
        int totalExpense = transactions.Where(transaction => transaction.isExpense).Sum(transaction => transaction.amount);
        UIManager.Print($"Összes kiadás:\t\t\t{totalExpense}\n");
    }

    private static void TransactionCount()
    {
        UIManager.Print($"Összes tranzakció:\t\t{transactions.Count()}\n");
    }

    private static void AverageTransaction()
    {
        double average = transactions.Average(transaction => transaction.amount);
        UIManager.Print($"Átlagos tranzakció összeg:\t{average:F2}\n");
    }

    private static void AverageMonthlyIncome()
    {
        int totalIncome = transactions.Where(transaction => !transaction.isExpense).Sum(transaction => transaction.amount);
        double avgMonthlyIncome = ledgers.Count > 0 ? totalIncome / (double)ledgers.Count : 0;
        UIManager.Print($"Átlagos havi bevétel:\t\t{avgMonthlyIncome:F2}\n");
    }

    private static void AverageMonthlyExpense()
    {
        int totalExpense = transactions.Where(transaction => transaction.isExpense).Sum(transaction => transaction.amount);
        double avgMonthlyExpense = ledgers.Count > 0 ? totalExpense / (double)ledgers.Count : 0;
        UIManager.Print($"Átlagos havi kiadás:\t\t{avgMonthlyExpense:F2}\n");
    }

    private static void MaxIncomeTransaction()
    {
        var maxIncome = transactions.Where(transaction => !transaction.isExpense).OrderByDescending(transaction => transaction.amount).FirstOrDefault();
        UIManager.Print($"Legnagyobb bevétel:\t\t{maxIncome?.amount} ({maxIncome?.date})\n");
    }

    private static void MaxExpenseTransaction()
    {
        var maxExpense = transactions.Where(transaction => transaction.isExpense).OrderByDescending(transaction => transaction.amount).FirstOrDefault();
        UIManager.Print($"Legnagyobb kiadás:\t\t{maxExpense?.amount} ({maxExpense?.date})\n");
    }

    private static void MostProfitableMonth()
    {
        var mostProfitableMonth = ledgers
            .OrderByDescending(month =>
                month.GetTransactions().Where(transaction => !transaction.isExpense).Sum(transaction => transaction.amount) -
                month.GetTransactions().Where(transaction => transaction.isExpense).Sum(transaction => transaction.amount))
            .FirstOrDefault();

        if (mostProfitableMonth != null)
            UIManager.Print($"Legprofitálóbb hónap:\t\t{mostProfitableMonth.GetYear()} {mostProfitableMonth.GetMonth()}\n");
        else
            UIManager.Print($"Legprofitálóbb hónap:\t\tnincs adat\n");
    }

    private static void MostProfitableYear()
    {
        var mostProfitableYear = ledgers
            .GroupBy(month => month.GetYear())
            .Select(yearGroup => new
            {
                Year = yearGroup.Key,
                Profit = yearGroup.SelectMany(month => month.GetTransactions()).Where(transaction => !transaction.isExpense).Sum(transaction => transaction.amount)
                        - yearGroup.SelectMany(month => month.GetTransactions()).Where(transaction => transaction.isExpense).Sum(transaction => transaction.amount)
            })
            .OrderByDescending(y => y.Profit)
            .FirstOrDefault();

        if (mostProfitableYear != null)
            UIManager.Print($"Legprofitálóbb év:\t\t{mostProfitableYear.Year}\n");
        else
            UIManager.Print($"Legprofitálóbb év:\t\tnincs adat\n");
    }
}
