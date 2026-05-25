using GiocoV1.Modelli;
using GiocoV1.Servizi;
class Program
{
    // Cose da fare
    // Comandi nello switch
    // Le frasi di benvenuto

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
        var sezione = mappa.Sezioni.FirstOrDefault(s => s.Nome == "Foresta Tutorial") ?? mappa.Sezioni.First();
        // crea la griglia per il movimento
        var griglia = ServizioMappa.CreaGriglia(sezione);
        // movimento come sempre
        var movimento = new ServizioMovimento(griglia);

        // 2) CREAZIONE PERSONAGGIO: Richiesta nome e cordinate di spawn
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
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine($"Benvenuto {personaggio.Nome}!"); // DA FARE
            Console.WriteLine($"Scegli una classe:");
            for (int i = 0; i < servizioClassi.ClassiDisponibili.Count; i++)
                Console.WriteLine($"{i + 1}) {servizioClassi.ClassiDisponibili[i].Nome}");
            char sceltaClasseChar = Console.ReadKey(true).KeyChar;
            if (int.TryParse(sceltaClasseChar.ToString(), out int sceltaClasseInt))
            {
                if (sceltaClasseInt < 1 || sceltaClasseInt > servizioClassi.ClassiDisponibili.Count) continue;
                while (true)
                {
                    Console.Clear();
                    var classeSelezionata = servizioClassi.ClassiDisponibili[sceltaClasseInt - 1];
                    Console.WriteLine($"{classeSelezionata.Nome}");
                    Console.WriteLine($"Statistiche iniziali: {classeSelezionata.Salute} HP, {classeSelezionata.Attacco} ATK, {classeSelezionata.Difesa} DEF, {classeSelezionata.Velocita} VEL");
                    Console.WriteLine();
                    Console.Write("[E] Conferma  Indietro[Q]");
                    char decisione = Console.ReadKey(true).KeyChar;
                    if (decisione == 'e' || decisione == 'E')
                    {
                        servizioClassi.ApplicaClasse(personaggio, classeSelezionata);
                        Console.Clear();
                        Console.WriteLine($"{nome} hai scelto: {classeSelezionata.Nome}");
                        deciso = false;
                        break;
                    }
                    else if (decisione == 'q' || decisione == 'Q') break;
                    else continue;
                }
            }
            else continue;
        }
        TastoAvanti();

        // 4) MOSTRA POSIZIONE INIZIALE
        var cellaIniziale = griglia[personaggio.PosY, personaggio.PosX];
        Console.Clear();
        Console.WriteLine($"Ti trovi nella {cellaIniziale!.Nome}. {cellaIniziale.Descrizione}");
        Console.WriteLine("Dove vuoi andare? ");
        Console.WriteLine();

        // 5) LOOP DI GIOCO
        while (true)
        {
            Console.Write("[R] Menù  [W] Su  [S] Giù  [A] Sinistra  [D] Destra");
            char direzione = Console.ReadKey(true).KeyChar;
            Console.Clear();
            if (direzione == 'r' || direzione == 'R') Menu(personaggio);
            string risultato = movimento.Muovi(personaggio, direzione);
            Console.WriteLine(risultato);
            Console.WriteLine($"Posizione attuale: X={personaggio.PosX}, Y={personaggio.PosY}");
            Console.WriteLine();
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
    static void TastoAvanti()
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
            Console.Clear();
            Console.WriteLine("MENÙ");
            Console.WriteLine("[1] Inventario");
            Console.WriteLine("[2] Statistiche");
            Console.WriteLine("[3] Quest");
            Console.WriteLine("[4] Mappa");
            Console.WriteLine("[5] Comandi");
            Console.Write("[6] Indietro");
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
                        Console.WriteLine($"{oggetto.Nome}");
                    }
                    TastoIndietro();
                    continue;

            /* Possibile implementazione barre dinamicheò
                case '2':
                    int ExpPerProssimoLivello(int livello)
                    {
                        return livello * 100; // esempio semplice: 100, 200, 300...
                    }

                    Console.Clear();

                    string nome1 = personaggio.Nome;
                    string titolo1 = $"PERSONAGGIO: {nome1}";
                    int larghezza1 = 30;

                    string BordoTop1 = "╔" + new string('═', larghezza1) + "╗";
                    string BordoMid1 = "╠" + new string('═', larghezza1) + "╣";
                    string BordoBottom1 = "╚" + new string('═', larghezza1) + "╝";

                    // Centra il titolo
                    int spazi1 = (larghezza1 - titolo1.Length) / 2;
                    string RigaTitolo1 = "║" + new string(' ', spazi1) + titolo1 + new string(' ', larghezza1 - titolo1.Length - spazi1) + "║";

                    // ===== BARRE DINAMICHE =====

                    string Barra(int attuale, int massimo, int lunghezza = 20)
                    {
                        if (massimo <= 0) massimo = 1;
                        int filled = (int)((double)attuale / massimo * lunghezza);
                        if (filled > lunghezza) filled = lunghezza;
                        int empty = lunghezza - filled;
                        return "[" + new string('█', filled) + new string('░', empty) + "]";
                    }

                    string barraHP = Barra(personaggio.SaluteAttuale, personaggio.SaluteMassima);

                    // ===== CALCOLO EXP =====
                    int expNext = ExpPerProssimoLivello(personaggio.Livello);
                    string barraEXP = Barra(personaggio.Esperienza, expNext);

                    // ===== STAMPA BOX =====

                    Console.WriteLine(BordoTop1);
                    Console.WriteLine(RigaTitolo1);
                    Console.WriteLine(BordoMid1);

                    Console.WriteLine($"║ ❤️  HP:   {barraHP} {personaggio.SaluteAttuale}/{personaggio.SaluteMassima}".PadRight(larghezza1 + 1) + "║");
                    Console.WriteLine($"║ ⭐ EXP:  {barraEXP} {personaggio.Esperienza}".PadRight(larghezza1 + 1) + "║");

                    Console.WriteLine($"║ 🗡️  Attacco:    {personaggio.Attacco}".PadRight(larghezza1 + 1) + "║");
                    Console.WriteLine($"║ 🛡️  Difesa:     {personaggio.Difesa}".PadRight(larghezza1 + 1) + "║");
                    Console.WriteLine($"║ ⚡ Velocità:    {personaggio.Velocita}".PadRight(larghezza1 + 1) + "║");
                    Console.WriteLine($"║ ⬆️ Livello:     {personaggio.Livello}".PadRight(larghezza1 + 1) + "║");
                    Console.WriteLine($"║ 💰 Monete:      {personaggio.Monete}".PadRight(larghezza1 + 1) + "║");
                    Console.WriteLine($"║ 🎒 Inventario:  {personaggio.Inventario.Count}/{personaggio.CapacitaInventario}".PadRight(larghezza1 + 1) + "║");
                    Console.WriteLine($"║ 📍 Posizione:   ({personaggio.PosX}, {personaggio.PosY})".PadRight(larghezza1 + 1) + "║");

                    Console.WriteLine(BordoBottom1);

                    TastoIndietro();
                    continue;
*/
                case '2':
                    Console.Clear();

                    string nome = personaggio.Nome;
                    string titolo = $"PERSONAGGIO: {nome}";
                    int larghezza = 30; // larghezza interna del box

                    string BordoTop = "╔" + new string('═', larghezza) + "╗";
                    string BordoMid = "╠" + new string('═', larghezza) + "╣";
                    string BordoBottom = "╚" + new string('═', larghezza) + "╝";

                    // Centra il titolo
                    int spazi = (larghezza - titolo.Length) / 2;
                    string RigaTitolo = "║" + new string(' ', spazi) + titolo + new string(' ', larghezza - titolo.Length - spazi) + "║";

                    Console.WriteLine(BordoTop);
                    Console.WriteLine(RigaTitolo);
                    Console.WriteLine(BordoMid);

                    Console.WriteLine($"║ ❤️  Salute:     {personaggio.SaluteAttuale}/{personaggio.SaluteMassima}".PadRight(larghezza + 1) + "║");
                    Console.WriteLine($"║ 🗡️  Attacco:    {personaggio.Attacco}".PadRight(larghezza + 1) + "║");
                    Console.WriteLine($"║ 🛡️  Difesa:     {personaggio.Difesa}".PadRight(larghezza + 1) + "║");
                    Console.WriteLine($"║ ⚡ Velocità:    {personaggio.Velocita}".PadRight(larghezza + 1) + "║");
                    Console.WriteLine($"║ ⭐ Esperienza:  {personaggio.Esperienza}".PadRight(larghezza + 1) + "║");
                    Console.WriteLine($"║ ⬆️  Livello:     {personaggio.Livello}".PadRight(larghezza + 1) + "║");
                    Console.WriteLine($"║ 💰 Monete:      {personaggio.Monete}".PadRight(larghezza + 1) + "║");
                    Console.WriteLine($"║ 🎒 Inventario:  {personaggio.Inventario.Count}/{personaggio.CapacitaInventario}".PadRight(larghezza + 1) + "║");
                    Console.WriteLine($"║ 📍 Posizione:   ({personaggio.PosX}, {personaggio.PosY})".PadRight(larghezza + 1) + "║");
                    Console.WriteLine(BordoBottom);
                    TastoIndietro();
                    continue;

                case '3':
                    Console.WriteLine("DA FARE");
                    TastoIndietro();
                    continue;
                case '4':
                    Console.WriteLine("DA FARE");
                    TastoIndietro();
                    continue;
                case '5':
                    Console.WriteLine("DA FARE");
                    TastoIndietro();
                    continue;
                case '6': break;
            }
            if (scelta == '6') break;
        }
    }
}