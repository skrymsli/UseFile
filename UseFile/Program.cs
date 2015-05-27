using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CommandLine;
using CommandLine.Text;

namespace UseFile
{

    class Options
    {
        [Option('t', "timeout", HelpText="Number of seconds to hold the files in use")]
        public int Timeout { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var options = new Options();
            
            if (!Parser.Default.ParseArguments(args, options))
            {
                PrintUsage(options);
                return;
            }

            var files = new List<FileStream>();
            foreach (var path in args.Where(File.Exists))
            {
                try
                {
                    files.Add(File.Open(path, FileMode.Open, FileAccess.ReadWrite, FileShare.None));
                }
                catch(Exception ex)
                {
                    Console.Error.WriteLine("Failed to use {0}.\r\n  Error: {1}", path, ex.Message);
                }
            }

            if (!files.Any())
            {
                Console.WriteLine("Specify some files to use.\r\n");
                PrintUsage(options);
                return;
            }

            if (options.Timeout > 0)
            {
                Console.WriteLine("Using {0} for {1} seconds...", PluralFile(() => files.Count), options.Timeout);
                while (options.Timeout > 0)
                {
                    Console.Write("                                \r");
                    Console.Write("{0}\r", options.Timeout--);
                    Thread.Sleep(TimeSpan.FromSeconds(1));
                }
            }
            else
            {
                Console.WriteLine("Press a key to stop using {0}...", PluralFile(()=>files.Count));
                Console.ReadKey(true);
            }
            files.ForEach(x=>x.Close());
        }

        static void PrintUsage(Options options)
        {
            var help = HelpText.AutoBuild(options);
            help.AddPreOptionsLine("\r\nUsage:\r\n\r\n  UseFile.exe <options> <file1> <file2> <file3> ...\r\n\r\nOptions:");
            Console.WriteLine(help.ToString());
        }

        static string PluralFile(Func<int> fnCount)
        {
            return Pluralize("file", "files", fnCount);
        }

        static string Pluralize(string singular, string plural, Func<int> fnCount)
        {
            var count = fnCount();
            return count == 1 ? singular : count + " " + plural;
        }
    }
}
