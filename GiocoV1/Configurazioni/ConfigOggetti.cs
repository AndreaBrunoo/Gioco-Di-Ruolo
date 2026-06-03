using GiocoV1.Modelli;

namespace GiocoV1.Configurazioni;
public class ConfigOggetti
{
    public List<Oggetto> Pozioni { get; set; } = new();
    public List<Oggetto> Armi { get; set; } = new();
    public List<Oggetto> Materiali { get; set; } = new();
}