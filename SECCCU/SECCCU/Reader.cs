using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SECCCU
{
    public class Reader
    {
        Random  random = new Random();
        public string ScanCard()
        {
            string line = File.ReadLines("ScanInput.txt").Skip(random.Next(0, File.ReadAllLines("ScanInput.txt").Length-1)).Take(1).First();
            return line;
        }
    }
}