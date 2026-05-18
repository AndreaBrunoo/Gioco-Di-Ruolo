using Gioco.Dominio.Enum;

namespace Gioco.Dominio.Modelli
{
    public class CellaMappa
    {
        public int IdMappa { get; set; }
        public int X { get; set; }
        public int Y { get; set; }

        public TipoCella Tipo { get; set; }

        public bool HaNemici { get; set; }
        public bool HaMercante { get; set; }
        public bool HaNpcQuest { get; set; }

        public int? IdPoolNemici { get; set; }
        public int? IdBoss { get; set; }
        public int? IdMinigioco { get; set; }
    }
}