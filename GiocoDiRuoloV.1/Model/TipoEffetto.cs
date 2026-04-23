namespace GiocoRuolo.Models
{
    public enum TipoEffetto
    {
        Nessuno = 0,
        Bruciatura = 1,   // Danno nel tempo
        Veleno = 2,       // Danno nel tempo
        Paralisi = 3,     // Chance di perdere il turno
        Congelamento = 4  // Immobilizzato per X turni
    }

    public class EffettoStato
    {
        public TipoEffetto Tipo { get; set; }
        public int Durata { get; set; } = 0;
    }
}