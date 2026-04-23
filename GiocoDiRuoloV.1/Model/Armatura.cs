namespace GiocoRuolo.Models
{
    /// <summary>
    /// Rappresenta un'armatura equipaggiabile dal personaggio.
    /// Estende Oggetto e aggiunge proprietà relative alla difesa e allo slot.
    /// </summary>
    public class Armatura : Oggetto
    {
        // Difesa fornita dall'armatura
        public int Difesa { get; set; }

        // Slot in cui può essere equipaggiata (Testa, Corpo, Gambe)
        public SlotEquipaggiamento Slot { get; set; }

        // Bonus alle statistiche (es. +1 Resistenza, +2 VitaMassima)
        public Dictionary<string, int> BonusStatistiche { get; set; } = new();
    }
}