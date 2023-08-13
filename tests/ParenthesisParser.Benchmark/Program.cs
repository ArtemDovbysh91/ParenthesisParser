using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using ParenthesisParser.Application;
using ParenthesisParser.Application.FS;

public class Program
{
    public static void Main(string[] args)
    {
        BenchmarkRunner.Run<Benchy>();
    }

    [MemoryDiagnoser]
    public class Benchy
    {
        private string input = "([{{}}[[[()]]][[]][]][[[]()]<<>>[]][[][][]](<[]>{{}})[{[()]}[{}]<>()](()[{<>{}}]{}<>){[[]]()<{}>}[<}[<(){}<>><>{}[]][(<<()>>[<>]){[]{}}]<(<>()){[]}(<>)><(<><>{})>[(<>[]){<()>{}}{<>}]([<>{}]{()})<{<>}>{})";

        [Benchmark]
        public void ParanthesisValidatorBenchmark()
        {
            ParanthesisValidator.ValidateStr(input);
        }             

        [Benchmark]
        public void ParanthesisValidatorFSBenchmark()
        {
            FSValidator.validate(input);
        }

        [Benchmark]
        public void ParanthesisValidatorFSPipeBenchmark()
        {
            FSValidator.validatePipe(input);
        }

        [Benchmark]
        public void ParanthesisValidatorFSWithHeadAndTailBenchmark()
        {
            FSValidator.validateWithHeadAndTail(input);
        }
    }
}