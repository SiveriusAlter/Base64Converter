public static class Program
{
    private const string CommandToBase64 = "-f";
    private const string CommandFromBase64 = "-b";

    private const string HelpMessage = $"""
                                        Используйте:
                                           Base64Converter.exe <{CommandToBase64}> <исходный_файл> <base64_файл> - конвертировать файл в Base64.
                                           Используй Base64Converter.exe <{CommandFromBase64}> <base64_файл> <выходной_файл> - конвертировать Base64 в файл.
                                        """;

    public static int Main(string[] args)
    {
        if (args.Length == 0)
        {
            Console.WriteLine("Не указаны аргументы.");
            Console.WriteLine(HelpMessage);
            return 1;
        }

        if (args.Length != 3)
        {
            Console.WriteLine("Неверное количество аргументов");
            Console.WriteLine(HelpMessage);
            return 1;
        }

        var command = args[0];
        var inputFile = args[1];
        var outputFile = args[2];

        if (command == CommandToBase64)
        {
            ConvertFileToBase64(inputFile, outputFile);
        }
        else if (command == CommandFromBase64)
        {
            ConvertBase64ToFile(inputFile, outputFile);
        }
        else
        {
            Console.WriteLine($"Неизвестная команда: {command}");
            Console.WriteLine(HelpMessage);
            return 1;
        }

        return 0;
    }

    private static void ConvertFileToBase64(string inputFileName, string outputFileName)
    {
        var inputPath = Path.Combine(Environment.CurrentDirectory, inputFileName);
        var outputPath = Path.Combine(Environment.CurrentDirectory, outputFileName);

        if (!File.Exists(inputPath))
        {
            throw new FileNotFoundException($"Файл не найден: {inputPath}");
        }

        var fileBytes = File.ReadAllBytes(inputPath);

        var base64 = Convert.ToBase64String(fileBytes);

        File.WriteAllText(outputPath, base64);
    }

    private static void ConvertBase64ToFile(string inputFileName, string outputFileName)
    {
        var inputPath = Path.Combine(Environment.CurrentDirectory, inputFileName);
        var outputPath = Path.Combine(Environment.CurrentDirectory, outputFileName);

        if (!File.Exists(inputPath))
        {
            throw new FileNotFoundException($"Файл не найден: {inputPath}");
        }


        var base64 = File.ReadAllText(inputPath);

        var fileBytes = Convert.FromBase64String(base64);

        File.WriteAllBytes(outputPath, fileBytes);
    }
}