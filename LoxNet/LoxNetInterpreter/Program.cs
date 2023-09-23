﻿namespace LoxNetInterpreter;

using System;

public class LoxNex
{
    private static Boolean hadError = false;
    
    public static void Main(string[] args)
    {
        if (args.Length > 1)
        {
            Console.WriteLine("Usage : loxnet [script]");
            Environment.Exit(64);
        }
        else if(args.Length == 1)
        {
            RunFile(args[0]);
        }
        else
        {
            RunPrompt();
        }
    }

    private static void RunFile(String path)
    {
        Console.WriteLine($"RunFile: {path} ");
        var source = File.ReadAllText(path);
        Run(source);
    }

    private static void RunPrompt()
    {
        Console.WriteLine("Run Lox command line by line .. ");

        while (true)
        {
            Console.Write("> ");
            var line = Console.ReadLine();
            if (String.IsNullOrEmpty(line))
            {
                break;
            }
            Run(line);
        }
    }
    
    private static void Run(String source)
    {
        Console.WriteLine($"Run code: {source}");
    }

    private static void Error(int line, String message)
    {
        Report(line, String.Empty, message);
    }

    private static void Report(int line, String where, String message)
    {
        Console.WriteLine($"[line {line}] Error {where}: {message}");
        hadError = true;
    }
    
}