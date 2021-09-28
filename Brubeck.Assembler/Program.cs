using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Brubeck.Assembler
{
    class Program
    {
        public static void Assemble(string path)
        {
            using StreamReader sr = new(path);
            string[] code = sr
                .ReadToEnd()
                .Split('\n')
                .Select(t => t.ToUpper())
                .ToArray();
            foreach(string cmd in code)
            {
                Console.WriteLine(cmd);
            }
        }
    }
}
