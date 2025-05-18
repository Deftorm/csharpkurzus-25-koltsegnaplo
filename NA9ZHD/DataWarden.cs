using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace NA9ZHD;
/// <summary>
/// Adatokat tároló Singleton osztály.... igen, singleton.... igen, a létrehozott objektumok tárolása futásidő alatt... igen, rossz, de naaa :(
/// </summary>
public class DataWarden
{
    private static readonly object padlock = new object(); //hogy szálbiztos legyen... nem mintha lenne itt async
    private static DataWarden? instance;

    private readonly string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\KOLTSEGNAPLO";
    private List<MonthlyLedger> months = new List<MonthlyLedger>();

    private DataWarden() { }

    public static DataWarden Instance
    {
        get
        {
            lock (padlock)
            {
                if (instance == null) instance = new DataWarden(); 
                return instance;
            }
        }
    }
    public void CheckDataFolderExistance()
    {
        if (!Directory.Exists(folderPath)) Directory.CreateDirectory(folderPath);
    }
    /// <summary>
    /// Új hónap beszúrása a tárolóba.
    /// </summary>
    /// <param name="month">Amit be kell szúrni hónap</param>
    public void StoreMonth(MonthlyLedger month) { months.Add(month); }
    public void LoadAllMonthsFromFile() { FileManager.ReadAllMonthsFromFilesJSON(); }
    public void SaveAllMonths()
    {
        foreach (MonthlyLedger month in months) FileManager.WriteMonthToFileJSON(month);
    }
    public List<MonthlyLedger> GetMonthlyLedgers() { return months; }
    public void UpdateMonthlyLedger(int index, MonthlyLedger month)
    {
        if (index >= 0 && index < months.Count) months[index] = month;
        else throw new IndexOutOfRangeException();
    }
    public string GetFolderPath() { return folderPath; }
}

public enum Months
{
    JANUÁR = 1,
    FEBRUÁR = 2,
    MÁRCIUS = 3,
    ÁPRILIS = 4,
    MÁJUS = 5,
    JÚNIUS = 6,
    JÚLIUS = 7,
    AUGUSZTUS = 8,
    SZEPTEMBER = 9,
    OKTÓBER = 10,
    NOVEMBER = 11,
    DECEMBER = 12
}

public enum TransactionCategory
{
    // Alapkiadások
    Food = 1,               // élelmiszer, bolt
    Dining = 2,             // étterem, kávézó
    Transport = 3,          // tömegközlekedés, taxi, üzemanyag
    Housing = 4,            // albérlet, rezsi
    Utilities = 5,          // áram, víz, internet
    Health = 6,             // gyógyszer, orvos, biztosítás
    Education = 7,          // tankönyv, kurzus, tandíj
    // Személyes kiadások
    Entertainment = 8,      // mozi, streaming, játék
    Shopping = 9,           // ruha, elektronika, kütyük
    Travel = 10,             // utazás, szállás
    Gifts = 11,              // ajándék, adomány
    Fitness = 12,            // edzőterem, sportfelszerelés
    Subscriptions = 13,      // havi előfizetések (Netflix, Spotify stb.)
    // Bevétel típusok
    Salary = 14,             // fizetés
    Freelance = 15,          // másodállás, projektmunkák
    Scholarship = 16,        // ösztöndíj
    Support = 17,            // szülői vagy állami támogatás
    Refund = 18,             // visszatérítés
    // Egyéb
    Other = 19,
}
