global using ParenthesisParser.Application;
global using ParenthesisParser.Application.FS;

namespace ParenthesisParser.UnitTests;

[TestFixture]
public class ParanthesisValidatorTests
{
    private readonly string emptyString = string.Empty;

    [Test]
    [TestCase("[<<>]", 4)]
    [TestCase("({}<>])", 5)]
    [TestCase("(<{()}{}{}><<>{[]}>[<<>><{}>{}>[]<>][]{}[])()", 30)]
    [TestCase("([{{}}[[[()]]][[]][]][[[]()]<<>>[]][[][][]](<[]>{{}})[{[()]}[{}]<>()](()[{<>{}}]{}<>){[[]]()<{}>}[<}[<(){}<>><>{}[]][(<<()>>[<>]){[]{}}]<(<>()){[]}(<>)><(<><>{})>[(<>[]){<()>{}}{<>}]([<>{}]{()})<{<>}>{})", 99)]
    [TestCase("123(", 0)]
    [TestCase("<{}>!", 4)]
    [TestCase("", -1)]
    [TestCase(null, -1)]
    [TestCase("(>", 1)]
    public void ParenthesesTest(string input, int expected)
    {
        // Arrange
        // var input = "()";
        // Act
        var result = ParanthesisValidator.ValidateStr(input);

        // Assert
        result.Should().Be(expected);
    }

    [Test]
    public void ParenthesesStringEmptyTest()
    {
        // Arrange
        var input = string.Empty;
        // Act
        var result = ParanthesisValidator.ValidateStr(input);

        // Assert
        result.Should().Be(-1);
    }

    [Test]
    [TestCase("[<<>]", 4)]
    [TestCase("({}<>])", 5)]
    [TestCase("(<{()}{}{}><<>{[]}>[<<>><{}>{}>[]<>][]{}[])()", 30)]
    [TestCase("([{{}}[[[()]]][[]][]][[[]()]<<>>[]][[][][]](<[]>{{}})[{[()]}[{}]<>()](()[{<>{}}]{}<>){[[]]()<{}>}[<}[<(){}<>><>{}[]][(<<()>>[<>]){[]{}}]<(<>()){[]}(<>)><(<><>{})>[(<>[]){<()>{}}{<>}]([<>{}]{()})<{<>}>{})", 99)]
    [TestCase("123(", 0)]
    [TestCase("<{}>!", 4)]
    [TestCase("", -1)]
    [TestCase(null, -1)]
    [TestCase("(>", 1)]
    public async Task ParanthesisValidatorLargeFilesTest(string input, int expected)
    {
        // Arrange
        string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? string.Empty, @"..\..\..\Data\input.txt");
        using (StreamWriter writer = new StreamWriter(path, false))
        {
            writer.Write(input);
        }

        // Act
        var result = await ParanthesisValidatorLargeFiles.ValidateAsync(path);

        // Assert
        result.Should().Be(expected);
    }

    [Test]
    [TestCase("[<<>]", 4)]
    [TestCase("({}<>])", 5)]
    [TestCase("(<{()}{}{}><<>{[]}>[<<>><{}>{}>[]<>][]{}[])()", 30)]
    [TestCase("([{{}}[[[()]]][[]][]][[[]()]<<>>[]][[][][]](<[]>{{}})[{[()]}[{}]<>()](()[{<>{}}]{}<>){[[]]()<{}>}[<}[<(){}<>><>{}[]][(<<()>>[<>]){[]{}}]<(<>()){[]}(<>)><(<><>{})>[(<>[]){<()>{}}{<>}]([<>{}]{()})<{<>}>{})", 99)]
    [TestCase("123(", 0)]
    [TestCase("<{}>!", 4)]
    [TestCase("", -1)]
    [TestCase(null, -1)]
    [TestCase("(>", 1)]
    public void ParanthesisValidatorFSTest(string input, int expected)
    {
        // Arrange

        // Act
        var result = FSValidator.validate(input);

        // Assert
        result.Should().Be(expected);
    }

    [Test]
    [TestCase("[<<>]", 4)]
    [TestCase("({}<>])", 5)]
    [TestCase("(<{()}{}{}><<>{[]}>[<<>><{}>{}>[]<>][]{}[])()", 30)]
    [TestCase("([{{}}[[[()]]][[]][]][[[]()]<<>>[]][[][][]](<[]>{{}})[{[()]}[{}]<>()](()[{<>{}}]{}<>){[[]]()<{}>}[<}[<(){}<>><>{}[]][(<<()>>[<>]){[]{}}]<(<>()){[]}(<>)><(<><>{})>[(<>[]){<()>{}}{<>}]([<>{}]{()})<{<>}>{})", 99)]
    [TestCase("123(", 0)]
    [TestCase("<{}>!", 4)]
    [TestCase("(>", 1)]
    public void ParanthesisValidatorFSPipeTest(string input, int expected)
    {
        // Arrange

        // Act
        var result = FSValidator.validatePipe(input);

        // Assert
        result.Should().Be(expected);
    }

    [Test]
    [TestCase("[<<>]", 4)]
    [TestCase("({}<>])", 5)]
    [TestCase("(<{()}{}{}><<>{[]}>[<<>><{}>{}>[]<>][]{}[])()", 30)]
    [TestCase("([{{}}[[[()]]][[]][]][[[]()]<<>>[]][[][][]](<[]>{{}})[{[()]}[{}]<>()](()[{<>{}}]{}<>){[[]]()<{}>}[<}[<(){}<>><>{}[]][(<<()>>[<>]){[]{}}]<(<>()){[]}(<>)><(<><>{})>[(<>[]){<()>{}}{<>}]([<>{}]{()})<{<>}>{})", 99)]
    [TestCase("123(", 0)]
    [TestCase("<{}>!", 4)]
    [TestCase("(>", 1)]
    public void ParanthesisValidatorFSWithHeadAndTailTest(string input, int expected)
    {
        // Arrange

        // Act
        var result = FSValidator.validateWithHeadAndTail(input);

        // Assert
        result.Should().Be(expected);
    }
}