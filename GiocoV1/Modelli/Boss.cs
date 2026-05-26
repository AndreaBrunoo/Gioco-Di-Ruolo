namespace GiocoV1.Modelli;
public class Boss : Entita
{
    public int Salute { get; set; }
    public int Attacco { get; set; }
    public int Difesa { get; set; }
    public int Velocita { get; set; }
    public int Livello { get; set; }
    public List<Mossa> Mosse { get; set; } = new();
    public List<Oggetto> Inventario { get; set; } = new();
    public bool Sconfitto { get; set; } = false; // per non respawnare
}