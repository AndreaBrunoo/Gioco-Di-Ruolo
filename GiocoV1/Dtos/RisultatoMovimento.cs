using GiocoV1.Modelli;

namespace GiocoV1.Dtos;
public class RisultatoMovimento
{
    public string Messaggio { get; set; } = "";
    public Entita? NemicoTrovato { get; set; } = null;
}