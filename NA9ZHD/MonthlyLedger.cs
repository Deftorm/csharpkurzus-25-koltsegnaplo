using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NA9ZHD;
/// <summary>
/// Reprezentál egy hónapot mely tartalmazza az ahhoz tartozó tranzakciókat.
/// </summary>
public class MonthlyLedger
{
    private ushort year { get; set; }
    private Months month { get; set; }
    private List<Transaction> transactions { get; set; }
    public MonthlyLedger(ushort year, Months month)
    {
        this.year = year;
        this.month = month;
        transactions = new List<Transaction>();
    }
    public MonthlyLedger(ushort year, Months month, List<Transaction> transactions) : this(year, month)
    {
        this.transactions = transactions;
    }
    public Months GetMonth() { return month; }
    public ushort GetYear() { return year; }
    public List<Transaction> GetTransactions() { return transactions; }
    public void AddTransaction(Transaction transaction) { transactions.Add(transaction);  }
}
