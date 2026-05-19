namespace GiocoV1.Modelli
{
    public class Oggetto
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public int BonusAttacco { get; set; }
        public int BonusDifesa { get; set; }
        public int BonusVelocita { get; set; }
        public int BonusSalute { get; set; }
        public int Quantita { get; set; }
    }
}