using System;
using System.IO;

namespace Buchungsvorschlaege
{
    class Program
    {
        /** Hauptprogramm, was beim Start ausgeführt wird
         * JSON File wird zunächst eingelesen und als String (kalenderexport) abgelegt
         * kalenderexport.Value[x] (vom Typ List<>) enthält alle Datensätze des JSON Files, x ist der Index für die Listenelemente
         * 
         * Mit dem jetzigen Stand kann die Anwendung JSON Files einlesen, objektorientiert speichern und ggf. Felder in der Konsole ausgeben
         * Beispiel: kalenderexport.Value[0].Recurrence.Pattern.Type zeigt auf die Property Recurrence.Pattern.Type des ersten JSON Datensatz/Kalendereintrags
         * Mit Schleifen kann man durch die Datensätze iterieren
         * Auf dieser Basis wird die Anwendung aufgebaut
         * 
         * Mit JSON Datensätzen kann man grundsätzlich nicht viel machen, sie müssen in einer Anwendung verarbeitet werden,
         * in unserer Anwendung ist es objektorientiert, d.h. geschachtelte Objekte die JSON Felder repräsentieren
         * 
         */
        static void Main(string[] args)
        {
            string jsonString = File.ReadAllText(@"..\..\..\json1.json");
            var kalenderexport = Kalenderexport.FromJson(jsonString);

            foreach (var el in kalenderexport.Value)
            {
                Console.WriteLine(el.ToString()); //Ausgabe der ToString Methode, für jedes Element
            }
            Console.WriteLine(kalenderexport.Value[0].Recurrence.Pattern.Type); //Beipiel für ein bestimmtes Feld
            Console.ReadKey();
        }
    }
}
    
