using Gioco.Dominio.Enum;

namespace Gioco.Dominio.Modelli
{
    /// <summary>
    /// Quest completa con obiettivi e ricompense.
    /// </summary>
    public class Quest
    {
        public int Id { get; set; }
        public string Titolo { get; set; } = string.Empty;
        public string Descrizione { get; set; } = string.Empty;

        public StatoQuest Stato { get; set; } = StatoQuest.NonIniziata;

        public List<ObiettivoQuest> Obiettivi { get; set; } = new();

        public int RicompensaXp { get; set; }
        public int RicompensaMonete { get; set; }
        public List<Oggetto> RicompensaOggetti { get; set; } = new();
    }
}