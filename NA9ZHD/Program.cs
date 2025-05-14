using NA9ZHD;

internal class Program
{
    private static void Main(string[] args)
    {
        DataWarden.Instance().CheckDataFolderExistance();
    }
}