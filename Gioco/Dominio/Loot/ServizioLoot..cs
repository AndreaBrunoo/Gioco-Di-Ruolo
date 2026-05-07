
using Gioco.Dominio.Modelli;
using Gioco.Dominio.Enum;

namespace Gioco.Dominio.Loot
{
    public class ServizioLoot
    {
        private readonly Random _rng = new Random();

        // Probabilità base per rarità
        private readonly Dictionary<Rarita, int> ProbRarita = new()
        {
            { Rarita.Comune, 60 },
            { Rarita.NonComune, 25 },
            { Rarita.Raro, 10 },
            { Rarita.Epico, 4 },
            { Rarita.Leggendario, 1 }
        };

        // ---------------------------------------------------------
        // GENERA LOOT COMPLETO
        // ---------------------------------------------------------
        public List<LootItem> GeneraLoot(Nemico nemico, TabellaLoot tabella)
        {
            var lootFinale = new List<LootItem>();

            // 1) Monete
            int monete = _rng.Next(tabella.MoneteMin, tabella.MoneteMax + 1);
            lootFinale.Add(new LootItem
            {
                Nome = "Monete",
                Quantita = monete,
                Rarita = Rarita.Comune
            });

            // 2) Oggetti
            foreach (var item in tabella.PossibiliLoot)
            {
                int roll = _rng.Next(0, 100);
                int prob = ProbRarita[item.Rarita];

                if (roll < prob)
                {
                    lootFinale.Add(item);
                }
            }

            // 3) Bonus boss
            if (nemico.Boss)
            {
                lootFinale.Add(new LootItem
                {
                    Nome = "Essenza del Boss",
                    Rarita = Rarita.Leggendario,
                    Quantita = 1
                });
            }

            return lootFinale;
        }

        // ---------------------------------------------------------
        // AGGIUNGE IL LOOT ALL'INVENTARIO
        // ---------------------------------------------------------
        public void ApplicaLoot(Personaggio p, List<LootItem> loot)
        {
            foreach (var item in loot)
            {
                if (item.Nome == "Monete")
                {
                    p.Monete += item.Quantita;
                    continue;
                }

                p.Inventario.Oggetti.Add(new Oggetto
                {
                    Nome = item.Nome,
                    Quantita = item.Quantita
                });
            }
        }
    }
}