namespace GiocoRuolo.Models
{
    /// <summary>
    /// Rappresenta l'esito finale di un combattimento.
    /// Contiene informazioni su vincitore, sconfitto, turni e ricompense.
    /// </summary>
    public class EsitoCombattimento
    {
        // True se il giocatore ha vinto lo scontro
        public bool GiocatoreVincitore { get; set; }

        // Id del vincitore (giocatore o nemico)
        public string VincitoreId { get; set; }

        // Id dello sconfitto
        public string SconfittoId { get; set; }

        // Lista completa dei turni del combattimento
        public List<TurnoCombattimento> Turni { get; set; } = new();

        // Ricompensa ottenuta (solo se il giocatore vince)
        public Ricompensa Ricompensa { get; set; }

        // Log completo dello scontro (utile per console)
        public string LogFinale { get; set; }
    }
}