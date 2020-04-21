# ReadMe
## Installationsanleitung
- GitHub Desktop installieren und Projekt clonen
- Visual Studio 2019 Community installieren (kostenlos)
- Projekt -> NuGet Pakete verwalten -> *Newtonsoft.Json* Framework installieren (falls nicht installiert)
	- Dann sollten keine Fehler angezeigt werden und die Anwendung sollte sich starten lassen

## Code Dokumentation
Das Projekt enthält eine Abhängigkeit zum Paket *Newtonsoft.Json*, die Dateien **json1.json**, **json2.json**, **Klassen.cs** und **Program.cs**.
Das Projekt ist eine .NET Core Konsolenanwendung und ist in C# geschrieben.

Die zwei JSON-Datensätze sind exportierte Kalendereinträge von Objektkultur.

In der Datei **Klassen.cs** werden Klassen definiert, die geschachtelt aufgebaut sind und zum Schema des JSON-Datensatzes passen. Die äußere Klasse *Kalenderexport* ist gemäß Domain-Driven Design das Aggregate Root, d.h. jeglicher Zugriff auf einzelne Felder erfolgt über die Instanz dieser Klasse. Sie enthält die Klasse *Value*, die als Liste vorliegt und alle Felder des Kalendereintrags enthält. Ein Listenelement enthält die Daten von genau einem Kalendereintrag. Über Indizes lassen sich individuelle Kalendereinträge innerhalb *Value* ansprechen.

Die eckigen Klammern bei den Attributen sind Bezeichnungen für das Framework *Newtonsoft.Json*, das die Json-Datensätze für uns konvertiert.


Die Funktion *loadJson()* liest das JSON File und konvertiert es in die objektorientierte Struktur. Der angegebene Pfad ist relativ und zeigt auf das Projektverzeichnis, wenn dort das Json File liegt wird es erfolgreich eingelesen. Zurück gegeben wird ein Objekt der Klasse Kalenderexport, das eine gesamten Kalenderexport repräsentiert.

> Beispiele: 
> *kalender.Value[0].Id* zeigt auf die Id des ersten Kalendereintrags

> *kalender.Value[1].Start.DateTime* zeigt auf das Feld DateTime des zweiten Eintrags

Die Datei Program.cs enthält die Main Funktion der Anwendung. Hier können Operationen auf das Objekt vom Kalenderexport ausgeführt werden.
