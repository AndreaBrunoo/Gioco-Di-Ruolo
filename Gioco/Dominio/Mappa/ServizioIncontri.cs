using System;
using System.Collections.Generic;
using Gioco.Dominio.Modelli;
using Gioco.Dominio.Enum;

namespace Gioco.Dominio.Mappa
{
    /// <summary>
    /// Gestisce gli incontri casuali con nemici.
    /// </summary>
    public class ServizioIncontri
    {
        private readonly Random _random = new();

        /// <summary>
        /// Determina se avviene un incontro casuale.
        /// </summary>
        public bool AvvieneIncontro(CellaMappa cella)
        {
            if (!cella.HaNemici)
                return false;

            // 20% di probabilità di incontro
            int tiro = _random.Next(1, 101);
            return tiro <= 20;
        }

        /// <summary>
        /// Restituisce un nemico casuale dal pool della cella.
        /// </summary>
        public Nemico GeneraNemicoDaPool(int idPool)
        {
            // Per ora: nemico fittizio.
            // In futuro: caricho i pool veri.

            return new Nemico
            {
                Id = 999,
                Nome = "Slime Verde",
                Boss = false,
                LivelloMinaccia = 1,
                Statistiche = new Statistiche
                {
                    SaluteMassima = 30,
                    SaluteAttuale = 30,
                    Attacco = 8,
                    Difesa = 3,
                    Velocita = 5,
                    ManaMassimo = 10,
                    ManaAttuale = 10
                },
                MoltiplicatoriElementali = new Dictionary<TipoElemento, double>
                {
                    { TipoElemento.Neutro, 1.0 },
                    { TipoElemento.Fuoco, 1.5 }, // Debole al fuoco
                    { TipoElemento.Ghiaccio, 1.0 },
                    { TipoElemento.Veleno, 1.0 },
                    { TipoElemento.Sacro, 1.0 },
                    { TipoElemento.Ombra, 1.0 }
                },
                TabellaLoot = new TabellaLoot
                {
                    MoneteMin = 1,
                    MoneteMax = 5
                }
            };
        }
    }
}