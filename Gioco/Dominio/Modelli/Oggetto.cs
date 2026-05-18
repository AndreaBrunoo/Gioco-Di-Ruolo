using Gioco.Dominio.Enum;

namespace Gioco.Dominio.Modelli
{
    public class Oggetto
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public TipoOggetto TipoOggetto { get; set; }
        public SlotEquipaggiamento SlotEquip { get; set; }

        public int BonusAttacco { get; set; }
        public int BonusDifesa { get; set; }
        public int BonusVelocita { get; set; }
        public int BonusSalute { get; set; }
        public int BonusMana { get; set; }

        public TipoElemento? Elemento { get; set; }

        public int Valore { get; set; }
        public Rarita Rarita { get; set; }
        public int Quantita { get; set; }
    }
}