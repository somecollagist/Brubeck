using System;
using System.Collections.Generic;
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
                foreach(string cmd in code)
                {
                    string push = "";
                    if(cmd[0] == ':')
                    {
                        //named label
                    }
                    else
                    {
                    }
                }
            }
            Console.WriteLine("Assembly complete!");
        }

        private static Dictionary<string, string> CommandOpcodePairs = new()
        {
            { "ADD", "AA" },
            { "SUB", "AE" },
            { "MUL", "AI" },
            { "DIV", "AO" },
            { "MOD",        "AU" },

            { "NOT",        "EA" },
            { "AND",        "EE" },
            { "OR",         "EI" },
            { "XOR",        "EO" },

            { "MOV",        "IA" },
            { "PUSH",       "IE" },
            { "PUSHALL",    "II" },
            { "POP",        "IO" },
            { "POPALL",     "IU" },

            { "CMP", "OA"},
            { "LABEL", "OE" },
            { "SUBR", "OI" },

            { "LSHIFT", "OO" },
            { "RSHIFT", "OU" },

            { "INC", "UA" },
            { "DEC", "UE" },
            { "CALL", "UI" },
            { "INT", "UO" },
            { "HALT", "UU"}
        };
    }
}
