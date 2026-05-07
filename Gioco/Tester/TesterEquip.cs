using System;
using System.Collections.Generic;
using Gioco.Dominio.Modelli;
using Gioco.Dominio.Equip;
using Gioco.Dominio.Enum;

namespace Gioco.Tester
{
    public static class TesterEquip
    {
        public static void Esegui(Personaggio personaggio)
        {
            Console.WriteLine("\n==============================");
            Console.WriteLine("      TEST: EQUIPAGGIAMENTO");
            Console.WriteLine("==============================\n");

            var servizioEquip = new ServizioEquipaggiamento();

            // Oggetti di test
            var spada = new Oggetto
            {
                Id = 300,
                Nome = "Spada d'Acciaio",
                TipoOggetto = TipoOggetto.Arma,
                SlotEquip = SlotEquipaggiamento.Arma,
                BonusAttacco = 10,
                Valore = 50,
                Rarita = 2
            };

            var elmo = new Oggetto
            {
                Id = 301,
                Nome = "Elmo di Ferro",
                TipoOggetto = TipoOggetto.Armatura,
                SlotEquip = SlotEquipaggiamento.Testa,
                BonusDifesa = 5,
                Valore = 30,
                Rarita = 1
            };

            // Aggiungiamo gli oggetti all'inventario
            personaggio.Inventario.Oggetti.Add(spada);
            personaggio.Inventario.Oggetti.Add(elmo);

            Console.WriteLine("--- INVENTARIO INIZIALE ---");
            foreach (var o in personaggio.Inventario.Oggetti)
                Console.WriteLine($"• {o.Nome}");

            // EQUIPAGGIO SPADA
            Console.WriteLine("\n--- EQUIPAGGIO SPADA ---");
            foreach (var log in servizioEquip.Equipaggia(personaggio, spada))
                Console.WriteLine(log);

            // EQUIPAGGIO ELMO
            Console.WriteLine("\n--- EQUIPAGGIO ELMO ---");
            foreach (var log in servizioEquip.Equipaggia(personaggio, elmo))
                Console.WriteLine(log);

            // RIEPILOGO
            Console.WriteLine("\n--- RIEPILOGO EQUIPAGGIAMENTO ---");
            Console.WriteLine($"Arma equipaggiata: {personaggio.Equipaggiamento.Arma?.Nome ?? "Nessuna"}");
            Console.WriteLine($"Testa equipaggiata: {personaggio.Equipaggiamento.Testa?.Nome ?? "Nessuna"}");

            Console.WriteLine("\n--- STATISTICHE AGGIORNATE ---");
            Console.WriteLine($"Attacco: {personaggio.Statistiche.Attacco}");
            Console.WriteLine($"Difesa: {personaggio.Statistiche.Difesa}");
            Console.WriteLine($"Velocità: {personaggio.Statistiche.Velocita}");
            Console.WriteLine($"Mana: {personaggio.Statistiche.ManaAttuale}/{personaggio.Statistiche.ManaMassimo}");
        }
    }
}