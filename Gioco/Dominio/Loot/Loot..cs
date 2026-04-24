using System;
using System.Collections.Generic;
using Gioco.Dominio.Modelli;

namespace Gioco.Dominio.Loot
{
    /// <summary>
    /// Gestisce il drop di monete e oggetti dai nemici.
    /// </summary>
    public class ServizioLoot
    {
        private readonly Random _random = new();

        /// <summary>
        /// Calcola quante monete droppa il nemico.
        /// </summary>
        public int CalcolaMonete(TabellaLoot tabella)
        {
            if (tabella.MoneteMax <= 0)
                return 0;

            return _random.Next(tabella.MoneteMin, tabella.MoneteMax + 1);
        }

        /// <summary>
        /// Restituisce la lista di oggetti droppati in base alle probabilità.
        /// </summary>
        public List<Oggetto> GeneraOggettiDroppati(TabellaLoot tabella)
        {
            var droppati = new List<Oggetto>();

            foreach (var voce in tabella.Oggetti)
            {
                int tiro = _random.Next(1, 101); // 1-100

                if (tiro <= voce.ProbabilitaDrop)
                {
                    droppati.Add(voce.Oggetto);
                }
            }

            return droppati;
        }

        /// <summary>
        /// Prova ad aggiungere gli oggetti all'inventario del personaggio.
        /// Se l'inventario è pieno, gli oggetti vengono scartati.
        /// </summary>
        public List<string> AggiungiOggettiAInventario(Personaggio personaggio, List<Oggetto> oggetti)
        {
            var log = new List<string>();

            foreach (var oggetto in oggetti)
            {
                if (personaggio.Inventario.Pieno)
                {
                    log.Add($"⚠ Inventario pieno! L'oggetto {oggetto.Nome} è stato perso.");
                    continue;
                }

                personaggio.Inventario.ProvaAggiungi(oggetto);
                log.Add($"📦 Ottenuto oggetto: {oggetto.Nome}");
            }

            return log;
        }
    }
}