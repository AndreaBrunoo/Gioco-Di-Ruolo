using System;
using Gioco.Dominio.Modelli;
using Gioco.Dominio.Salvataggi;

namespace Gioco.Tester
{
    public static class TesterSalvataggio
    {
        public static void Esegui(Personaggio personaggio)
        {
            Console.WriteLine("\n==============================");
            Console.WriteLine("      TEST: SALVATAGGIO");
            Console.WriteLine("==============================\n");

            var servizioSalvataggio = new ServizioSalvataggio();
            string percorso = "salvataggio_test.json";

            Console.WriteLine("--- SALVATAGGIO PERSONAGGIO ---");
            servizioSalvataggio.Salva(percorso, personaggio);
            Console.WriteLine($"💾 Personaggio salvato in: {percorso}");

            Console.WriteLine("\n--- CARICAMENTO PERSONAGGIO ---");
            var caricato = servizioSalvataggio.Carica(percorso);

            if (caricato == null)
            {
                Console.WriteLine("❌ Errore: impossibile caricare il personaggio!");
                return;
            }

            Console.WriteLine("🔄 Personaggio caricato correttamente!");

            // RIEPILOGO
            Console.WriteLine("\n--- RIEPILOGO SALVATAGGIO ---");
            Console.WriteLine($"Nome: {caricato.Nome}");
            Console.WriteLine($"Livello: {caricato.Livello}");
            Console.WriteLine($"XP: {caricato.Esperienza}");
            Console.WriteLine($"Monete: {caricato.Monete}");
            Console.WriteLine($"Salute: {caricato.Statistiche.SaluteAttuale}/{caricato.Statistiche.SaluteMassima}");
            Console.WriteLine($"Attacco: {caricato.Statistiche.Attacco}");
            Console.WriteLine($"Difesa: {caricato.Statistiche.Difesa}");
            Console.WriteLine($"Velocità: {caricato.Statistiche.Velocita}");
            Console.WriteLine($"Mana: {caricato.Statistiche.ManaAttuale}/{caricato.Statistiche.ManaMassimo}");

            Console.WriteLine("\nInventario caricato:");
            if (caricato.Inventario.Oggetti.Count == 0)
                Console.WriteLine("• Nessun oggetto");
            else
                foreach (var o in caricato.Inventario.Oggetti)
                    Console.WriteLine($"• {o.Nome}");

            Console.WriteLine("\nAbilità caricate:");
            if (caricato.AbilitaSbloccate.Count == 0)
                Console.WriteLine("• Nessuna abilità");
            else
                foreach (var a in caricato.AbilitaSbloccate)
                    Console.WriteLine($"• {a.Nome}");
        }
    }
}