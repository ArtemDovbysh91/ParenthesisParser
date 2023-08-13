namespace ParenthesisParser.Application;

public static class ParanthesisValidatorLargeFiles
{
    private const int defaultResponse = -1;
    private static Dictionary<char, char> bracketsMap = new Dictionary<char, char>{
            {'{',  '}'},
            {'(',  ')'},
            {'[',  ']'},
            {'<',  '>'},
        };

    public static async Task<int> ValidateAsync(string path)
    {
        Stack<char> openBrackets = new Stack<char>();
        var index = -1;

        var buffer = new char[1];
        char bracket;
        using StreamReader reader = File.OpenText(path);

        do
        {
            var readIndex = await reader.ReadAsync(buffer);

            if(readIndex == 0)
            {
                return defaultResponse;
            }

            index++;
            bracket = buffer[0];
            if (bracketsMap.ContainsKey(bracket))
            {
                openBrackets.Push(bracket);
            }
            else
            {
                if (!openBrackets.Any())
                {
                    reader.Close();
                    return index;
                }
                if (bracketsMap[openBrackets.Pop()] == bracket)
                {
                    continue;
                }

                reader.Close();
                return index;
            }
        } while (!reader.EndOfStream);
        reader.Close();

        return defaultResponse;
    }
}