using System.Collections.Generic;
using Gioco.Dominio.Modelli;

namespace Gioco.Dominio.Mappa
{
    /// <summary>
    /// Gestisce la mappa e il movimento del personaggio.
    /// </summary>
    public class ServizioMappa
    {
        private readonly Dictionary<(int idMappa, int x, int y), CellaMappa> _celle;

        public ServizioMappa(List<CellaMappa> celle)
        {
            _celle = new Dictionary<(int, int, int), CellaMappa>();

            foreach (var cella in celle)
            {
                _celle[(cella.IdMappa, cella.X, cella.Y)] = cella;
            }
        }

        /// <summary>
        /// Restituisce la cella in cui si trova il personaggio.
        /// </summary>
        public CellaMappa? GetCella(Personaggio personaggio)
        {
            _celle.TryGetValue((personaggio.IdMappa, personaggio.PosX, personaggio.PosY), out var cella);
            return cella;
        }

        /// <summary>
        /// Prova a muovere il personaggio nella direzione indicata.
        /// </summary>
        public bool Muovi(Personaggio personaggio, int dx, int dy)
        {
            int nuovoX = personaggio.PosX + dx;
            int nuovoY = personaggio.PosY + dy;

            if (_celle.ContainsKey((personaggio.IdMappa, nuovoX, nuovoY)))
            {
                personaggio.PosX = nuovoX;
                personaggio.PosY = nuovoY;
                return true;
            }

            return false;
        }
    }
}