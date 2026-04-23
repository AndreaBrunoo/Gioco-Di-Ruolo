namespace GiocoRuolo.Models
{
    /// <summary>
    /// Rappresenta un nemico incontrabile nella mappa.
    /// Contiene statistiche, attacchi, loot e valore di pericolosità.
    /// </summary>
    public class Nemico
    {
        // Nome del nemico (es. "Goblin", "Lupo", "Scheletro")
        public string Nome { get; set; }

        // Statistiche del nemico
        public Statistiche Statistiche { get; set; }

        // Attacchi che il nemico può eseguire
        public List<Attacco> Attacchi { get; set; } = new();

        // Rarità del nemico (influenza drop e difficoltà)
        public Rarita Rarita { get; set; }

        // Valore di pericolosità (usato per calcolare monete ed esperienza)
        public int Pericolosita { get; set; }

        // Loot possibile dal nemico
        public Loot Loot { get; set; }

        // Velocità di movimento/attacco (per determinare chi attacca per primo)
        public int Velocita => Statistiche?.Velocita ?? 0;
    }
}