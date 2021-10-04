using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Brubeck.Assembler
{
    public class Program
    {
        public static void Assemble(string path, bool verbose = false)
        {
            string[] code;
            using (StreamReader sr = new(path))
            {
                Console.WriteLine($"Full path: {Path.GetFullPath(path)}");
                code = sr
                    .ReadToEnd()
                    .Split('\n')
                    .Select(t => t.ToUpper())
                    .ToArray();
            }

            using(StreamWriter sr = new(Path.ChangeExtension(path, "brbk5")))
            {
                if (verbose)
                {
                    foreach (string cmd in code)
                    {
                        if (cmd[0] == ':')   //LABEL
                        {

                        }
                        else
                        {
                            switch (cmd.Substring(0, cmd.IndexOf(' ')))
                            {
                                case "MOV":
                                    break;

                                case "ADD":
                                    break;

                                case "SUB":
                                    break;
                            }
                        }
                    }
                }
            }
            Console.WriteLine("Assembly complete!");
        }
    }
}
