using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NA9ZHD;
/// <summary>
/// Reprezentál egy tranzakciót. Lehet bevétel vagy kiadás.
/// </summary>
public record class Tranzaction
{
    private DateTime date { get; set; }
    private string description { get; set; }
    private bool isExpense { get; set; }
    private int amount { get; set; }
    public Tranzaction(DateTime date, string description, bool isExpense, int amount)
    {
        this.date = date;
        this.description = description;
        this.isExpense = isExpense;
        this.amount = amount;
    }
}
