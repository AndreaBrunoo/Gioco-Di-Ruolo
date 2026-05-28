using System.Text.Json;
using GiocoV1.Configurazioni;
using GiocoV1.Modelli;

namespace GiocoV1.Servizi
{
    public static class ServizioNemici
    {
        public static ConfigNemici CaricaNemici(string percorso)
        {
            if (!File.Exists(percorso))
                throw new FileNotFoundException($"File nemici non trovato: {percorso}");

            string json = File.ReadAllText(percorso);

            var opzioni = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var config = JsonSerializer.Deserialize<ConfigNemici>(json, opzioni);

            if (config == null)
                throw new Exception("Errore nel parsing del JSON dei nemici!");

            return config;
        }

        // ---------------------------------------------------------
        // 1) TENTA LO SPAWN DI UN NEMICO
        // ---------------------------------------------------------
        public static Entita? TentaSpawnNemico(
            Sezione sezione,
            CellaPosizionata cella,
            ConfigNemici configurazioneNemici)
        {
            // Se la sezione non ha configurazione → nessuno spawn
            if (!configurazioneNemici.Sezioni.TryGetValue(sezione.Nome, out var configurazione))
                return null;

            // 1) CONTROLLO BOSS
            if (configurazione.Boss != null && !configurazione.Boss.Sconfitto)
            {
                // Se la cella è una cella boss → spawna solo il boss
                foreach (var celleBoss in configurazione.CelleBoss)
                {
                    if (celleBoss.X == cella.X && celleBoss.Y == cella.Y)
                    {
                        return ClonaBoss(configurazione.Boss);
                    }
                }
            }

            // 2) CONTROLLO NEMICI NORMALI
            Random random = new();

            // Primo numero → probabilità di spawn
            int tiroSpawnGenerale = random.Next(1, 101);
            if (tiroSpawnGenerale > configurazione.ProbabilitaSpawn)
                return null; // nessun nemico

            // Secondo numero → quale nemico
            int totaleProbabilità = configurazione.Nemici.Sum(n => n.ProbabilitaSpawn);
            int tiroSpawnNemico = random.Next(1, totaleProbabilità + 1);

            int intervallo = 0;
            foreach (var nemico in configurazione.Nemici)
            {
                intervallo += nemico.ProbabilitaSpawn;
                if (tiroSpawnNemico <= intervallo)
                    return ClonaNemico(nemico);
            }
            return null;
        }

        // ---------------------------------------------------------
        // 2) CLONA UN NEMICO
        // ---------------------------------------------------------
        private static Nemico ClonaNemico(Nemico nemico)
        {
            return new Nemico
            {
                Nome = nemico.Nome,
                SaluteMassima = nemico.SaluteMassima,
                SaluteAttuale = nemico.SaluteMassima,
                Attacco = nemico.Attacco,
                Difesa = nemico.Difesa,
                Velocita = nemico.Velocita,
                Livello = nemico.Livello,
                ProbabilitaSpawn = nemico.ProbabilitaSpawn,
                Mosse = new List<Mossa>(nemico.Mosse)
            };
        }

        // ---------------------------------------------------------
        // 3) CLONA UN BOSS 
        // ---------------------------------------------------------
        private static Boss ClonaBoss(Boss boss)
        {
            return new Boss
            {
                Nome = boss.Nome,
                Salute = boss.Salute,
                Attacco = boss.Attacco,
                Difesa = boss.Difesa,
                Velocita = boss.Velocita,
                Livello = boss.Livello,
                Mosse = new List<Mossa>(boss.Mosse),
                Inventario = new List<Oggetto>(boss.Inventario)
            };
        }
    }
}