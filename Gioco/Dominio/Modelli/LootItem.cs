using Gioco.Dominio.Enum;

namespace Gioco.Dominio.Modelli
{
    public class LootItem
    {
        public string Nome { get; set; } = "";
        public Rarita Rarita { get; set; }
        public int Quantita { get; set; } = 1;
    }
}