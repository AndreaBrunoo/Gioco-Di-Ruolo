namespace GiocoRuolo.DTO
{
    public class CombattimentoResponseDto
    {
        // Turno appena eseguito
        public TurnoCombattimentoDto Turno { get; set; }

        // Stato aggiornato del giocatore
        public PersonaggioDto Giocatore { get; set; }

        // Stato aggiornato del nemico
        public NemicoDto Nemico { get; set; }

        // Se il combattimento è terminato
        public bool CombattimentoTerminato { get; set; }

        // Esito finale (solo se terminato)
        public EsitoCombattimentoDto EsitoFinale { get; set; }
    }
}