using GiocoRuolo.Models;

namespace GiocoRuolo.DTO
{
    public class ArmaturaDto
    {
        public string Id { get; set; }
        public string Nome { get; set; }
        public string Descrizione { get; set; }

        public int Difesa { get; set; }
        public SlotEquipaggiamento Slot { get; set; }

        public Dictionary<string, int> BonusStatistiche { get; set; } = new();

        public string Rarita { get; set; }
        public int Valore { get; set; }
    }
}