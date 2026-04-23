namespace GiocoRuolo.DTO
{
    public class RicompensaDto
    {
        public int Monete { get; set; }
        public int Esperienza { get; set; }

        public List<OggettoDto> Oggetti { get; set; } = new();

        public bool HaOggetti => Oggetti != null && Oggetti.Count > 0;
    }
}