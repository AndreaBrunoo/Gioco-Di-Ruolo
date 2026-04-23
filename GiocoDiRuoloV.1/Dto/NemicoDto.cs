using GiocoRuolo.Models;

namespace GiocoRuolo.DTO
{
    public class NemicoDto
    {
        public string Id { get; set; }
        public string Nome { get; set; }

        public StatisticheDto Statistiche { get; set; }

        public List<AttaccoDto> Attacchi { get; set; } = new();

        public Rarita Rarita { get; set; }

        public int Pericolosita { get; set; }

        public LootDto Loot { get; set; }

        public int Velocita { get; set; }
    }
}