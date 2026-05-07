using System;
using Gioco.Dominio.Modelli;
using Gioco.Dominio.Combattimento;
using Gioco.Dominio.Enum;

namespace Gioco.Tester
{
    public static class TesterCombattimento
    {
        public static void Esegui(Personaggio personaggio)
        {
            Console.WriteLine("\n==============================");
            Console.WriteLine("     TEST: COMBATTIMENTO");
            Console.WriteLine("==============================\n");

            // Nemico di test
            var nemico = new Nemico
            {
                Id = 1,
                Nome = "Goblin",
                Boss = false,
                LivelloMinaccia = 1,
                Statistiche = new Statistiche
                {
                    SaluteMassima = 50,
                    SaluteAttuale = 50,
                    Attacco = 10,
                    Difesa = 5,
                    Velocita = 8,
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
                }
            };

            // Attacco base del nemico
            nemico.Attacchi.Add(new Attacco
            {
                Id = 2,
                Nome = "Pugno Rozzo",
                TipoAttacco = TipoAttacco.CorpoACorpo,
                Elemento = TipoElemento.Neutro,
                PotenzaBase = 12,
                CostoMana = 0,
                PrecisioneBase = 85,
                ProbabilitaCritico = 10
            });

            var servizioCombattimento = new ServizioCombattimento();
            var risultato = servizioCombattimento.SimulaCombattimento(personaggio, nemico);

            // LOG DETTAGLIATO
            foreach (var azione in risultato.Log)
                Console.WriteLine(azione.Descrizione);

            // RIEPILOGO
            Console.WriteLine("\n--- RISULTATO COMBATTIMENTO ---");
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