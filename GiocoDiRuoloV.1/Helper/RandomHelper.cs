using System;

namespace GiocoRuolo.Helpers
{
    /// <summary>
    /// Helper centralizzato per tutte le operazioni casuali del gioco.
    /// Evita duplicazioni e garantisce coerenza nei risultati.
    /// </summary>
    public static class RandomHelper
    {
        // Istanza unica di Random per evitare duplicazioni e numeri ripetuti
        private static readonly Random _random = new();

        /// <summary>
        /// Restituisce un numero intero casuale tra min (incluso) e max (escluso).
        /// </summary>
        public static int NextInt(int min, int max)
        {
            return _random.Next(min, max);
        }

        /// <summary>
        /// Restituisce un numero double casuale tra 0.0 e 1.0.
        /// </summary>
        public static double NextDouble()
        {
            return _random.NextDouble();
        }

        /// <summary>
        /// Restituisce true con una probabilità percentuale specificata.
        /// Es: Probabilità(30) -> 30% di probabilità di ottenere true.
        /// </summary>
        public static bool Probabilità(int percentuale)
        {
            return _random.Next(0, 100) < percentuale;
        }

        /// <summary>
        /// Estrae un elemento casuale da una lista.
        /// </summary>
        public static T EstraiDaLista<T>(List<T> lista)
        {
            if (lista == null || lista.Count == 0)
                throw new ArgumentException("La lista non può essere vuota.");

            int index = _random.Next(0, lista.Count);
            return lista[index];
        }
    }
}