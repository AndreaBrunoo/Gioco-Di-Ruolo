namespace GiocoRuolo.Models
{
    /// <summary>
    /// Rappresenta un personaggio non giocante (NPC).
    /// Può essere un mercante, un quest giver o un personaggio narrativo.
    /// </summary>
    public class NPC
    {
        // Nome dell'NPC
        public string Nome { get; set; }

        // Descrizione o dialogo introduttivo
        public string Descrizione { get; set; }

        // Tipo di NPC (Mercante, QuestGiver, Neutrale, Ostile, ecc.)
        public TipoNPC Tipo { get; set; }

        // Lista di dialoghi disponibili
        public List<string> Dialoghi { get; set; } = new();

        // Quest assegnabili da questo NPC (se è un quest giver)
        public List<Quest> QuestDisponibili { get; set; } = new();

        // Inventario del mercante (solo se Tipo = Mercante)
        public List<Oggetto> Inventario { get; set; } = new();

        // Posizione nella mappa (es. "Villaggio1", "ForestaOscura")
        public string PosizioneId { get; set; }
    }
}