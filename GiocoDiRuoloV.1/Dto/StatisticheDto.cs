using GiocoRuolo.Models;

namespace GiocoRuolo.DTO
{
    public class StatisticheDto
    {
        public int Attacco { get; set; }
        public int Difesa { get; set; }
        public int VitaMassima { get; set; }
        public int ManaMassimo { get; set; }
        public int Velocita { get; set; }
        public int Critico { get; set; }

        public ResistenzeDto Resistenze { get; set; }
        public EffettoStatoDto EffettoAttivo { get; set; }
    }
}