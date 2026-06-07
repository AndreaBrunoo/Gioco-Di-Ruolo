using GiocoV1.Modelli;
using GiocoV1.Enum;
using System.Runtime.CompilerServices;

namespace GiocoV1.Servizi;

public static class ServizioLivello
{
    private static readonly Random random = new Random();

    public static void Esperienza(Nemico nemico, EsitoIncontro esito)
    {
        if (esito != EsitoIncontro.Vittoria) return;
        int esperienza = random.Next(
            (nemico.Livello / nemico.ProbabilitaSpawn) * 200,
            (nemico.Livello / nemico.ProbabilitaSpawn) * 300);
    }
}