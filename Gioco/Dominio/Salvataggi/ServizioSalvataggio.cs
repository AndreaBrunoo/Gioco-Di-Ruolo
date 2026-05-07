using System.IO;
using System.Text.Json;
using Gioco.Dominio.Modelli;

namespace Gioco.Dominio.Salvataggi
{
    /// <summary>
    /// Gestisce salvataggio e caricamento del gioco in JSON.
    /// </summary>
    public class ServizioSalvataggio
    {
        private readonly JsonSerializerOptions _options = new()
        {
            WriteIndented = true,
            IncludeFields = true
        };

        public void Salva(string percorso, Personaggio personaggio)
        {
            var save = new SalvataggioGioco
            {
                Personaggio = personaggio,
                IdMappa = personaggio.IdMappa,
                PosX = personaggio.PosX,
                PosY = personaggio.PosY,
                QuestAttiva = personaggio.QuestAttiva
            };

            var json = JsonSerializer.Serialize(save, _options);
            File.WriteAllText(percorso, json);
        }

        public Personaggio Carica(string percorso)
        {
            var json = File.ReadAllText(percorso);
            var save = JsonSerializer.Deserialize<SalvataggioGioco>(json, _options);

            if (save == null)
                throw new Exception("Errore nel caricamento del salvataggio.");

            var personaggio = save.Personaggio;
            personaggio.IdMappa = save.IdMappa;
            personaggio.PosX = save.PosX;
            personaggio.PosY = save.PosY;
            personaggio.QuestAttiva = save.QuestAttiva;

            return personaggio;
        }
    }
}