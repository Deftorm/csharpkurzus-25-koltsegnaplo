using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

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
    MonthlyLedger ledger { get; set; }
    public MonthStatistician(MonthlyLedger ledger)
    {
        this.ledger = ledger;
    }
    public override void ShowData()
    {
        UIManager.Print("\n");
        foreach (var transaction in ledger.GetTransactions()) UIManager.PrintTransaction(transaction);
    }
    public override int[] CalculateProfit()
    {
        int kiadasOssz = 0, bevetelOssz = 0;
        foreach (var transaction in ledger.GetTransactions())
        {
            if (transaction.isExpense) kiadasOssz += transaction.amount;
            else bevetelOssz += transaction.amount;
        }
        return [bevetelOssz, kiadasOssz, bevetelOssz - kiadasOssz];
    }
    public override void ShowProfit()
    {
        List<MonthlyLedger> allMonths = DataWarden.Instance.GetMonthlyLedgers();
        int index = allMonths.FindIndex(l => l.GetYear() == ledger.GetYear() && l.GetMonth() == ledger.GetMonth());
        if (index > 0)
        {
            MonthlyLedger previousLedger = allMonths[index - 1];
            Statistician previousStatistician = new MonthStatistician(previousLedger);
            UIManager.PrintMonthProfit(CalculateProfit(), ComparePrevious(previousStatistician));
        }
        else if (index == 0) UIManager.PrintMonthProfit(CalculateProfit(), 0);
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
    public override void ShowData()
    {
        throw new NotImplementedException();
    }
    public override int[] CalculateProfit()
    {
        throw new NotImplementedException();
    }
    public override void ShowProfit()
    {
        throw new NotImplementedException();
    }
    protected override byte ComparePrevious(Statistician previousYear)
    {
        if (previousYear.GetType() != typeof(YearStatistician)) return 0;
        return 1;
    }
    public override void CompareProfits(Statistician other)
    {
        throw new NotImplementedException();
    }
}