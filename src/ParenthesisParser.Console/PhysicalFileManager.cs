namespace ParenthesisParser.Console;

using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Concurrent;

internal class PhysicalFileManager
{
    private static PhysicalFileProvider? _fileProvider;
    private static IChangeToken? _fileChangeToken;
    private static string _path = string.Empty;
    private static ConcurrentDictionary<string, DateTime> _files = new ConcurrentDictionary<string, DateTime>();
    private static Func<string, int>? _func;

    public PhysicalFileManager(string path, Func<string, int> func)
    {
        _func = func;
        _path = path;
        _fileProvider = new PhysicalFileProvider(path);
        WatchForFileChanges();

        Console.WriteLine("Put txt files in the src/ParenthesisParser.Console/Data folder.");
        Console.ReadLine();
    }

    private static void WatchForFileChanges()
    {
        IEnumerable<string> files = Directory.EnumerateFiles(_path, "*.txt");
        foreach (string file in files)
        {
            if (_files.TryGetValue(file, out DateTime existingTime))
            {
                _files.TryUpdate(file, File.GetLastWriteTime(file), existingTime);
            }
            else
            {
                if (File.Exists(file))
                {
                    var result = Task.Run(() => _func?.Invoke(file));
                    var fileName = file.Substring(file.LastIndexOf("/") + 1);
                    Console.WriteLine($"File with name {fileName} has output = {result.Result}");
                    _files.TryAdd(file, File.GetLastWriteTime(file));
                }
            }
        }

        _fileChangeToken = _fileProvider?.Watch("**/*.txt");
        _ = _fileChangeToken?.RegisterChangeCallback(Notify, default);
    }

    private static void Notify(object? state)
    {
        WatchForFileChanges();
    }
}
