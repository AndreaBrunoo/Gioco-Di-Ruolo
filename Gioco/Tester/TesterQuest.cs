using System;
using System.Collections.Generic;
using Gioco.Dominio.Modelli;
using Gioco.Dominio.Quest;

namespace Gioco.Tester
{
    public static class TesterQuest
    {
        public static void Esegui(Personaggio personaggio)
        {
            Console.WriteLine("\n==============================");
            Console.WriteLine("         TEST: QUEST");
            Console.WriteLine("==============================\n");

            var servizioQuest = new ServizioQuest();

            // NPC con quest
            var npc = new Npc
            {
                Id = 1,
                Nome = "Elderon",
                DialogoIniziale = "Per favore, aiuta il villaggio!",
                DialogoCompletamento = "Grazie, hai salvato tutti!",

                QuestDaDare = new Quest
                {
                    Id = 1,
                    Titolo = "Uccidi 2 Goblin",
                    Descrizione = "Elimina 2 goblin che minacciano il villaggio.",
                    Obiettivi = new List<ObiettivoQuest>
                    {
                        new ObiettivoQuest
                        {
                            Descrizione = "Uccidi Goblin",
                            QuantitaRichiesta = 2
                        }
                    },
                    RicompensaXp = 50,
                    RicompensaMonete = 20
                }
            };

            // ACCETTA QUEST
            Console.WriteLine("--- ACCETTA QUEST ---");
            foreach (var log in servizioQuest.AccettaQuest(personaggio, npc))
                Console.WriteLine(log);

            // SIMULAZIONE UCCISIONE 2 GOBLIN
            Console.WriteLine("\n--- AGGIORNA OBIETTIVI ---");
            servizioQuest.AggiornaObiettivo(personaggio, "Uccidi Goblin");
            servizioQuest.AggiornaObiettivo(personaggio, "Uccidi Goblin");

            // CONSEGNA QUEST
            Console.WriteLine("\n--- CONSEGNA QUEST ---");
            foreach (var log in servizioQuest.ConsegnaQuest(personaggio, npc))
                Console.WriteLine(log);

            // RIEPILOGO
            Console.WriteLine("\n--- RIEPILOGO QUEST ---");
            Console.WriteLine($"XP Totale: {personaggio.Esperienza}");
            Console.WriteLine($"Monete Totali: {personaggio.Monete}");
            Console.WriteLine($"Quest completate: {personaggio.QuestCompletate.Count}");
        }
    }
}