using System.Runtime.CompilerServices;

namespace ParenthesisParser.Application;

public static class ParanthesisValidator
{
    private const int defaultResponse = -1;
    private static Dictionary<char, char> bracketsMap = new Dictionary<char, char>{
            {'{',  '}'},
            {'(',  ')'},
            {'[',  ']'},
            {'<',  '>'},
        };

    public static int ValidateStr(string input)
    {
        if (string.IsNullOrEmpty(input))
        {
            return defaultResponse;
        }

        Stack<char> openBrackets = new Stack<char>();

        for (int i = 0; i < input.Length; i++)
        {
            char bracket = input[i];
            if (bracketsMap.ContainsKey(bracket))
            {
                openBrackets.Push(bracket);
            }
            else
            {
                if (!openBrackets.Any())
                {
                    return i;
                }
                if (bracketsMap[openBrackets.Pop()] == bracket)
                {
                    continue;
                }

                return i;
            }
        }

        return defaultResponse;
    }

    public static int ValidateFile(string path)
    {
        string content = File.ReadAllText(path);

        return ValidateStr(content);
    }
}