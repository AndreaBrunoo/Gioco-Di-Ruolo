using Gioco.Dominio.Enum;

namespace Gioco.Dominio.Modelli
{
    public class Abilita
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Descrizione { get; set; } = string.Empty;

        public TipoAbilita Tipo { get; set; }

        // Costo mana per abilità attive
        public int CostoMana { get; set; }

        // Bonus per abilità passive
        public int BonusAttacco { get; set; }
        public int BonusDifesa { get; set; }
        public int BonusVelocita { get; set; }
        public int BonusSalute { get; set; }
        public int BonusMana { get; set; }

        // Danno base per abilità attive
        public int Potenza { get; set; }

        // Elemento dell’abilità
        public TipoElemento Elemento { get; set; } = TipoElemento.Neutro;
    }
}