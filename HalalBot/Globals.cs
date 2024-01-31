
namespace HalalBot;

static class Globals
{
    private static string NasheedsPath;
    public static List<string> Nasheeds { get; private set; }

    public static void Init(string nasheedPath)
    {
        NasheedsPath = nasheedPath;
        Nasheeds = GetNasheeds(nasheedPath);
        Console.WriteLine($"{Nasheeds.Count} nasheed loaded");
        Console.WriteLine("initialized globals");
    }

    private static List<string> GetNasheeds(string path)
    {
        // return Directory.GetFiles(path, "*.mp3");
        DirectoryInfo directory = new(path); //Assuming Test is your Folder

        FileInfo[] nasheedsInfo = directory.GetFiles("*.mp3"); //Getting Text files
        List<string> nasheedsNames = new();

        foreach (FileInfo file in nasheedsInfo)
        {
            nasheedsNames.Add(file.Name);
        }
        return nasheedsNames;
    }

    public static string GetRandomNasheed()
    {
        Random r = new();
        return NasheedsPath + "\\" + Nasheeds[r.Next(0, Nasheeds.Count - 1)];
    }
}
