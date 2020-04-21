using System;

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
         * in unserer Anwendung ist es objektorientiert strukturiert, d.h. geschachtelte Objekte die JSON Felder repräsentieren
         * 
         */

        static void Main(string[] args)
        {
            Kalenderexport kalender = Kalenderexport.loadJson();
            kalender.ShowAll();
        }
    }
}
    
