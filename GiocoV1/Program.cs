using GiocoV1.Modelli;
using GiocoV1.Servizi;

class Program
{
    // Prima cosa da fare Comandi nello switch
    // Seconda cosa alleggerire il while ClassePersonaggio  

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
        var mappa = ServizioMappa.CaricaMappa("Mappa_principale.json");

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
        do
        {
            Console.Write("Scrivi il nome del tuo personaggio: ");
            nome = Console.ReadLine()!.Trim();
            Console.Clear();
        }
        while (string.IsNullOrEmpty(nome));

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
        TastiConversazione();

        // 4) MOSTRA POSIZIONE INIZIALE
        var cellaIniziale = griglia[personaggio.PosY, personaggio.PosX];
        Console.Clear();
        Console.WriteLine($"Benvenuto {personaggio.Nome}!");
        Console.WriteLine($"Ti trovi nella {cellaIniziale!.Nome}. {cellaIniziale.Descrizione}");
        Console.WriteLine();
        Console.WriteLine("Comandi: [W] = Su  [S] = Giù  [A] = Sinistra  [D] = Destra");
        Console.WriteLine("----------------------------------------------");

        // 5) LOOP DI GIOCO
        while (true)
        {
            Console.WriteLine("Dove vuoi andare? ");
            Console.WriteLine();
            Console.Write("[R] Menù  [W] = Su  [S] = Giù  [A] = Sinistra  [D] = Destra");
            char direzione = Console.ReadKey(true).KeyChar;
            Console.Clear();
            if (direzione == 'r' || direzione == 'R')
            {
                // stampa menu
                continue;
            }

            string risultato = movimento.Muovi(personaggio, direzione);
            Console.WriteLine(risultato);

            Console.WriteLine($"Posizione attuale: X={personaggio.PosX}, Y={personaggio.PosY}");
            Console.WriteLine();
        }
    }

    static void TastiConversazione()
    {
        Console.WriteLine();
        Console.Write("[E] Avanti");
        while (true)
        {
            char e = Console.ReadKey(true).KeyChar;
            if (e == 'E' || e == 'e') break;
        }
    }
    static void Menu(Personaggio personaggio)
    {
        while (true)
        {
            Console.WriteLine("MENÙ");
            Console.WriteLine("[1] Inventario");
            Console.WriteLine("[2] Statistiche");
            Console.WriteLine("[3] Quest");
            Console.WriteLine("[4] Mappa");
            Console.WriteLine("[5] Comandi");
            Console.WriteLine("[6] Indietro");
            char scelta = Console.ReadKey(true).KeyChar;
            switch (scelta)
            {
                case '1':
                    if (personaggio.Inventario.Count == 0)
                    {
                        Console.WriteLine("L'inventario è vuoto");
                        break;
                    }
                    foreach (var oggetto in personaggio.Inventario)
                    {
                        Console.WriteLine($"{oggetto.Nome}");
                    }
                    Console.WriteLine("Premi un tasto per tornare al menu...");
                    Console.ReadKey(true);
                    Console.Clear();
                    break;
                case '2':
                    Console.WriteLine($"{personaggio.SaluteAttuale}/{personaggio.SaluteMassima} HP");
                    Console.WriteLine($"{personaggio.Attacco} ATK");
                    Console.WriteLine($"{personaggio.Difesa} DEF");
                    Console.WriteLine($"{personaggio.Velocita} VEL");
                    Console.WriteLine($"{personaggio.Esperienza} EXP");
                    Console.WriteLine($"{personaggio.Livello} Lv");
                    break;
                case '3':
                    Console.WriteLine("DA FARE");
                    Console.WriteLine("Premi un tasto per tornare al menu...");
                    Console.ReadKey(true);
                    Console.Clear();
                    break;
                case '4':
                    Console.WriteLine("DA FARE");
                    Console.WriteLine("Premi un tasto per tornare al menu...");
                    Console.ReadKey(true);
                    Console.Clear();
                    break;
                case '5':
                    Console.WriteLine("DA FARE");
                    Console.WriteLine("Premi un tasto per tornare al menu...");
                    Console.ReadKey(true);
                    Console.Clear();
                    break;
            }
        }
    }
}