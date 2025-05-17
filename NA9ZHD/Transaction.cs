using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NA9ZHD;
/// <summary>
/// Reprezentál egy tranzakciót. Lehet bevétel vagy kiadás.
/// </summary>
public record class Transaction
{
    private DateTime date { get; set; }
    private bool isExpense { get; set; }
    private TransactionCategory transactionCategory { get; set; }
    private int amount { get; set; }
    private string description { get; set; }
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
    public DateTime GetDate() { return date; }
    public bool IsExpense() { return isExpense; }
    public TransactionCategory GetTransactionCategory() {  return transactionCategory; }
    public int GetAmount() { return amount; }
    public string GetDescription() { return description; }
}