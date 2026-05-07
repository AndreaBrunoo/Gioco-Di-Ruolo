using System;
using System.Collections.Generic;
using Gioco.Dominio.Modelli;
using Gioco.Dominio.Mappa;
using Gioco.Dominio.Combattimento;
using Gioco.Dominio.Enum;

namespace Gioco.Tester
{
    public static class TesterMappa
    {
        public static void Esegui(Personaggio personaggio)
        {
            Console.WriteLine("\n==============================");
            Console.WriteLine("         TEST: MAPPA");
            Console.WriteLine("==============================\n");

            // Creiamo una piccola mappa di test
            var celle = new List<CellaMappa>
            {
                new CellaMappa { IdMappa = 1, X = 0, Y = 0, Tipo = TipoCella.Strada, HaNemici = true, IdPoolNemici = 1 },
                new CellaMappa { IdMappa = 1, X = 1, Y = 0, Tipo = TipoCella.Villaggio },
                new CellaMappa { IdMappa = 1, X = 0, Y = 1, Tipo = TipoCella.Dungeon, HaNemici = true, IdPoolNemici = 1 }
            };

            var servizioMappa = new ServizioMappa(celle);
            var servizioIncontri = new ServizioIncontri();
            var servizioCombattimento = new ServizioCombattimento();

            // Posizione iniziale
            personaggio.IdMappa = 1;
            personaggio.PosX = 0;
            personaggio.PosY = 0;

            Console.WriteLine($"Posizione iniziale: ({personaggio.PosX}, {personaggio.PosY})");

            // Tentiamo di muoverci a destra
            Console.WriteLine("\n--- MUOVI A DESTRA ---");
            if (servizioMappa.Muovi(personaggio, 1, 0))
                Console.WriteLine("Ti sei mosso a destra.");
            else
                Console.WriteLine("Non puoi muoverti in quella direzione.");

            var cellaAttuale = servizioMappa.GetCella(personaggio);

            if (cellaAttuale == null)
            {
                Console.WriteLine("Errore: il personaggio si trova fuori dalla mappa!");
                return;
            }

            Console.WriteLine($"Ora ti trovi nella cella: {cellaAttuale.Tipo}");

            // Controllo incontro
            Console.WriteLine("\n--- CONTROLLO INCONTRO ---");

            if (servizioIncontri.AvvieneIncontro(cellaAttuale))
            {
                Console.WriteLine("Incontro casuale!");

                if (cellaAttuale.IdPoolNemici == null)
                {
                    Console.WriteLine("⚠ La cella ha nemici ma non ha un pool definito!");
                }
                else
                {
                    var nemico = servizioIncontri.GeneraNemicoDaPool(cellaAttuale.IdPoolNemici.Value);

                    Console.WriteLine($"Hai incontrato: {nemico.Nome}");

                    var risultato = servizioCombattimento.SimulaCombattimento(personaggio, nemico);

                    Console.WriteLine("\n--- LOG COMBATTIMENTO ---");
                    foreach (var azione in risultato.Log)
                        Console.WriteLine(azione.Descrizione);

                    Console.WriteLine("\n--- RISULTATO INCONTRO ---");
                    Console.WriteLine($"Esito: {risultato.Esito}");
                    Console.WriteLine($"XP guadagnata: {risultato.EsperienzaGuadagnata}");
                    Console.WriteLine($"Monete guadagnate: {risultato.MoneteGuadagnate}");
                }
            }
            else
            {
                Console.WriteLine("Nessun incontro.");
            }

            // RIEPILOGO
            Console.WriteLine("\n--- RIEPILOGO MAPPA ---");
            Console.WriteLine($"Posizione finale: ({personaggio.PosX}, {personaggio.PosY})");
            Console.WriteLine($"Salute: {personaggio.Statistiche.SaluteAttuale}/{personaggio.Statistiche.SaluteMassima}");
            Console.WriteLine($"XP Totale: {personaggio.Esperienza}");
            Console.WriteLine($"Monete Totali: {personaggio.Monete}");
        }
    }
}