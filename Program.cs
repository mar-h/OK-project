using System;
using System.Diagnostics;
using System.IO;

namespace Buchungsvorschlaege
{
    class Program
    {
        static void Main(string[] args)
        {
            Kalenderexport kalender = Kalenderexport.loadJson();
            kalender.deleteAllBlocker();
            kalender.ShowAll();
            Console.ReadKey();
        }
    }
}