using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Apostasy.AST;
using Apostasy.Interpret;

namespace Apostasy.SampleApp
{
    class Program
    {
        private static string[] _input;
        private static List<Token> _tokens;
        private static List<Token> _invalidTokens;
        private static Lexer _lex;
        private static Parser _parser;
        private static Interpreter _interpreter;

        public static string VersionNumber = "v.0.0.1";

        static void Main(string[] args)
        {
            _tokens = new List<Token>();
            _invalidTokens = new List<Token>();
            Console.WriteLine("Apostasy " + VersionNumber);
            Console.WriteLine("By Kennuckles\n\n");
            Console.WriteLine("Press 1 for reading a demo file, 2 for terminal, or 0 to exit the application.\nDefault: Demo File Example\n\nInput: ");

            var key = Console.ReadKey();

            while (key.Key is not ConsoleKey.D0 or ConsoleKey.NumPad0)
            {
                if (key.Key is ConsoleKey.D1 or ConsoleKey.NumPad1)
                    ReadDemoFile(args);
                else if (key.Key is ConsoleKey.D2 or ConsoleKey.NumPad2)
                    Terminal();
                else
                    ReadDemoFile(args);
                
                Console.WriteLine("Press 1 for reading a demo file, 2 for terminal, or 0 to exit the application.\nDefault: Demo File Example\n\nInput: ");

                key = Console.ReadKey();
            }
        }
        
        public static void ReadDemoFile(string[] inp)
        {
            string[] input;
#if DEBUG
            Console.WriteLine("---------- DEBUG MODE ----------");
            input = new string[1] { "(rpy script path here)" };
#else            
            input = inp;
#endif

            if (input.Length == 0)
            {
                Console.WriteLine("\nHow to use:");
                Console.WriteLine("Apostasy.exe [path to .rpy file here]\n");
                return;
            }
            else if (input.Length > 1)
            {
                Console.WriteLine("\nToo many arguments!");
                Console.WriteLine("Please ensure that you have entered in a valid path to a Ren'Py script and try again...");
                return;
            }

            if (!input[0].EndsWith(".rpy"))
            {
                Console.WriteLine("\nWrong file type! Expected .rpy file ending.");
                Console.WriteLine("Please ensure that you have entered in a valid path to a Ren'Py script and try again...");
                return;
            }

            Console.WriteLine("\nReading {0}...\n", Path.GetFileName(input[0]));
            _lex = new Lexer(input[0], true);
            Stopwatch watch = new Stopwatch();
            watch.Start();

            var token = new Token(TokenType.UNKNOWN, "", 0);

            //Console.WriteLine("|-  Token   -|-\t\t\t\t\t\t Value \t\t\t\t\t\t-|- Line # -| ");
            while (token.Type != TokenType.EOF)
            {
                token = _lex.GetNextTokenImpl();
                _tokens.Add(token);
                //Console.WriteLine("| {0, -13}| {1, -177} | {2, -4} |", Enum.GetName(typeof(TokenType), token.type), token.value, token.lineNumber);
                if (token.Type == TokenType.UNKNOWN)
                {
                    _invalidTokens.Add(token);
                }
            }

            watch.Stop();
            _lex.CloseLexer();
            Console.WriteLine("\nFinished!");
            Console.WriteLine("Identified {0} tokens.", _tokens.Count);
            Console.WriteLine("There were {0} invalid identifications made...\n", _invalidTokens.Count);
            Console.WriteLine("----------Token---------|-----Value---|------Line #-----|");
            foreach (Token t in _invalidTokens)
            {
                Console.WriteLine("|       {0, -15} | 0x{1, -5}     | {2, -6}    |", Enum.GetName(typeof(TokenType), t.Type), t.Value, t.LineNumber);
            }

            Console.WriteLine("Total elapsed time: {0}s\n", watch.Elapsed.TotalSeconds);
            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }

        public static void Terminal()
        {
            Console.Clear();
            Console.WriteLine("Apostasy Console v0.0.1");
            Console.WriteLine("Type 'exit' to exit.\n");
            while (true)
            {
                try
                {
                    Console.Write("APC> ");
                    var text = Console.ReadLine();

                    if (!string.IsNullOrEmpty(text))
                    {
                        if (text.ToLower().Equals("exit"))
                        {
                            return;
                        }

                        _lex = new Lexer(text, false);
                        _parser = new Parser(_lex);
                        _interpreter = new Interpreter(_parser);
                        var result = _interpreter.Interpret();
                
                        Console.WriteLine(result);
                    }
                
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }

            }

            
        }
    }
}
