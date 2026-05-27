using GiocoV1.Modelli;

namespace GiocoV1.Servizi;
public static class ServizioIncontri
{
    public static void Incontro(Personaggio personaggio, Entita entita)
    {
        if (entita is Nemico)
        {
            Console.WriteLine("La battaglia ha inizio");
            while (true)
            {
                Console.WriteLine("Quale mossa vuoi utilizzare?");
                for (int i =0; i <3; i++)
                {
                    var mossa = personaggio.Equipaggiamenti.MosseEquipaggiate[i];
                    Console.WriteLine($"[{i + 1}]{(mossa?.Nome ?? "---")}");
                    char sceltaMossa = Console.ReadKey(true).KeyChar;
                }
            }
        }
        else if (entita is Boss)
        {
            
        }
        else return;
    }
}