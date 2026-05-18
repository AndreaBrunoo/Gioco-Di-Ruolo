using System.Collections.Generic;

namespace Gioco.Dominio.Modelli
{
    public class Inventario
    {
        public int CapacitaMassima { get; } = 10;
        public List<Oggetto> Oggetti { get; } = new();

        public bool Pieno => Oggetti.Count >= CapacitaMassima;

        public bool Aggiungi(Oggetto oggetto)
        {
            if (Pieno)
                return false;

            Oggetti.Add(oggetto);
            return true;
        }

        public bool Rimuovi(Oggetto oggetto)
        {
            return Oggetti.Remove(oggetto);
        }
    }
}