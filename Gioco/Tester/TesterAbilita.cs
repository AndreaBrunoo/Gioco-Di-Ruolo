using System;
using System.Collections.Generic;
using Gioco.Dominio.Modelli;
using Gioco.Dominio.Abilità;
using Gioco.Dominio.Enum;

namespace Gioco.Tester
{
    public static class TesterAbilita
    {
        public static void Esegui(Personaggio personaggio)
        {
            Console.WriteLine("\n==============================");
            Console.WriteLine("       TEST: ABILITÀ");
            Console.WriteLine("==============================\n");

            var servizioAbilita = new ServizioAbilita();

            // Abilità di test
            var abilitaAttiva = new Abilita
            {
                Id = 10,
                Nome = "Palla di Fuoco",
                Descrizione = "Lancia una sfera infuocata.",
                Tipo = TipoAbilita.Attiva,
                Potenza = 40,
                CostoMana = 10,
                Elemento = TipoElemento.Fuoco
            };

            var abilitaPassiva = new Abilita
            {
                Id = 11,
                Nome = "Forza del Guerriero",
                Descrizione = "Aumenta permanentemente l'attacco.",
                Tipo = TipoAbilita.Passiva,
                BonusAttacco = 5
            };

            Console.WriteLine("--- SBLOCCO ABILITÀ PASSIVA ---");
            foreach (var log in servizioAbilita.SbloccaAbilita(personaggio, abilitaPassiva))
                Console.WriteLine(log);

            Console.WriteLine("\n--- SBLOCCO ABILITÀ ATTIVA ---");
            foreach (var log in servizioAbilita.SbloccaAbilita(personaggio, abilitaAttiva))
                Console.WriteLine(log);

            // RIEPILOGO
            Console.WriteLine("\n--- RIEPILOGO ABILITÀ ---");
            Console.WriteLine("Abilità sbloccate:");

            foreach (var a in personaggio.AbilitaSbloccate)
                Console.WriteLine($"• {a.Nome} ({a.Tipo})");

            Console.WriteLine("\n--- STATISTICHE AGGIORNATE ---");
            Console.WriteLine($"Attacco: {personaggio.Statistiche.Attacco}");
            Console.WriteLine($"Difesa: {personaggio.Statistiche.Difesa}");
            Console.WriteLine($"Mana: {personaggio.Statistiche.ManaAttuale}/{personaggio.Statistiche.ManaMassimo}");
        }
    }
}