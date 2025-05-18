using System;

namespace NA9ZHD;
/// <summary>
/// Reprezentál egy tranzakciót. Lehet bevétel vagy kiadás.
/// </summary>
public record class Transaction
{
    // Note: Csak a szerializáció miatt publikus
    public DateTime date { get; set; }
    public bool isExpense { get; set; }
    public TransactionCategory transactionCategory { get; set; }
    public int amount { get; set; }
    public string description { get; set; }
    public Transaction(DateTime date, bool isExpense, TransactionCategory transactionCategory, int amount, string description)
    {
        this.date = date;
        this.isExpense = isExpense;
        this.transactionCategory = transactionCategory;
        this.amount = amount;
        this.description = description;
    }
    public override string ToString()
    {
        return String.Empty;
    }
}