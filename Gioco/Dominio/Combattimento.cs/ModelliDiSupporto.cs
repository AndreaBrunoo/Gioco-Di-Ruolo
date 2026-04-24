namespace Gioco.Dominio.Combattimento
{
    /// <summary>
    /// Esito finale di uno scontro.
    /// </summary>
    public enum EsitoCombattimento
    {
        VittoriaGiocatore,
        SconfittaGiocatore,
        Fuga,
        Pareggio
    }

    /// <summary>
    /// Singola azione in un turno (per log o UI).
    /// </summary>
    public class AzioneCombattimento
    {
        public string Descrizione { get; set; } = string.Empty;
    }

    /// <summary>
    /// Risultato completo di un combattimento.
    /// </summary>
    public class RisultatoCombattimento
    {
        public EsitoCombattimento Esito { get; set; }
        public List<AzioneCombattimento> Log { get; set; } = new();

        public int EsperienzaGuadagnata { get; set; }
        public int MoneteGuadagnate { get; set; }

        // Qui potrai aggiungere: lista oggetti droppati, ecc.
    }
}