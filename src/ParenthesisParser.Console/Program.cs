using ParenthesisParser.Application;
using ParenthesisParser.Console;
using System.Reflection;

string _path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? string.Empty, @"Data");
var physucalWatcher = new PhysicalFileManager(_path, ParanthesisValidator.ValidateFile);