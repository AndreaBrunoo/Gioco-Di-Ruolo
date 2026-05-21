using System.Text.Json;
using GiocoV1.Modelli;

namespace GiocoV1.Servizi
{
    public static class ServizioMappa
    {
        public static Mappa CaricaMappa(string file)
        {
            string json = File.ReadAllText($"Mappe/{file}");
            var mappa = JsonSerializer.Deserialize<Mappa>(json);

            if (mappa == null)
                throw new Exception("Errore nel file JSON della mappa.");

            return mappa;
        }

        public static Cella?[,] CreaGriglia(Sezione sezione)
        {
            Cella?[,] griglia = new Cella?[sezione.Altezza, sezione.Larghezza];

            foreach (var c in sezione.Celle)
            {
                griglia[c.Y, c.X] = new Cella
                {
                    Nome = c.Nome,
                    Descrizione = c.Descrizione
                };
            }

            return griglia;
        }
    }
}