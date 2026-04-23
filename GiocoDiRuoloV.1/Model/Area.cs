namespace GiocoRuolo.Models
{
    /// <summary>
    /// Rappresenta una singola area della mappa.
    /// Contiene NPC, nemici, boss e collegamenti ad altre aree.
    /// </summary>
    public class Area
    {
        // Identificatore univoco dell'area
        public string Id { get; set; }

        // Nome dell'area (es. "Foresta Oscura", "Villaggio Iniziale")
        public string Nome { get; set; }

        // Descrizione narrativa dell'area
        public string Descrizione { get; set; }

        // NPC presenti nell'area
        public List<NPC> NpcPresenti { get; set; } = new();

        // Nemici presenti nell'area
        public List<Nemico> Nemici { get; set; } = new();

        // Boss presenti nell'area (opzionale)
        public List<Boss> Boss { get; set; } = new();

        // Aree collegate (es. "Foresta" -> "Villaggio")
        public List<string> AreeCollegate { get; set; } = new();

        // True se l'area è sbloccata per il giocatore
        public bool Sbloccata { get; set; }
    }
}