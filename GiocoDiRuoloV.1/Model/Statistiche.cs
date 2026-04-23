namespace GiocoRuolo.Models
{
     /// <summary>
    /// Rappresenta le statistiche base e attuali di un personaggio o nemico.
    /// Utilizzate per calcolare danni, difesa, velocità e resistenza in combattimento.
    /// </summary>
    public class Statistiche
    {
        // Attacco: influenza i danni degli attacchi
        public int Attacco { get; set; }

        // Difesa: riduce i danni subiti
        public int Difesa { get; set; }

        // Punti vita massimi
        public int VitaMassima { get; set; }

        // Punti vita attuali
        public int VitaAttuale { get; set; }

        // Mana o energia per attacchi magici
        public int ManaMassimo { get; set; }
        public int ManaAttuale { get; set; }
        

        // Velocità: determina chi attacca per primo
        public int Velocita { get; set; }
        public int Critico { get; set; }

        public Resistenze Resistenze { get; set; } = new();
        public EffettoStato EffettoAttivo { get; set; } = new();
    }
}
