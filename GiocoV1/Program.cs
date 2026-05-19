using GiocoV1.Modelli;
using GiocoV1.Servizi;

class Program
{
    static void Main()
    {
        Console.Clear();
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
            Nome = nome
        };
        var servizioClassi = new ServizioClassi();

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
                    Console.WriteLine("[E] Conferma  Indietro[Q]");
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
    }
}