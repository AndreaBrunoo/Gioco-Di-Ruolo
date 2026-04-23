using GiocoRuolo.Models;

namespace GiocoRuolo.DTO
{
    public class ArmaDto
    {
        public string Id { get; set; }
        public string Nome { get; set; }
        public string Descrizione { get; set; }

        public int Danno { get; set; }
        public TipoAttacco TipoAttacco { get; set; }

        public Dictionary<string, int> BonusStatistiche { get; set; } = new();

        public string Rarita { get; set; }
        public int Valore { get; set; }
    }
}