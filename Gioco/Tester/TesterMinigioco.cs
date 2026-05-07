using System;
using Gioco.Dominio.Modelli;
using Gioco.Dominio.Minigiochi;
using Gioco.Dominio.Enum;

namespace Gioco.Tester
{
    public static class TesterMinigioco
    {
        public static void Esegui(Personaggio personaggio)
        {
            Console.WriteLine("\n==============================");
            Console.WriteLine("       TEST: MINIGIOCO");
            Console.WriteLine("==============================\n");

            var servizioMinigiochi = new ServizioMinigiochi();

            // Minigioco di test
            var minigioco = new Minigioco
            {
                Id = 1,
                Nome = "Puzzle delle Rune",
                Descrizione = "Risolvi un antico puzzle magico.",
                RicompensaXp = 30,
                RicompensaMonete = 10,
                RicompensaOggetto = new Oggetto
                {
                    Id = 300,
                    Nome = "Amuleto delle Rune",
                    TipoOggetto = TipoOggetto.Armatura,
                    SlotEquip = SlotEquipaggiamento.Accessorio,
                    BonusMana = 10,
                    Valore = 20,
                    Rarita = 2
                }
            };

            Console.WriteLine("--- INIZIO MINIGIOCO ---");
            Console.WriteLine($"Nome: {minigioco.Nome}");
            Console.WriteLine($"Descrizione: {minigioco.Descrizione}");

            // COMPLETAMENTO MINIGIOCO
            Console.WriteLine("\n--- COMPLETAMENTO MINIGIOCO ---");
            foreach (var log in servizioMinigiochi.CompletaMinigioco(personaggio, minigioco))
                Console.WriteLine(log);

            // RIEPILOGO
            Console.WriteLine("\n--- RIEPILOGO MINIGIOCO ---");
            Console.WriteLine($"XP Totale: {personaggio.Esperienza}");
            Console.WriteLine($"Monete Totali: {personaggio.Monete}");

            Console.WriteLine("\n--- INVENTARIO ---");
            if (personaggio.Inventario.Oggetti.Count == 0)
            {
                Console.WriteLine("Inventario vuoto.");
            }
            else
            {
                foreach (var o in personaggio.Inventario.Oggetti)
                    Console.WriteLine($"• {o.Nome}");
            }
        }
    }
}