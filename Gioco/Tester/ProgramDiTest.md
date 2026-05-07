
## Program.cs di test

Codice completo per provare il gioco e le sue funzioni principali. 

```c#


using System;
using System.Collections.Generic;
using System.Threading;
using Gioco.Dominio.Modelli;
using Gioco.Dominio.Enum;
using Gioco.Dominio.Classi;
using Gioco.Tester;

class Program
{
    static void Main()
    {
        Console.Title = "RPG Engine - Test Suite";

        var personaggio = CreaPersonaggioIniziale();

        while (true)
        {
            Console.Clear();
            StampaTitolo("RPG ENGINE - TEST MENU");

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("1) Test Combattimento");
            Console.WriteLine("2) Test Boss Fight");
            Console.WriteLine("3) Test Shop");
            Console.WriteLine("4) Test Equipaggiamento");
            Console.WriteLine("5) Test Quest");
            Console.WriteLine("6) Test Minigioco");
            Console.WriteLine("7) Test Mappa");
            Console.WriteLine("8) Test Abilità");
            Console.WriteLine("9) Test Salvataggio");
            Console.WriteLine("10) TEST COMPLETO AUTOMATICO");
            Console.WriteLine("0) Esci");
            Console.ResetColor();

            Console.Write("\nSeleziona un'opzione: ");
            string? scelta = Console.ReadLine();

            Console.Clear();

            switch (scelta)
            {
                case "1":
                    StampaSezione("TEST COMBATTIMENTO");
                    TesterCombattimento.Esegui(personaggio);
                    break;

                case "2":
                    StampaSezione("TEST BOSS FIGHT");
                    TesterBoss.Esegui(personaggio);
                    break;

                case "3":
                    StampaSezione("TEST SHOP");
                    TesterShop.Esegui(personaggio);
                    break;

                case "4":
                    StampaSezione("TEST EQUIPAGGIAMENTO");
                    TesterEquip.Esegui(personaggio);
                    break;

                case "5":
                    StampaSezione("TEST QUEST");
                    TesterQuest.Esegui(personaggio);
                    break;

                case "6":
                    StampaSezione("TEST MINIGIOCO");
                    TesterMinigioco.Esegui(personaggio);
                    break;

                case "7":
                    StampaSezione("TEST MAPPA");
                    TesterMappa.Esegui(personaggio);
                    break;

                case "8":
                    StampaSezione("TEST ABILITÀ");
                    TesterAbilita.Esegui(personaggio);
                    break;

                case "9":
                    StampaSezione("TEST SALVATAGGIO");
                    TesterSalvataggio.Esegui(personaggio);
                    break;

                case "10":
                    StampaSezione("TEST COMPLETO AUTOMATICO");
                    AnimazioneBreve("Esecuzione test completi");
                    TesterCompleto.Esegui(personaggio);
                    break;

                case "0":
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Uscita dal programma...");
                    Console.ResetColor();
                    return;

                default:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Scelta non valida.");
                    Console.ResetColor();
                    break;
            }

            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine(new string('-', 40));
            Console.ResetColor();
            Console.WriteLine("Premi INVIO per tornare al menu...");
            Console.ReadLine();
        }
    }

    // ================== HELPER GRAFICI ==================

    static void StampaTitolo(string testo)
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine(new string('=', 39));
        Console.WriteLine(CentraTesto(testo, 39));
        Console.WriteLine(new string('=', 39));
        Console.WriteLine();
        Console.ResetColor();
    }

    static void StampaSezione(string testo)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine(new string('=', 39));
        Console.WriteLine(CentraTesto(testo, 39));
        Console.WriteLine(new string('=', 39));
        Console.WriteLine();
        Console.ResetColor();
    }

    static string CentraTesto(string testo, int larghezza)
    {
        if (testo.Length >= larghezza) return testo;
        int spazi = (larghezza - testo.Length) / 2;
        return new string(' ', spazi) + testo;
    }

    static void AnimazioneBreve(string testo)
    {
        Console.ForegroundColor = ConsoleColor.DarkCyan;
        Console.Write(testo);
        for (int i = 0; i < 3; i++)
        {
            Thread.Sleep(250);
            Console.Write(".");
        }
        Console.WriteLine("\n");
        Console.ResetColor();
    }

    // ================== CREAZIONE PERSONAGGIO ==================

    static Personaggio CreaPersonaggioIniziale()
    {
        Personaggio personaggio = new Personaggio
        {
            Id = 1,
            Nome = "Eroe",
            Classe = ClassePersonaggio.Guerriero,
            Livello = 1,
            Esperienza = 0,
            Monete = 50,

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

        var servizioClassi = new ServizioClassi();
        servizioClassi.ApplicaStatisticheBase(personaggio);
        personaggio.AbilitaSbloccate.AddRange(
            servizioClassi.AbilitaIniziali(personaggio.Classe)
        );

        return personaggio;
    }
}


```