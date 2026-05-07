using Gioco.Dominio.Modelli;
using Gioco.Dominio.Enum;

namespace Gioco.Dominio.Mappa
{
    public class ServizioIncontri
    {
        private readonly Random _rng = new Random();

        // Probabilità base di incontro
        private const int ProbBase = 30; // 30%

        // Probabilità incontro raro
        private const int ProbRaro = 5;  // 5%

        // Probabilità miniboss casuale
        private const int ProbMiniBoss = 1; // 1%

        // ---------------------------------------------------------
        // DETERMINA SE AVVIENE UN INCONTRO
        // ---------------------------------------------------------
        public bool AvvieneIncontro(CellaMappa cella)
        {
            if (!cella.HaNemici)
                return false;

            int probabilita = ProbBase;

            // Modificatori per tipo cella
            switch (cella.Tipo)
            {
                case TipoCella.Strada:
                    probabilita -= 10; // meno incontri
                    break;

                case TipoCella.BoscoFitto:
                    probabilita += 15; // più incontri
                    break;

                case TipoCella.Grotta:
                    probabilita += 25; // quasi garantito
                    break;

                case TipoCella.Arena:
                    return true; // incontro forzato
            }

            int roll = _rng.Next(0, 100);
            return roll < probabilita;
        }

        // ---------------------------------------------------------
        // GENERA NEMICO DAL POOL
        // ---------------------------------------------------------
        public Nemico GeneraNemicoDaPool(PoolNemici pool)
        {
            // Miniboss casuale?
            if (pool.Nemici.Any(n => n.Boss))
            {
                int rollBoss = _rng.Next(0, 100);
                if (rollBoss < ProbMiniBoss)
                {
                    return pool.Nemici.First(n => n.Boss);
                }
            }

            // Incontro raro?
            int rollRaro = _rng.Next(0, 100);
            if (rollRaro < ProbRaro)
            {
                var rari = pool.Nemici.Where(n => n.LivelloMinaccia >= 5).ToList();
                if (rari.Count > 0)
                    return rari[_rng.Next(rari.Count)];
            }

            // Nemico normale
            var normali = pool.Nemici.Where(n => !n.Boss).ToList();
            return normali[_rng.Next(normali.Count)];
        }

        // ---------------------------------------------------------
        // GENERA NEMICO DATO IL POOL ID
        // ---------------------------------------------------------
        public Nemico? GeneraNemicoDaPoolId(int poolId, List<PoolNemici> poolTotali)
        {
            var pool = poolTotali.FirstOrDefault(p => p.Id == poolId);
            if (pool == null)
                return null;

            return GeneraNemicoDaPool(pool);
        }
    }
}