using GiocoV1.Modelli;

namespace GiocoV1.Servizi
{
    public class ServizioMovimento
    {
        private readonly Cella?[,] _griglia;

        public ServizioMovimento(Cella?[,] griglia)
        {
            _griglia = griglia;
        }

        public string Muovi(Personaggio personaggio, string direzione)
        {
            int nuovoX = personaggio.PosX;
            int nuovoY = personaggio.PosY;

            if (direzione == "w" || direzione == "W") nuovoY--;
            if (direzione == "s" || direzione == "S") nuovoY++;
            if (direzione == "d" || direzione == "D") nuovoX++;
            if (direzione == "a" || direzione == "A") nuovoX--;

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
        }
    }
}