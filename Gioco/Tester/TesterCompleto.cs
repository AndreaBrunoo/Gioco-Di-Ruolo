using System;
using Gioco.Dominio.Modelli;

namespace Gioco.Tester
{
    public static class TesterCompleto
    {
        public static void Esegui(Personaggio personaggio)
        {
            Console.WriteLine("\n=======================================");
            Console.WriteLine("     TEST COMPLETO AUTOMATICO");
            Console.WriteLine("=======================================\n");

            Console.WriteLine("Inizio test completo...\n");

            // 1) Combattimento base
            Console.WriteLine("\n>>> TEST 1: COMBATTIMENTO BASE <<<");
            TesterCombattimento.Esegui(personaggio);

            // 2) Boss fight
            Console.WriteLine("\n>>> TEST 2: BOSS FIGHT <<<");
            TesterBoss.Esegui(personaggio);

            // 3) Shop
            Console.WriteLine("\n>>> TEST 3: SHOP <<<");
            TesterShop.Esegui(personaggio);

            // 4) Equipaggiamento
            Console.WriteLine("\n>>> TEST 4: EQUIPAGGIAMENTO <<<");
            TesterEquip.Esegui(personaggio);

            // 5) Quest
            Console.WriteLine("\n>>> TEST 5: QUEST <<<");
            TesterQuest.Esegui(personaggio);

            // 6) Minigioco
            Console.WriteLine("\n>>> TEST 6: MINIGIOCO <<<");
            TesterMinigioco.Esegui(personaggio);

            // 7) Mappa e incontri
            Console.WriteLine("\n>>> TEST 7: MAPPA <<<");
            TesterMappa.Esegui(personaggio);

            // 8) Abilità
            Console.WriteLine("\n>>> TEST 8: ABILITÀ <<<");
            TesterAbilita.Esegui(personaggio);

            // 9) Salvataggio
            Console.WriteLine("\n>>> TEST 9: SALVATAGGIO <<<");
            TesterSalvataggio.Esegui(personaggio);

            // RIEPILOGO FINALE
            Console.WriteLine("\n=======================================");
            Console.WriteLine("        RIEPILOGO FINALE TEST");
            Console.WriteLine("=======================================\n");

            Console.WriteLine($"Nome: {personaggio.Nome}");
            Console.WriteLine($"Livello: {personaggio.Livello}");
            Console.WriteLine($"XP Totale: {personaggio.Esperienza}");
            Console.WriteLine($"Monete Totali: {personaggio.Monete}");
            Console.WriteLine($"Salute: {personaggio.Statistiche.SaluteAttuale}/{personaggio.Statistiche.SaluteMassima}");
            Console.WriteLine($"Attacco: {personaggio.Statistiche.Attacco}");
            Console.WriteLine($"Difesa: {personaggio.Statistiche.Difesa}");
            Console.WriteLine($"Velocità: {personaggio.Statistiche.Velocita}");
            Console.WriteLine($"Mana: {personaggio.Statistiche.ManaAttuale}/{personaggio.Statistiche.ManaMassimo}");

            Console.WriteLine("\nInventario finale:");
            if (personaggio.Inventario.Oggetti.Count == 0)
                Console.WriteLine("• Nessun oggetto");
            else
                foreach (var o in personaggio.Inventario.Oggetti)
                    Console.WriteLine($"• {o.Nome}");

            Console.WriteLine("\nAbilità finali:");
            if (personaggio.AbilitaSbloccate.Count == 0)
                Console.WriteLine("• Nessuna abilità");
            else
                foreach (var a in personaggio.AbilitaSbloccate)
                    Console.WriteLine($"• {a.Nome}");

            Console.WriteLine("\n=== TEST COMPLETO TERMINATO ===");
        }
    }
}