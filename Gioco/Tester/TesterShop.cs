using System;
using System.Collections.Generic;
using Gioco.Dominio.Modelli;
using Gioco.Dominio.Shop;
using Gioco.Dominio.Enum;

namespace Gioco.Tester
{
    public static class TesterShop
    {
        public static void Esegui(Personaggio personaggio)
        {
            Console.WriteLine("\n==============================");
            Console.WriteLine("         TEST: SHOP");
            Console.WriteLine("==============================\n");

            // Mercante di test
            var mercante = new Mercante
            {
                Id = 1,
                Nome = "Baldur il Mercante",
                InventarioVendita = new List<Oggetto>
                {
                    new Oggetto
                    {
                        Id = 200,
                        Nome = "Pozione di Cura",
                        TipoOggetto = TipoOggetto.Consumabile,
                        SlotEquip = SlotEquipaggiamento.Nessuno,
                        BonusSalute = 20,
                        Valore = 10,
                        Rarita = 1
                    },
                    new Oggetto
                    {
                        Id = 201,
                        Nome = "Pozione di Mana",
                        TipoOggetto = TipoOggetto.Consumabile,
                        SlotEquip = SlotEquipaggiamento.Nessuno,
                        BonusMana = 15,
                        Valore = 12,
                        Rarita = 1
                    }
                }
            };

            var servizioShop = new ServizioShop();

            Console.WriteLine("--- INVENTARIO MERCANTE ---");
            foreach (var o in mercante.InventarioVendita)
                Console.WriteLine($"• {o.Nome} (Valore: {o.Valore})");

            Console.WriteLine("\n--- TEST ACQUISTO ---");
            var oggettoDaComprare = mercante.InventarioVendita[0];

            foreach (var log in servizioShop.CompraOggetto(personaggio, mercante, oggettoDaComprare))
                Console.WriteLine(log);

            Console.WriteLine("\n--- TEST VENDITA ---");
            foreach (var log in servizioShop.VendiOggetto(personaggio, mercante, oggettoDaComprare))
                Console.WriteLine(log);

            // RIEPILOGO
            Console.WriteLine("\n--- RIEPILOGO SHOP ---");
            Console.WriteLine($"Monete attuali: {personaggio.Monete}");
            Console.WriteLine($"Oggetti in inventario: {personaggio.Inventario.Oggetti.Count}");

            if (personaggio.Inventario.Oggetti.Count > 0)
            {
                Console.WriteLine("Inventario:");
                foreach (var o in personaggio.Inventario.Oggetti)
                    Console.WriteLine($"• {o.Nome}");
            }
            else
            {
                Console.WriteLine("Inventario vuoto.");
            }
        }
    }
}