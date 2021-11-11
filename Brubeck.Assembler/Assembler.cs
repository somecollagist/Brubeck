using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Brubeck.Assembler
{
    public partial class Assembler
    {
        /// <summary>
        /// Assembles assembly code from the given path.
        /// </summary>
        /// <param name="path">Path to assemble from (should be a .brbkasm file).</param>
        /// <param name="verbose">Show debug information.</param>
        public static void Run(string path, bool verbose = false)
        {
            if (!verbose) Console.SetOut(TextWriter.Null);

            try
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

                string newpath = Path.ChangeExtension(path, "brbk5");
                Console.WriteLine($"Machine code path: {newpath}");

                using (StreamWriter sw = new(newpath))
                {
                    foreach (string cmd in code)
                    {
                        Console.WriteLine($"Instruction: {cmd}");

                        Mnemonic mnn = new(cmd);

                        string push = $"{mnn.Adverb}{mnn.Opcode}";
                        push += Utils.GetRegisterAlias(int.Parse(mnn.Args[0][1..]));

                        switch(mnn.Adverb)
                        {
                            //Register
                            case 'A':
                                push += Utils.GetRegisterAlias(int.Parse(mnn.Args[1][1..]));
                                break;

                            //Memory Location
                            case 'E':
                                push += mnn.Args[1].Substring(0, 10);
                                break;

                            //Constant
                            case 'O':
                                push += mnn.Args[1];
                                break;

                            default:
                                throw new AssemblySegmentationFault();
                        }
                        Console.WriteLine($"Decoded to machine code instruction {push}");
                        sw.Write(push);
                    }
                }
            }
            catch (Exception e)
            {
                switch (e)
                {
                    case FileNotFoundException ex:
                        Console.WriteLine($"No file {ex.FileName} could be found.");
                        return;

                    case KeyNotFoundException ex:
                        string mnemonic = Regex.Match(ex.Message, @"(?<=\')[A-Z]+(?=\')").Value;
                        Console.WriteLine($"Opcode mnemonic {mnemonic} is not recognised.");
                        return;
                }
            }

            Console.SetOut(Console.Out);
            Console.WriteLine("Assembly Complete!");
        }
    }
}
