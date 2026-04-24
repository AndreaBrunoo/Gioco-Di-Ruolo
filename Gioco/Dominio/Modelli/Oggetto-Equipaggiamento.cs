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
        public int Rarita { get; set; }
    }

    public class Equipaggiamento
    {
        public Oggetto? Arma { get; set; }
        public Oggetto? Testa { get; set; }
        public Oggetto? Corpo { get; set; }
        public Oggetto? Gambe { get; set; }
        public Oggetto? Accessorio { get; set; }
    }
}