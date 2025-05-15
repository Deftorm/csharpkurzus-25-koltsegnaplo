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
    private List<Tranzaction> tranzactions { get; set; }
    public MonthlyLedger(ushort year, Months month)
    {
        this.year = year;
        this.month = month;
        tranzactions = new List<Tranzaction>();
    }
    public MonthlyLedger(ushort year, Months month, List<Tranzaction> tranzactions) : this(year, month)
    {
        this.tranzactions = tranzactions;
    }
}
