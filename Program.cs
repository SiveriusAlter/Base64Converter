public static class Program
{
    public static void Main(string[] args)
    {
        string[] commands = ["-f", "-b"];
        var message =
            $"""
             Используй Base64Converter.exe <{commands[0]}> <file> <base64file> чтобы конвертировать файл в Base64.
             Используй Base64Converter.exe <{commands[1]}> <base64file> <file> чтобы конвертировать Base64 в файл.
             """;

        if (args.Length == 0)
        {
            Console.WriteLine(message);
        }

        if (args[0] == commands[0])
        {
            var file = args[1];
            var base64File = args[2];
            ConvertFIleToBase64(file, base64File);
        }
        else if (args[0] == commands[1])
        {
            var file = args[2];
            var base64File = args[1];
            ConvertBase64ToFile(base64File, file);
        }
        else
        {
            Console.WriteLine(message);
        }
    }

    private static void ConvertFIleToBase64(string fileName, string base64File)
    {
        var pathBase64 = Path.Combine(Environment.CurrentDirectory, base64File);
        var pathFile = Path.Combine(Environment.CurrentDirectory, fileName);

        var fileBytes = File.ReadAllBytes(pathFile);

        var base64 = Convert.ToBase64String(fileBytes);
        File.Create(pathFile).Close();
        File.WriteAllText(pathBase64, base64);
    }

    private static void ConvertBase64ToFile(string base64File, string fileName)
    {
        var pathBase64 = Path.Combine(Environment.CurrentDirectory, base64File);
        var pathFile = Path.Combine(Environment.CurrentDirectory, fileName);

        var base64 = File.ReadAllText(pathBase64);

        var fileBytes = Convert.FromBase64String(base64);
        File.Create(pathFile).Close();
        File.WriteAllBytes(pathFile, fileBytes);
    }
}