namespace GiocoRuolo.DTO
{
    public class OggettoDto
    {
        public string Id { get; set; }
        public string Nome { get; set; }
        public string Descrizione { get; set; }

        public int Quantita { get; set; }

        public string Rarita { get; set; }   // comune, raro, epico, leggendario
        public string Tipo { get; set; }     // arma, pozione, materiale, ecc.
    }
}