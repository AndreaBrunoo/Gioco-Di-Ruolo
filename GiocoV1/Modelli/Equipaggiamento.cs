namespace GiocoV1.Modelli;
public class Equipaggiamento
{
    public Mossa?[] MosseEquipaggiate { get; set; } = new Mossa?[3];

    public Oggetto? Arma { get; set; }
    public Oggetto? Elmo { get; set; }
    public Oggetto? Corazza { get; set; }
    public Oggetto? Gambali { get; set; }
    public Oggetto? Stivali { get; set; }
}