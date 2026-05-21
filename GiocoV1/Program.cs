using GiocoV1.Modelli;
using GiocoV1.Servizi;

class Program
{
    // Cancella solo la riga del prompt, non tutto lo schermo
    // Console.Write("\r" + new string(' ', Console.WindowWidth) + "\r");
    // Non stampa il carattere scritto 
    // char e = Console.ReadKey(true).KeyChar;
    // Qualsiasi carattere va avanti
    // Console.ReadKey();

    static void Main()
    {
        Console.Clear();

        // 1) CREAZIONE MAPPA
        var mappa = ServizioMappa.CaricaMappa("mappa_principale.json");

        // scegli la sezione di spawn
        var sezione = mappa.Sezioni.FirstOrDefault(s => s.Nome == "Foresta Tutorial");

        if (sezione == null)
        {
            Console.WriteLine("ERRORE: La sezione 'Foresta Tutorial' non esiste nel JSON!");
            return;
        }


        // crea la griglia per il movimento
        var griglia = ServizioMappa.CreaGriglia(sezione);

        // movimento come sempre
        var movimento = new ServizioMovimento(griglia);

        // 2) CREAZIONE PERSONAGGIO: Richiesta nome e cordinate di spawn
        string nome = "";
        while (true)
        {
            Console.WriteLine();
            Console.Write("Srivi il nome del tuo personaggio: ");
            nome = Console.ReadLine()!.Trim();
            if (string.IsNullOrEmpty(nome))
            {
                Console.Clear();
                Console.WriteLine("Il nome deve contenere almeno un carattere");
                continue;
            }
            break;
        }

        Console.Clear();
        var personaggio = new Personaggio
        {
            Nome = nome,
            PosX = 5,
            PosY = 5
        };
        var servizioClassi = new ServizioClassi();

        // 3) SCELTA CLASSE PERSONAGGIO
        bool deciso = true;
        while (deciso)
        {
            Console.WriteLine();
            Console.WriteLine($"{nome} scegli una classe:");
            for (int i = 0; i < servizioClassi.ClassiDisponibili.Count; i++)
            {
                Console.WriteLine($"{i + 1}) {servizioClassi.ClassiDisponibili[i].Nome}");
            }

            char sceltaClasseChar = Console.ReadKey().KeyChar;
            if (int.TryParse(sceltaClasseChar.ToString(), out int sceltaClasseInt))
            {
                if (sceltaClasseInt < 1 || sceltaClasseInt > servizioClassi.ClassiDisponibili.Count)
                {
                    Console.Clear();
                    Console.WriteLine();
                    Console.WriteLine($"Il numero {sceltaClasseInt} non è nella lista");
                    continue;
                }
                Console.Clear();
                while (true)
                {
                    var classeSelezionata = servizioClassi.ClassiDisponibili[sceltaClasseInt - 1];
                    Console.WriteLine($"{classeSelezionata.Nome}");
                    Console.WriteLine($"Statistiche iniziali: {classeSelezionata.Salute} HP, {classeSelezionata.Attacco} ATK, {classeSelezionata.Difesa} DEF, {classeSelezionata.Velocita} VEL");
                    Console.WriteLine();
                    Console.Write("[E] Conferma  Indietro[Q]");
                    char decisione = Console.ReadKey().KeyChar;

                    if (decisione == 'e' || decisione == 'E')
                    {
                        servizioClassi.ApplicaClasse(personaggio, classeSelezionata);
                        Console.Clear();
                        Console.WriteLine($"{nome} hai scelto: {classeSelezionata.Nome}");
                        Console.WriteLine($"Le tue statistiche iniziali sono: {personaggio.SaluteMassima} HP, {personaggio.Attacco} ATK, {personaggio.Difesa} DEF, {personaggio.Velocita} VEL");
                        deciso = false;
                        break;
                    }
                    else if (decisione == 'q' || decisione == 'Q')
                    {
                        Console.Clear();
                        break;
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine();
                        Console.WriteLine($"Il carattere '{decisione}' non è valido");
                        continue;
                    }
                }
            }
            else
            {
                Console.Clear();
                Console.WriteLine();
                Console.WriteLine($"Il carattere '{sceltaClasseChar}' non è nella lista");
                continue;
            }
        }
        TastoConversazione();

        // 4) MOSTRA POSIZIONE INIZIALE
        var cellaIniziale = griglia[personaggio.PosY, personaggio.PosX];
        Console.Clear();
        Console.WriteLine($"Benvenuto {personaggio.Nome}!");
        Console.WriteLine($"Ti trovi nella {cellaIniziale!.Nome}. {cellaIniziale.Descrizione}");
        Console.WriteLine();
        Console.WriteLine("Comandi: W = su, S = giù, A = sinistra, D = destra");
        Console.WriteLine("----------------------------------------------");

        // 5) LOOP DI GIOCO
        while (true)
        {
            Console.Write("Dove vuoi andare? ");
            string direzione = Console.ReadLine() ?? "";
            Console.Clear();

            string risultato = movimento.Muovi(personaggio, direzione);
            Console.WriteLine(risultato);

            Console.WriteLine($"Posizione attuale: X={personaggio.PosX}, Y={personaggio.PosY}");
            Console.WriteLine();
        }
    }

    static void TastoConversazione()
    {
        Console.WriteLine();
        Console.Write("[E] Avanti");

        while (true)
        {
            char e = Console.ReadKey(true).KeyChar;
            if (e == 'E' || e == 'e') break;
        }
    }
}