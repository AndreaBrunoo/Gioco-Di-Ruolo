using Gioco.Dominio.Modelli;

namespace Gioco.Dominio.Mappa
{
    public class ServizioMappa
    {
        private readonly Dictionary<int, List<CellaMappa>> _mappe;

        public ServizioMappa(
            List<CellaMappa> celleVillaggio,
            List<CellaMappa> celleBosco,
            List<CellaMappa> cellePianure,
            List<CellaMappa> celleMontagna,
            List<CellaMappa> celleRovine)
        {
            _mappe = new Dictionary<int, List<CellaMappa>>
            {
                { 1, celleVillaggio },
                { 2, celleBosco },
                { 3, cellePianure },
                { 4, celleMontagna },
                { 5, celleRovine }
            };
        }

        // ---------------------------------------------------------
        // OTTIENI CELLA CORRENTE
        // ---------------------------------------------------------
        public CellaMappa? GetCella(Personaggio p)
        {
            if (!_mappe.ContainsKey(p.IdMappa))
                return null;

            return _mappe[p.IdMappa]
                .FirstOrDefault(c => c.X == p.PosX && c.Y == p.PosY);
        }

        // ---------------------------------------------------------
        // MUOVI IL PERSONAGGIO
        // ---------------------------------------------------------
        public bool Muovi(Personaggio p, int dx, int dy)
        {
            int nuovoX = p.PosX + dx;
            int nuovoY = p.PosY + dy;

            if (!_mappe.ContainsKey(p.IdMappa))
                return false;

            var celle = _mappe[p.IdMappa];

            var nuovaCella = celle.FirstOrDefault(c => c.X == nuovoX && c.Y == nuovoY);

            if (nuovaCella == null)
                return false;

            // Movimento valido
            p.PosX = nuovoX;
            p.PosY = nuovoY;

            return true;
        }

        // ---------------------------------------------------------
        // CAMBIO MAPPA (es. uscita dal villaggio → bosco)
        // ---------------------------------------------------------
        public bool CambiaMappa(Personaggio p, int nuovaMappa, int x, int y)
        {
            if (!_mappe.ContainsKey(nuovaMappa))
                return false;

            p.IdMappa = nuovaMappa;
            p.PosX = x;
            p.PosY = y;

            return true;
        }

        // ---------------------------------------------------------
        // CONTROLLA SE LA CELLA HA NEMICI
        // ---------------------------------------------------------
        public bool CellaHaNemici(Personaggio p)
        {
            var cella = GetCella(p);
            return cella != null && cella.HaNemici;
        }

        // ---------------------------------------------------------
        // OTTIENI POOL NEMICI DELLA CELLA
        // ---------------------------------------------------------
        public int? GetPoolNemici(Personaggio p)
        {
            var cella = GetCella(p);
            return cella?.IdPoolNemici;
        }
    }
}