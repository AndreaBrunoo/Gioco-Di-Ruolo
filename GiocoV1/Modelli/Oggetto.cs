namespace GiocoV1.Modelli;

public class Oggetto
{
    public string Tipologia { get; set; } = string.Empty;
    public string Nome { get; set; } = string.Empty;
    public int BonusAttacco { get; set; }
    public int BonusDifesa { get; set; }
    public int BonusVelocita { get; set; }
    public int BonusSalute { get; set; }
}