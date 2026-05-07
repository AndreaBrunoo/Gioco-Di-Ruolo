using System;
using System.Collections.Generic;
using Gioco.Dominio.Modelli;
using Gioco.Dominio.Combattimento;
using Gioco.Dominio.Enum;

namespace Gioco.Tester
{
    public static class TesterBoss
    {
        public static void Esegui(Personaggio personaggio)
        {
            Console.WriteLine("\n==============================");
            Console.WriteLine("       TEST: BOSS FIGHT");
            Console.WriteLine("==============================\n");

            // Boss di test
            var boss = new Nemico
            {
                Id = 999,
                Nome = "Drago Antico",
                Boss = true,
                LivelloMinaccia = 10,

                Statistiche = new Statistiche
                {
                    SaluteMassima = 200,
                    SaluteAttuale = 200,
                    Attacco = 30,
                    Difesa = 15,
                    Velocita = 20,
                    ManaMassimo = 50,
                    ManaAttuale = 50
                },

                // Soglie fasi
                SogliaFase2 = 60,
                SogliaFase3 = 25,
                NomeFase2 = "Furia Ardente",
                NomeFase3 = "Ultimo Respiro",

                MoltiplicatoriElementali = new Dictionary<TipoElemento, double>
                {
                    { TipoElemento.Neutro, 1.0 },
                    { TipoElemento.Fuoco, 0.5 },
                    { TipoElemento.Ghiaccio, 1.5 },
                    { TipoElemento.Veleno, 1.0 },
                    { TipoElemento.Sacro, 1.2 },
                    { TipoElemento.Ombra, 1.0 }
                }
            };

            // Attacchi fase 1
            boss.Attacchi.Add(new Attacco
            {
                Nome = "Artigliata",
                PotenzaBase = 25,
                PrecisioneBase = 90,
                ProbabilitaCritico = 10,
                Elemento = TipoElemento.Neutro
            });

            // Attacchi fase 2
            boss.AttacchiFase2.Add(new Attacco
            {
                Nome = "Soffio di Fuoco",
                PotenzaBase = 40,
                PrecisioneBase = 85,
                ProbabilitaCritico = 20,
                Elemento = TipoElemento.Fuoco
            });

            // Attacchi fase 3
            boss.AttacchiFase3.Add(new Attacco
            {
                Nome = "Esplosione Draconica",
                PotenzaBase = 60,
                PrecisioneBase = 80,
                ProbabilitaCritico = 30,
                Elemento = TipoElemento.Fuoco
            });

            var servizioCombattimento = new ServizioCombattimento();
            var risultato = servizioCombattimento.SimulaCombattimento(personaggio, boss);

            // LOG DETTAGLIATO
            foreach (var azione in risultato.Log)
                Console.WriteLine(azione.Descrizione);

            // RIEPILOGO
            Console.WriteLine("\n--- RISULTATO BOSS FIGHT ---");
            Console.WriteLine($"Esito: {risultato.Esito}");
            Console.WriteLine($"XP guadagnata: {risultato.EsperienzaGuadagnata}");
            Console.WriteLine($"Monete guadagnate: {risultato.MoneteGuadagnate}");

            Console.WriteLine("\n--- STATISTICHE PERSONAGGIO ---");
            Console.WriteLine($"Salute: {personaggio.Statistiche.SaluteAttuale}/{personaggio.Statistiche.SaluteMassima}");
            Console.WriteLine($"Mana: {personaggio.Statistiche.ManaAttuale}/{personaggio.Statistiche.ManaMassimo}");
            Console.WriteLine($"Attacco: {personaggio.Statistiche.Attacco}");
            Console.WriteLine($"Difesa: {personaggio.Statistiche.Difesa}");
            Console.WriteLine($"Velocità: {personaggio.Statistiche.Velocita}");
            Console.WriteLine($"Monete: {personaggio.Monete}");
            Console.WriteLine($"XP Totale: {personaggio.Esperienza}");
        }
    }
}