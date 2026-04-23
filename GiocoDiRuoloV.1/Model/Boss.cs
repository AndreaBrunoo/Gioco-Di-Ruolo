namespace GiocoRuolo.Models
{
    /// <summary>
    /// Rappresenta un boss, una versione potenziata e più rara di un nemico.
    /// Include statistiche maggiorate, loot speciale e un identificatore unico.
    /// </summary>
    public class Boss : Nemico
    {
        // Identificatore unico del boss (es. "DragoAntico", "ReLich")
        public string IdBoss { get; set; }

        // Descrizione narrativa del boss
        public string Descrizione { get; set; }

        // Livello consigliato per affrontarlo
        public int LivelloConsigliato { get; set; }

        // Loot speciale (armi/armature rare o leggendarie)
        public Loot LootSpeciale { get; set; }

        // Flag per indicare se il boss è già stato sconfitto
        public bool Sconfitto { get; set; }
    }
}