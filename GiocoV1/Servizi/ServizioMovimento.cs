using GiocoV1.Modelli;
using GiocoV1.Configurazioni;
using GiocoV1.Dtos;

namespace GiocoV1.Servizi
{
    public class ServizioMovimento
    {
        private readonly Cella?[,] _griglia;
        public ServizioMovimento(Cella?[,] griglia)
        {
            _griglia = griglia;
        }

        /* public string Muovi(Personaggio personaggio, char direzione)
         {
             int nuovoX = personaggio.PosX;
             int nuovoY = personaggio.PosY;

             if (direzione == 'w' || direzione == 'W') nuovoY--;
             if (direzione == 's' || direzione == 'S') nuovoY++;
             if (direzione == 'd' || direzione == 'D') nuovoX++;
             if (direzione == 'a' || direzione == 'A') nuovoX--;

             // Controllo limiti
             if (nuovoX < 0 || nuovoX >= _griglia.GetLength(1) ||
                 nuovoY < 0 || nuovoY >= _griglia.GetLength(0))
                 return "Non puoi andare fuori dalla mappa.";

             // Controllo cella vuota
             if (_griglia[nuovoY, nuovoX] == null)
                 return "Non puoi andare lì, non c'è nulla.";

             // Movimento valido
             personaggio.PosX = nuovoX;
             personaggio.PosY = nuovoY;

             var cella = _griglia[nuovoY, nuovoX]!;
             return $"Ti trovi in: {cella.Nome}. {cella.Descrizione}";
         }*/

        public RisultatoMovimento Muovi(Personaggio personaggio, char direzione, Sezione sezione, ConfigNemici configNemici)
        {
            // 🔹 Se il tasto non è valido → non muovere e mostra la cella attuale
            if (!"wasdWASD".Contains(direzione))
            {
                var cellaAttuale = _griglia[personaggio.PosY, personaggio.PosX]!;

                return new RisultatoMovimento
                {
                    Messaggio = $"Ti trovi in: {cellaAttuale.Nome}. {cellaAttuale.Descrizione}",
                    NemicoTrovato = null
                };
            }
            
            var risultato = new RisultatoMovimento();
            int nuovoX = personaggio.PosX;
            int nuovoY = personaggio.PosY;

            if (direzione == 'w' || direzione == 'W') nuovoY--;
            if (direzione == 's' || direzione == 'S') nuovoY++;
            if (direzione == 'd' || direzione == 'D') nuovoX++;
            if (direzione == 'a' || direzione == 'A') nuovoX--;

            if (nuovoX < 0 || nuovoX >= _griglia.GetLength(1) ||
                nuovoY < 0 || nuovoY >= _griglia.GetLength(0))
            {
                risultato.Messaggio = "Non puoi andare fuori dalla mappa.";
                return risultato;
            }

            if (_griglia[nuovoY, nuovoX] == null)
            {
                risultato.Messaggio = "Non puoi andare lì, non c'è nulla.";
                return risultato;
            }

            personaggio.PosX = nuovoX;
            personaggio.PosY = nuovoY;

            var cella = _griglia[nuovoY, nuovoX]!;

            var cellaPosizionata = new CellaPosizionata
            {
                X = nuovoX,
                Y = nuovoY,
                Nome = cella.Nome,
                Descrizione = cella.Descrizione
            };

            // ⭐ TENTA LO SPAWN ⭐
            var entita = ServizioNemici.TentaSpawnNemico(sezione, cellaPosizionata, configNemici);

            if (entita != null)
            {
                risultato.NemicoTrovato = entita;
                risultato.Messaggio = entita is Boss b
                    ? $"⚠️ Ti trovi al cospetto di {b.Nome}!"
                    : $"{entita.Nome} ti blocca la strada";
                return risultato;
            }

            risultato.Messaggio = $"Ti trovi in: {cella.Nome}. {cella.Descrizione}";
            return risultato;
        }
    }
}