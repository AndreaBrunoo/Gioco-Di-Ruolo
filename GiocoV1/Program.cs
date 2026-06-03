using GiocoV1.Modelli;
using GiocoV1.Servizi;
using GiocoV1.Configurazioni;
using GiocoV1.Dtos;

class Program
{
    // Cose da fare

    // FARE IL SERVIZIO INCONTRO BOSS
    // fARE IN MODO CHE IL PERSONAGGIO IMPOSTI LA SUA PRIMA MOSSA TRAMITE TUTORIAL PER ORA è AUTOMATICO
    // AGGIUNGERE L'OPZIONE INVENTARIO DURANTE IL COMBATTIMENTO
    // AGGIUNGERE OGGETTI OFFENSIVI TIPO (COLTELLO DA LANCIO O SHURIKEN...)
    // AGGIUNGERE UN JSON PER GLI OGGETTI METTERNE UN PAIO E TESTARE

    // Appunti

    // Cancella solo la riga del prompt, non tutto lo schermo
    // Console.Write("\r" + new string(' ', Console.WindowWidth) + "\r");

    static void Main()
    {
        while (true)
        {
            var servizioSalvataggio = new ServizioSalvataggio();
            StatoGioco? stato = null;

            // ============================
            //       MENU INIZIALE
            // ============================
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== MENU INIZIALE ===");
                Console.WriteLine("[1] Nuova Partita");
                Console.WriteLine("[2] Carica Partita");
                Console.Write("[3] Chiudi");

                char scelta = Console.ReadKey(true).KeyChar;
                Console.Clear();

                if (scelta == '1')
                {
                    Console.Clear();
                    Console.Write("Iniziare nuova partita? ");
                    bool decisioneInizio = TastiSiENo();
                    if (decisioneInizio)
                    {
                        stato = NuovaPartita();
                        break;
                    }
                    else continue;
                }
                else if (scelta == '2')
                {
                    stato = MenuCaricamento(servizioSalvataggio);
                    if (stato != null) break;
                }
                else if (scelta == '3') return;
            }
            AvviaGioco(stato);
        }
    }

    // ============================
    //       NUOVA PARTITA
    // ============================
    static StatoGioco NuovaPartita()
    {
        Console.Clear();

        // 1) CREAZIONE MAPPA
        var mappa = ServizioMappa.CaricaMappa("Mappa_principale.json");
        var sezione = mappa.Sezioni.FirstOrDefault(s => s.Nome == "Foresta Tutorial") ?? mappa.Sezioni.First();
        var griglia = ServizioMappa.CreaGriglia(sezione);
        var movimento = new ServizioMovimento(griglia);

        // 2) CREAZIONE PERSONAGGIO
        string nome;
        do
        {
            Console.Write("Scrivi il nome del tuo personaggio: ");
            nome = Console.ReadLine()!.Trim();
            if (!string.IsNullOrEmpty(nome))
                nome = char.ToUpper(nome[0]) + nome.Substring(1).ToLower();
            Console.Clear();
        }
        while (string.IsNullOrEmpty(nome));

        var personaggio = new Personaggio
        {
            Nome = nome,
            PosX = 5,
            PosY = 5,
            Mosse = new List<Mossa>
            {
                new Mossa
                {
                    Nome = "Fendente",
                    PotenzaBase = 10,
                    PrecisioneBase = 100,
                    ProbabilitaCritico = 10
                }
            }
        };

        // Equipaggia automaticamente la mossa nello slot 0
        personaggio.Equipaggiamenti.MosseEquipaggiate[0] = personaggio.Mosse[0];

        SchermataBenvenuto(personaggio);
        var servizioClassi = new ServizioClassi();

        // 3) SCELTA CLASSE
        bool deciso = true;
        while (deciso)
        {
            Console.Clear();
            Console.WriteLine("Scegli una classe:");

            for (int i = 0; i < servizioClassi.ClassiDisponibili.Count; i++)
                Console.WriteLine($"{i + 1}) {servizioClassi.ClassiDisponibili[i].Nome}");

            char sceltaClasseChar = Console.ReadKey(true).KeyChar;

            if (int.TryParse(sceltaClasseChar.ToString(), out int sceltaClasseInt))
            {
                if (sceltaClasseInt < 1 || sceltaClasseInt > servizioClassi.ClassiDisponibili.Count)
                    continue;

                while (true)
                {
                    Console.Clear();
                    var classeSelezionata = servizioClassi.ClassiDisponibili[sceltaClasseInt - 1];

                    Console.WriteLine($"{classeSelezionata.Nome}");
                    Console.WriteLine($"Statistiche iniziali: {classeSelezionata.Salute} HP, {classeSelezionata.Attacco} ATK, {classeSelezionata.Difesa} DEF, {classeSelezionata.Velocita} VEL");
                    Console.WriteLine();
                    Console.Write("[E] Conferma  [Q] Indietro");

                    char decisione = Console.ReadKey(true).KeyChar;

                    if (decisione == 'e' || decisione == 'E')
                    {
                        servizioClassi.ApplicaClasse(personaggio, classeSelezionata);
                        Console.Clear();
                        Console.WriteLine($"{nome} hai scelto: {classeSelezionata.Nome}");
                        deciso = false;
                        break;
                    }
                    else if (decisione == 'q' || decisione == 'Q')
                        break;
                }
            }
        }
        TastoAvanti();

        // CREA LO STATO DI GIOCO
        return new StatoGioco
        {
            Personaggio = personaggio,
            AreaCorrente = sezione.Nome,
            PosizioneX = personaggio.PosX,
            PosizioneY = personaggio.PosY
        };
    }

    static void SchermataBenvenuto(Personaggio personaggio)
    {
        Console.Clear();

        string titolo = "PROJECT FRONTIER";
        int larghezza = 50;

        string bordoTop = "╔" + new string('═', larghezza) + "╗";
        string bordoMid = "╠" + new string('═', larghezza) + "╣";
        string bordoBottom = "╚" + new string('═', larghezza) + "╝";

        // Centra il titolo
        int spazi = (larghezza - titolo.Length) / 2;
        string rigaTitolo = "║" + new string(' ', spazi) + titolo + new string(' ', larghezza - titolo.Length - spazi) + "║";

        Console.WriteLine(bordoTop);
        Console.WriteLine(rigaTitolo);
        Console.WriteLine(bordoMid);

        Console.WriteLine($"║ Benvenuto, {personaggio.Nome}!".PadRight(larghezza + 1) + "║");
        Console.WriteLine($"║ Il tuo viaggio sta per iniziare...".PadRight(larghezza + 1) + "║");
        Console.WriteLine($"║".PadRight(larghezza + 1) + "║");
        Console.WriteLine($"║ • Esplora terre misteriose".PadRight(larghezza + 1) + "║");
        Console.WriteLine($"║ • Affronta creature sconosciute".PadRight(larghezza + 1) + "║");
        Console.WriteLine($"║ • Cresci, combatti, sopravvivi".PadRight(larghezza + 1) + "║");
        Console.WriteLine($"║".PadRight(larghezza + 1) + "║");
        Console.WriteLine($"║ Preparati, avventuriero...".PadRight(larghezza + 1) + "║");

        Console.WriteLine(bordoBottom);

        Console.WriteLine();
        Console.Write("[E] Continua");

        while (true)
        {
            char c = Console.ReadKey(true).KeyChar;
            if (c == 'E' || c == 'e') break;
        }
    }

    // ============================
    //       CARICAMENTO PARTITA
    // ============================
    static StatoGioco MenuCaricamento(ServizioSalvataggio salvataggio)
    {
        var files = salvataggio.ElencaSalvataggi();

        if (files.Count == 0)
        {
            Console.WriteLine("Nessun salvataggio trovato.");
            TastoIndietro();
            return null;
        }

        Console.WriteLine("=== SCEGLI UN SALVATAGGIO ===");

        for (int i = 0; i < files.Count; i++)
            Console.WriteLine($"[{i + 1}] {Path.GetFileName(files[i])}");

        Console.WriteLine("[Q] Indietro");

        while (true)
        {
            char scelta = Console.ReadKey(true).KeyChar;

            if (scelta == 'q' || scelta == 'Q')
                return null;

            if (int.TryParse(scelta.ToString(), out int fileScelto))
            {
                if (fileScelto >= 1 && fileScelto <= files.Count)
                {
                    Console.Clear();
                    return salvataggio.Carica(files[fileScelto - 1]);
                }
            }
        }
    }

    // ============================
    //       AVVIO DEL GIOCO
    // ============================
    static void AvviaGioco(StatoGioco stato)
    {
        var personaggio = stato.Personaggio;
        // 🔹 Carico la mappa dal file
        var mappa = ServizioMappa.CaricaMappa("Mappa_principale.json");
        // 🔹 Carico le mosse
        ServizioMosse.CaricaMosse("Mosse.json");
        // 🔹 Carico i nemici
        var configNemici = ServizioNemici.CaricaNemici("Nemici.json");
        // 🔹 Assegno le mosse ai nemici
        ServizioNemici.AssegnaMosse(configNemici, new ServizioMosse());
        // 🔹 Trovo la sezione corretta
        var sezione = mappa.Sezioni.First(s => s.Nome == stato.AreaCorrente);
        // 🔹 Creo la griglia per il movimento
        var griglia = ServizioMappa.CreaGriglia(sezione);
        var movimento = new ServizioMovimento(griglia);
        // 🔹 Posizione iniziale
        var cellaIniziale = griglia[personaggio.PosY, personaggio.PosX];

        Console.Clear();
        Console.WriteLine($"Ti trovi nella {cellaIniziale!.Nome}. {cellaIniziale.Descrizione}");
        Console.WriteLine();

        while (true)
        {
            Console.Write("[R] Menù  [W] Su  [S] Giù  [A] Sinistra  [D] Destra");
            char direzione = Console.ReadKey(true).KeyChar;
            Console.Clear();
            if (direzione == 'r' || direzione == 'R')
            {
                bool continua = Menu(personaggio, stato);
                if (!continua) return;
            }
            var risultato = movimento.Muovi(personaggio, direzione, sezione, configNemici);
            Console.WriteLine(risultato.Messaggio);
            Console.WriteLine();
            if (risultato.NemicoTrovato != null)
            {
                ServizioIncontri.Incontro(personaggio, risultato.NemicoTrovato);
            }
        }
    }

    // ============================
    //       MENU DI GIOCO
    // ============================
    static bool Menu(Personaggio personaggio, StatoGioco stato)
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("MENÙ");
            Console.WriteLine("[1] Inventario");
            Console.WriteLine("[2] Statistiche");
            Console.WriteLine("[3] Mosse");
            Console.WriteLine("[4] Mappa");
            Console.WriteLine("[5] Quest");
            Console.WriteLine("[6] Salva");
            Console.WriteLine("[7] Indietro");
            Console.Write("[8] Esci");

            char scelta = Console.ReadKey(true).KeyChar;
            Console.Clear();

            switch (scelta)
            {
                case '1':
                    if (personaggio.Inventario.Count == 0)
                    {
                        Console.WriteLine("L'inventario è vuoto");
                        TastoIndietro();
                        continue;
                    }
                    foreach (var oggetto in personaggio.Inventario)
                    {
                        Console.WriteLine($"{oggetto.Quantita}X {oggetto.Nome}");
                        Console.Write($"ATK +{oggetto.BonusAttacco} ");
                        Console.Write($"DIF +{oggetto.BonusDifesa} ");
                        Console.Write($"VEL +{oggetto.BonusVelocita} ");
                        Console.Write($"HP +{oggetto.BonusSalute} ");
                        Console.Write($"{oggetto.Tipologia}");
                    }
                    TastoIndietro();
                    continue;
                case '2':
                    MostraStatistiche(personaggio);
                    continue;
                case '3':
                    if (personaggio.Mosse.Count == 0)
                    {
                        Console.WriteLine("Non hai mosse disponibili");
                        TastoIndietro();
                        continue;
                    }
                    foreach (var mossa in personaggio.Mosse)
                    {
                        Console.WriteLine($"{mossa.Nome}");
                        Console.Write($"Potenza: {mossa.PotenzaBase} ");
                        Console.Write($"Precisione: {mossa.PrecisioneBase} ");
                        Console.Write($"Prob. Crit.: {mossa.ProbabilitaCritico} ");
                    }
                    TastoIndietro();
                    continue;
                case '4':
                case '5':
                    Console.WriteLine("DA FARE");
                    TastoIndietro();
                    continue;
                case '6':
                    var servizioSalvataggio = new ServizioSalvataggio();
                    string percorso = servizioSalvataggio.SalvaNuovoSlot(stato);
                    Console.WriteLine("Partita salvata con successo!");
                    Console.WriteLine($"File: {Path.GetFileName(percorso)}");
                    Console.WriteLine($"Salvato il: {DateTime.Now:dd/MM/yyyy HH:mm}");
                    TastoIndietro();
                    continue;
                case '7': return true;
                case '8': return false;
            }
        }
    }

    static void MostraStatistiche(Personaggio personaggio)
    {
        int larghezza = 30;
        string titolo = $"PERSONAGGIO: {personaggio.Nome}";

        string BordoTop = "╔" + new string('═', larghezza) + "╗";
        string BordoMid = "╠" + new string('═', larghezza) + "╣";
        string BordoBottom = "╚" + new string('═', larghezza) + "╝";

        int spazi = (larghezza - titolo.Length) / 2;
        string RigaTitolo = "║" + new string(' ', spazi) + titolo + new string(' ', larghezza - titolo.Length - spazi) + "║";

        Console.WriteLine(BordoTop);
        Console.WriteLine(RigaTitolo);
        Console.WriteLine(BordoMid);

        string salute = $"{personaggio.SaluteAttuale}/{personaggio.SaluteMassima}";
        Console.WriteLine($"{"║",-2}{"Salute",-18}{salute,-11}║");
        Console.WriteLine($"{"║",-2}{"Attacco",-18}{personaggio.Attacco,-11}║");
        Console.WriteLine($"{"║",-2}{"Difesa",-18}{personaggio.Difesa,-11}║");
        Console.WriteLine($"{"║",-2}{"Velocità",-18}{personaggio.Velocita,-11}║");
        Console.WriteLine($"{"║",-2}{"Esperienza",-18}{personaggio.Esperienza,-11}║");
        Console.WriteLine($"{"║",-2}{"Livello",-18}{personaggio.Livello,-11}║");
        Console.WriteLine($"{"║",-2}{"Monete",-18}{personaggio.Monete,-11}║");
        // Console.WriteLine($"{"║",-2}{"Arma",-18}{Equipaggiamento.Arma.TipologiaArma,-11}║");

        Console.WriteLine(BordoBottom);
        TastoIndietro();
    }

    // ============================
    //       UTILITY
    // ============================
    static bool TastiSiENo()
    {
        Console.WriteLine();
        Console.Write("[E] Si  [Q] No");
        while (true)
        {
            char t = Console.ReadKey(true).KeyChar;
            if (t == 'E' || t == 'e')
                return true;  // avanti
            if (t == 'Q' || t == 'q')
                return false; // indietro
        }
    }
    static void TastoIndietro()
    {
        Console.WriteLine();
        Console.Write("[Q] Indietro");
        while (true)
        {
            char e = Console.ReadKey(true).KeyChar;
            if (e == 'Q' || e == 'q') break;
        }
    }
    public static void TastoAvanti()
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