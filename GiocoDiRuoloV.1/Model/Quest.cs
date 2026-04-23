namespace GiocoRuolo.Models
{
    /// <summary>
    /// Rappresenta una quest assegnabile da un NPC.
    /// Contiene obiettivi, stato e ricompense.
    /// </summary>
    public class Quest
    {
        // Identificatore univoco della quest
        public string Id { get; set; }

        // Nome della quest
        public string Nome { get; set; }

        // Descrizione narrativa della quest
        public string Descrizione { get; set; }

        // Tipo della quest (Principale, Secondaria, Ripetibile)
        public TipoQuest Tipo { get; set; }

        // Obiettivi della quest
        public List<ObiettivoQuest> Obiettivi { get; set; } = new();

        // Ricompense ottenute al completamento
        public Ricompensa Ricompensa { get; set; }

        // Stato attuale della quest
        public StatoQuest Stato { get; set; } = StatoQuest.NonIniziata;

        // NPC che assegna la quest
        public string NpcId { get; set; }

        // Flag per indicare se la quest è ripetibile
        public bool Ripetibile { get; set; }
    }
}