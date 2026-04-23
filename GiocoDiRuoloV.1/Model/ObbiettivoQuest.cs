namespace GiocoRuolo.Models
{
    /// <summary>
    /// Rappresenta un singolo obiettivo all'interno di una quest.
    /// Può essere di tipo raccolta, uccisione, dialogo, esplorazione, ecc.
    /// </summary>
    public class ObiettivoQuest
    {
        // Identificatore univoco dell'obiettivo
        public string Id { get; set; }

        // Descrizione dell'obiettivo (es. "Sconfiggi 3 lupi")
        public string Descrizione { get; set; }

        // Tipo dell'obiettivo (Uccidi, Raccogli, Parla, Esplora...)
        public TipoObiettivo Tipo { get; set; }

        // Quantità richiesta (es. 3 lupi, 5 erbe, ecc.)
        public int QuantitaRichiesta { get; set; }

        // Quantità attualmente completata
        public int QuantitaAttuale { get; set; }

        // Id del target (nemico, oggetto, NPC, posizione)
        public string TargetId { get; set; }

        // Stato dell'obiettivo
        public StatoObiettivo Stato { get; set; } = StatoObiettivo.NonIniziato;

        // True se l'obiettivo è completato
        public bool Completato => Stato == StatoObiettivo.Completato;
    }
}