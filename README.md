# ParenthesisParser
ParenthesisParser is a dotnet Solution that helps with parsing and validating nested parenthesis/chunks in text files. Whether you're dealing with small files or large ones, ParenthesisParser offers various validators to suit different requirements.

## How to run
To run solution, follow these speps:
1. Clone the repository to your local machine:

`git clone git@github.com:ArtemDovbysh91/ParenthesisParser.git`

2. Navigate to the project directory:
   
`cd ParenthesisParser`

3. run docker-compose:

`docker-compose up`

4. If you need a new .txt file for testing, you can place it in the src\ParenthesisParser.Console\Data folder. This folder is mapped as a Docker volume for the Parser container. After a few seconds, the console should display new lines indicating the parsing results.

# Projects
## ParenthesisValidator, C#
The ParenthesisParser.Application project includes the **ParenthesisValidator** that offers a straightforward approach to validate nested chunks. This validator can be used to verify files containing nested chunks and ensure their proper structure. This project also contains the **ParenthesisValidatorLargeFiles**, an alternative version designed to handle large files efficiently. This validator reads characters from files one by one, making it suitable for processing big files without consuming excessive memory.

## FunctionalValidators, F#
The ParenthesisParser.Application.FS project introduces three functional-based validators:

### validate
The validate method employs a functional approach similar to the ParenthesisValidator. It uses a stack and mutable variable to keep track of validation results.

### validatePipe
The validatePipe function takes advantage of a functional approach and utilizes a pipe mechanism to determine validation results using a stack.

### validateWithHeadAndTail
The validateWithHeadAndTail function uses the head :: tail technique instead of a stack. This functional approach offers an alternative method for validating nested chunks.

## Test Project
The **ParenthesisParser.Test** project contains a test suite for all the Validate methods provided by both projects. These tests ensure the correctness and reliability of the validation functionalities across various scenarios.

## Benchmark Project
The **ParenthesisParser.Benchmark** project houses benchmark tests for all validators. These benchmarks provide insights into the performance characteristics of each validation method. Below is a sample real output comparing the execution times of different methods:

|                                         Method |     Mean |     Error |    StdDev |   Median |   Gen0 | Allocated |
|----------------------------------------------- |---------:|----------:|----------:|---------:|-------:|----------:|
|                  ParanthesisValidatorBenchmark | 1.955 us | 0.0390 us | 0.0935 us | 1.963 us | 0.0153 |     104 B |
|                ParanthesisValidatorFSBenchmark | 7.580 us | 0.1511 us | 0.4334 us | 7.493 us | 0.8240 |    5184 B |
|            ParanthesisValidatorFSPipeBenchmark | 9.351 us | 0.1856 us | 0.3875 us | 9.282 us | 0.9918 |    6296 B |
| ParanthesisValidatorFSWithHeadAndTailBenchmark | 9.493 us | 0.1887 us | 0.4735 us | 9.338 us | 1.2360 |    7824 B |

```// * Legends *
  Mean      : Arithmetic mean of all measurements
  Error     : Half of 99.9% confidence interval
  StdDev    : Standard deviation of all measurements
  Median    : Value separating the higher half of all measurements (50th percentile)
  Gen0      : GC Generation 0 collects per 1000 operations
  Allocated : Allocated memory per single operation (managed only, inclusive, 1KB = 1024B)
  1 us      : 1 Microsecond (0.000001 sec)
```
