
using System.CommandLine;

public static class PrintOptions
{
    public static void Print(ParseResult parseResult)
    {
        IList<Option> options = parseResult.CommandResult.Command.Options;

        foreach (var option in options)
        {
            object? value = option switch
            {
                Option<int> o => parseResult.GetValue(o),
                Option<string> o => parseResult.GetValue(o),
                Option<bool> o => parseResult.GetValue(o),
                _ => "tipo não suportado"
            };

            string normalizedName = option.Name
                .ToString()
                .Replace("-", " ")
                .Trim();

            Console.WriteLine($"{normalizedName}: {value}");
        }
    }
}
