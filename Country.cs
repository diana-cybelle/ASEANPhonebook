using System;
using System.Collections.Generic;

namespace ASEANPhonebook;

public static class Country
{
    // Only the five countries required by the project are accepted.
    private static readonly Dictionary<string, string> Countries =
        new(StringComparer.OrdinalIgnoreCase)
        {
            ["60"] = "Malaysia",
            ["62"] = "Indonesia",
            ["63"] = "Philippines",
            ["65"] = "Singapore",
            ["66"] = "Thailand"
        };

    public static bool IsValidCode(string code)
    {
        return Countries.ContainsKey(code);
    }

    public static string GetName(string code)
    {
        return Countries.TryGetValue(code, out string? name)
            ? name
            : "Unknown country";
    }

    public static void DisplayCountryChoices()
    {
        Console.WriteLine("[1] Philippines (63)");
        Console.WriteLine("[2] Thailand (66)");
        Console.WriteLine("[3] Singapore (65)");
        Console.WriteLine("[4] Indonesia (62)");
        Console.WriteLine("[5] Malaysia (60)");
    }

    public static string GetCodeFromChoice(int choice)
    {
        return choice switch
        {
            1 => "63",
            2 => "66",
            3 => "65",
            4 => "62",
            5 => "60",
            _ => ""
        };
    }
}
