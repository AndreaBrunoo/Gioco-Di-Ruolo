namespace GiocoRuolo.Models
{
    /// <summary>
    /// Rappresenta un villaggio all'interno della mappa di gioco.
    /// Contiene NPC, mercanti, servizi e collegamenti ad altre aree.
    /// </summary>
    public class Villaggio
    {
        // Identificatore univoco del villaggio
        public string Id { get; set; }

        // Nome del villaggio (es. "Borgo Verde", "Rocca del Sole")
        public string Nome { get; set; }

        // Descrizione narrativa del villaggio
        public string Descrizione { get; set; }

        // NPC presenti nel villaggio (abitanti, quest giver, guide...)
        public List<NPC> NpcPresenti { get; set; } = new();

        // Mercanti disponibili nel villaggio
        public List<Mercante> Mercanti { get; set; } = new();

        // Quest disponibili nel villaggio
        public List<Quest> QuestDisponibili { get; set; } = new();

        // Aree collegate (es. "ForestaOscura", "StradaReale")
        public List<string> AreeCollegate { get; set; } = new();

        // True se il villaggio è un punto di respawn
        public bool PuntoRespawn { get; set; }

        // True se il villaggio offre un punto di riposo (locanda)
        public bool HaLocanda { get; set; }

        // True se il villaggio offre un fabbro per riparare equipaggiamento
        public bool HaFabbro { get; set; }
    }
}