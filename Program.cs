using System;

namespace Buchungsvorschlaege
{
    class Program
    {
        static void Main(string[] args)
        {
            Kalenderexport kalender = Kalenderexport.loadJson();
            kalender.ShowAll();
        }
    }
}
    
