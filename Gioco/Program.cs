using System;
using System.Collections.Generic;
using Gioco.Dominio.Enum;
using Gioco.Dominio.Modelli;
using Gioco.Dominio.Combattimento;
using Gioco.Dominio.Loot;
using Gioco.Dominio.Progressione;
using Gioco.Dominio.Mappa;
using Gioco.Dominio.Shop;
using Gioco.Dominio.Equip;

class Program
{
    static void Main()
    {
        // Creazione personaggio base
        var personaggio = new Personaggio
        {
            Id = 1,
            Nome = "Eroe",
            Classe = TipoClassePersonaggio.Guerriero,
            Livello = 1,
            Esperienza = 0,
            Monete = 0,
            Statistiche = new Statistiche
            {
                SaluteMassima = 100,
                SaluteAttuale = 100,
                Attacco = 20,
                Difesa = 10,
                Velocita = 15,
                ManaMassimo = 30,
                ManaAttuale = 30
            },
            MoltiplicatoriElementali = new Dictionary<TipoElemento, double>
            {
                { TipoElemento.Fuoco, 1.0 },
                { TipoElemento.Ghiaccio, 1.0 },
                { TipoElemento.Veleno, 1.0 },
                { TipoElemento.Sacro, 1.0 },
                { TipoElemento.Ombra, 1.0 },
                { TipoElemento.Neutro, 1.0 }
            }
        };

        // Attacco del personaggio
        var attaccoSpada = new Attacco
        {
            Id = 1,
            Nome = "Colpo di Spada",
            TipoAttacco = TipoAttacco.CorpoACorpo,
            Elemento = TipoElemento.Neutro,
            PotenzaBase = 25,
            CostoMana = 0,
            PrecisioneBase = 90,
            ProbabilitaCritico = 15
        };

        personaggio.Attacchi.Add(attaccoSpada);

        // Creazione nemico
        var nemico = new Nemico
        {
            Id = 1,
            Nome = "Goblin",
            Boss = false,
            LivelloMinaccia = 2,
            Statistiche = new Statistiche
            {
                SaluteMassima = 60,
                SaluteAttuale = 60,
                Attacco = 12,
                Difesa = 5,
                Velocita = 10,
                ManaMassimo = 10,
                ManaAttuale = 10
            },
            MoltiplicatoriElementali = new Dictionary<TipoElemento, double>
            {
                { TipoElemento.Neutro, 1.0 },
                { TipoElemento.Fuoco, 1.0 },
                { TipoElemento.Ghiaccio, 1.0 },
                { TipoElemento.Veleno, 1.0 },
                { TipoElemento.Sacro, 1.0 },
                { TipoElemento.Ombra, 1.0 }
            },
            TabellaLoot = new TabellaLoot
            {
                MoneteMin = 5,
                MoneteMax = 15,
                Oggetti = new List<VoceLoot>()
            }
        };

        nemico.TabellaLoot.Oggetti.Add(new VoceLoot
        {
            Oggetto = new Oggetto
            {
                Id = 100,
                Nome = "Spada Arrugginita",
                TipoOggetto = TipoOggetto.Arma,
                SlotEquip = SlotEquipaggiamento.Arma,
                BonusAttacco = 5,
                Valore = 10,
                Rarita = 1
            },
            ProbabilitaDrop = 50
        });

        // Attacco del nemico
        var attaccoPugnale = new Attacco
        {
            Id = 2,
            Nome = "Pugnale Rozzo",
            TipoAttacco = TipoAttacco.CorpoACorpo,
            Elemento = TipoElemento.Neutro,
            PotenzaBase = 15,
            CostoMana = 0,
            PrecisioneBase = 85,
            ProbabilitaCritico = 10
        };

        nemico.Attacchi.Add(attaccoPugnale);

        // Avvio combattimento
        var servizioCombattimento = new ServizioCombattimento();
        var risultato = servizioCombattimento.SimulaCombattimento(personaggio, nemico);

        var celle = new List<CellaMappa>
        {
            new CellaMappa { IdMappa = 1, X = 0, Y = 0, Tipo = TipoCella.Strada, HaNemici = true, IdPoolNemici = 1 },
            new CellaMappa { IdMappa = 1, X = 1, Y = 0, Tipo = TipoCella.Villaggio },
            new CellaMappa { IdMappa = 1, X = 0, Y = 1, Tipo = TipoCella.Dungeon, HaNemici = true, IdPoolNemici = 1 }
        };

        var servizioMappa = new ServizioMappa(celle);
        var servizioIncontri = new ServizioIncontri();

        // Posizione iniziale
        personaggio.IdMappa = 1;
        personaggio.PosX = 0;
        personaggio.PosY = 0;

        // Muovi a destra
        if (servizioMappa.Muovi(personaggio, 1, 0))
        {
            Console.WriteLine("Ti sei mosso a destra.");
        }

        var cellaAttuale = servizioMappa.GetCella(personaggio);

        if (cellaAttuale is null)
        {
            Console.WriteLine("Errore: il personaggio si trova fuori dalla mappa!");
        }
        else
        {
            if (servizioIncontri.AvvieneIncontro(cellaAttuale))
            {
                Console.WriteLine("Incontro casuale!");

                if (cellaAttuale.IdPoolNemici is null)
                {
                    Console.WriteLine("⚠ La cella ha nemici ma non ha un pool definito!");
                }
                else
                {
                    var nemicoCasuale = servizioIncontri.GeneraNemicoDaPool(cellaAttuale.IdPoolNemici.Value);

                    var risultato2 = servizioCombattimento.SimulaCombattimento(personaggio, nemicoCasuale);

                    foreach (var azione in risultato2.Log)
                        Console.WriteLine(azione.Descrizione);
                }
            }
            else
            {
                Console.WriteLine("Nessun incontro.");
            }
        }

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
        }
    }
        };

        var servizioShop = new ServizioShop();

        Console.WriteLine("\n--- TEST ACQUISTO ---");
        var logAcquisto = servizioShop.CompraOggetto(personaggio, mercante, mercante.InventarioVendita[0]);
        foreach (var l in logAcquisto) Console.WriteLine(l);

        Console.WriteLine("\n--- TEST VENDITA ---");
        var logVendita = servizioShop.VendiOggetto(personaggio, mercante, mercante.InventarioVendita[0]);
        foreach (var l in logVendita) Console.WriteLine(l);

        var servizioEquip = new ServizioEquipaggiamento();

        // Prendiamo la spada arrugginita droppata
        var spada = personaggio.Inventario.Oggetti.Find(o => o.Nome == "Spada Arrugginita");

        if (spada != null)
        {
            Console.WriteLine("\n--- EQUIPAGGIO ---");
            var logEquip = servizioEquip.Equipaggia(personaggio, spada);
            foreach (var l in logEquip) Console.WriteLine(l);

            Console.WriteLine($"Nuovo attacco: {personaggio.Statistiche.Attacco}");
        }

        // Stampa log
        foreach (var azione in risultato.Log)
        {
            Console.WriteLine(azione.Descrizione);
        }

        Console.WriteLine();
        Console.WriteLine($"Esito: {risultato.Esito}");
        Console.WriteLine($"XP guadagnata: {risultato.EsperienzaGuadagnata}");
        Console.WriteLine();
        Console.WriteLine("=== STATISTICHE FINALI ===");
        Console.WriteLine($"Livello: {personaggio.Livello}");
        Console.WriteLine($"XP: {personaggio.Esperienza}");
        Console.WriteLine($"Monete: {personaggio.Monete}");
        Console.WriteLine($"Salute: {personaggio.Statistiche.SaluteAttuale}/{personaggio.Statistiche.SaluteMassima}");
        Console.WriteLine($"Attacco: {personaggio.Statistiche.Attacco}");
        Console.WriteLine($"Difesa: {personaggio.Statistiche.Difesa}");
        Console.WriteLine($"Velocità: {personaggio.Statistiche.Velocita}");
        Console.WriteLine($"Mana: {personaggio.Statistiche.ManaAttuale}/{personaggio.Statistiche.ManaMassimo}");

        Console.ReadLine();
    }
}