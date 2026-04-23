namespace GiocoRuolo.DTO
{
    public class CombattimentoRequestDto
    {
        // ID del personaggio controllato dal giocatore
        public string GiocatoreId { get; set; }

        // ID del nemico o boss da affrontare
        public string NemicoId { get; set; }

        // ID dell'attacco scelto dal giocatore per questo turno
        public string AttaccoId { get; set; }

        // Flag utile se vuoi gestire combattimenti automatici o simulati
        public bool Auto { get; set; } = false;
    }
}