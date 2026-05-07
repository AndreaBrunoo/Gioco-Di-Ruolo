using System.Collections.Generic;
using Gioco.Dominio.Enum;

namespace Gioco.Dominio.Modelli
{
    /// <summary>
    /// Obiettivo singolo della quest (es. uccidi 3 goblin).
    /// </summary>
    public class ObiettivoQuest
    {
        public string Descrizione { get; set; } = string.Empty;
        public int QuantitaRichiesta { get; set; }
        public int QuantitaAttuale { get; set; }

        public bool Completato => QuantitaAttuale >= QuantitaRichiesta;
    }

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