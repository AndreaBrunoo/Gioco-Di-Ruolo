using System.Collections.Generic;
using Gioco.Dominio.Modelli;

namespace Gioco.Dominio.Shop
{
    /// <summary>
    /// Gestisce acquisti e vendite tra giocatore e mercante.
    /// </summary>
    public class ServizioShop
    {
        /// <summary>
        /// Il giocatore compra un oggetto dal mercante.
        /// </summary>
        public List<string> CompraOggetto(Personaggio personaggio, Mercante mercante, Oggetto oggetto)
        {
            var log = new List<string>();

            if (!mercante.InventarioVendita.Contains(oggetto))
            {
                log.Add("❌ Il mercante non vende questo oggetto.");
                return log;
            }

            if (personaggio.Monete < oggetto.Valore)
            {
                log.Add($"❌ Non hai abbastanza monete per comprare {oggetto.Nome}.");
                return log;
            }

            if (personaggio.Inventario.Pieno)
            {
                log.Add("❌ Inventario pieno! Non puoi acquistare altri oggetti.");
                return log;
            }

            personaggio.Monete -= oggetto.Valore;
            personaggio.Inventario.ProvaAggiungi(oggetto);

            log.Add($"🛒 Hai acquistato {oggetto.Nome} per {oggetto.Valore} monete.");

            return log;
        }

        /// <summary>
        /// Il giocatore vende un oggetto al mercante.
        /// </summary>
        public List<string> VendiOggetto(Personaggio personaggio, Mercante mercante, Oggetto oggetto)
        {
            var log = new List<string>();

            if (!personaggio.Inventario.Oggetti.Contains(oggetto))
            {
                log.Add("❌ Non possiedi questo oggetto.");
                return log;
            }

            int valoreVendita = oggetto.Valore / 2; // 50% del valore

            personaggio.Monete += valoreVendita;
            personaggio.Inventario.Rimuovi(oggetto);

            log.Add($"💰 Hai venduto {oggetto.Nome} per {valoreVendita} monete.");

            return log;
        }
    }
}