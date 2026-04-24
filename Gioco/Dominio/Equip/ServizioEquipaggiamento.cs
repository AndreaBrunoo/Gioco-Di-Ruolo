using System.Collections.Generic;
using Gioco.Dominio.Modelli;
using Gioco.Dominio.Enum;

namespace Gioco.Dominio.Equip
{
    /// <summary>
    /// Gestisce l'equipaggiamento del personaggio (indossare e rimuovere oggetti).
    /// </summary>
    public class ServizioEquipaggiamento
    {
        /// <summary>
        /// Prova a equipaggiare un oggetto dal suo inventario.
        /// </summary>
        public List<string> Equipaggia(Personaggio personaggio, Oggetto oggetto)
        {
            var log = new List<string>();

            if (!personaggio.Inventario.Oggetti.Contains(oggetto))
            {
                log.Add("❌ L'oggetto non è nel tuo inventario.");
                return log;
            }

            if (oggetto.SlotEquip == SlotEquipaggiamento.Nessuno)
            {
                log.Add($"❌ {oggetto.Nome} non è equipaggiabile.");
                return log;
            }

            // Rimuovi eventuale oggetto già equipaggiato nello stesso slot
            var rimosso = RimuoviOggettoDaSlot(personaggio, oggetto.SlotEquip);
            if (rimosso != null)
            {
                personaggio.Inventario.ProvaAggiungi(rimosso);
                log.Add($"↩ Hai rimosso {rimosso.Nome}.");
            }

            // Equipaggia il nuovo oggetto
            AssegnaOggettoASlot(personaggio, oggetto);
            personaggio.Inventario.Rimuovi(oggetto);

            // Aggiorna statistiche
            ApplicaBonusStatistiche(personaggio);

            log.Add($"🛡️ Hai equipaggiato {oggetto.Nome}.");

            return log;
        }

        /// <summary>
        /// Rimuove un oggetto equipaggiato e lo rimette nell'inventario.
        /// </summary>
        public List<string> Rimuovi(Personaggio personaggio, SlotEquipaggiamento slot)
        {
            var log = new List<string>();

            var oggetto = RimuoviOggettoDaSlot(personaggio, slot);

            if (oggetto == null)
            {
                log.Add("❌ Nessun oggetto equipaggiato in questo slot.");
                return log;
            }

            if (personaggio.Inventario.Pieno)
            {
                log.Add($"⚠ Inventario pieno! {oggetto.Nome} è stato rimosso ma perso.");
                return log;
            }

            personaggio.Inventario.ProvaAggiungi(oggetto);
            ApplicaBonusStatistiche(personaggio);

            log.Add($"🗃️ Hai rimosso {oggetto.Nome}.");

            return log;
        }

        // ------------------------------
        // METODI PRIVATI
        // ------------------------------

        private Oggetto? RimuoviOggettoDaSlot(Personaggio personaggio, SlotEquipaggiamento slot)
        {
            Oggetto? oggetto = slot switch
            {
                SlotEquipaggiamento.Arma => personaggio.Equipaggiamento.Arma,
                SlotEquipaggiamento.Testa => personaggio.Equipaggiamento.Testa,
                SlotEquipaggiamento.Corpo => personaggio.Equipaggiamento.Corpo,
                SlotEquipaggiamento.Gambe => personaggio.Equipaggiamento.Gambe,
                SlotEquipaggiamento.Accessorio => personaggio.Equipaggiamento.Accessorio,
                _ => null
            };

            if (oggetto == null)
                return null;

            // Svuota lo slot
            switch (slot)
            {
                case SlotEquipaggiamento.Arma: personaggio.Equipaggiamento.Arma = null; break;
                case SlotEquipaggiamento.Testa: personaggio.Equipaggiamento.Testa = null; break;
                case SlotEquipaggiamento.Corpo: personaggio.Equipaggiamento.Corpo = null; break;
                case SlotEquipaggiamento.Gambe: personaggio.Equipaggiamento.Gambe = null; break;
                case SlotEquipaggiamento.Accessorio: personaggio.Equipaggiamento.Accessorio = null; break;
            }

            return oggetto;
        }

        private void AssegnaOggettoASlot(Personaggio personaggio, Oggetto oggetto)
        {
            switch (oggetto.SlotEquip)
            {
                case SlotEquipaggiamento.Arma: personaggio.Equipaggiamento.Arma = oggetto; break;
                case SlotEquipaggiamento.Testa: personaggio.Equipaggiamento.Testa = oggetto; break;
                case SlotEquipaggiamento.Corpo: personaggio.Equipaggiamento.Corpo = oggetto; break;
                case SlotEquipaggiamento.Gambe: personaggio.Equipaggiamento.Gambe = oggetto; break;
                case SlotEquipaggiamento.Accessorio: personaggio.Equipaggiamento.Accessorio = oggetto; break;
            }
        }

        /// <summary>
        /// Ricalcola le statistiche del personaggio in base all'equipaggiamento.
        /// </summary>
        private void ApplicaBonusStatistiche(Personaggio personaggio)
        {
            // Prima riportiamo le statistiche ai valori base (senza equip)
            var baseStats = personaggio.Statistiche;

            // Reset ai valori base (senza bonus)
            int saluteBase = baseStats.SaluteMassima - SommaBonus(personaggio, o => o.BonusSalute);
            int attaccoBase = baseStats.Attacco - SommaBonus(personaggio, o => o.BonusAttacco);
            int difesaBase = baseStats.Difesa - SommaBonus(personaggio, o => o.BonusDifesa);
            int velocitaBase = baseStats.Velocita - SommaBonus(personaggio, o => o.BonusVelocita);
            int manaBase = baseStats.ManaMassimo - SommaBonus(personaggio, o => o.BonusMana);

            // Ora applichiamo i bonus
            baseStats.SaluteMassima = saluteBase + SommaBonus(personaggio, o => o.BonusSalute);
            baseStats.Attacco = attaccoBase + SommaBonus(personaggio, o => o.BonusAttacco);
            baseStats.Difesa = difesaBase + SommaBonus(personaggio, o => o.BonusDifesa);
            baseStats.Velocita = velocitaBase + SommaBonus(personaggio, o => o.BonusVelocita);
            baseStats.ManaMassimo = manaBase + SommaBonus(personaggio, o => o.BonusMana);

            // Mantene salute/mana attuali entro i limiti
            baseStats.SaluteAttuale = Math.Min(baseStats.SaluteAttuale, baseStats.SaluteMassima);
            baseStats.ManaAttuale = Math.Min(baseStats.ManaAttuale, baseStats.ManaMassimo);
        }

        private int SommaBonus(Personaggio personaggio, System.Func<Oggetto, int> selettore)
        {
            int somma = 0;

            var equip = personaggio.Equipaggiamento;

            if (equip.Arma != null) somma += selettore(equip.Arma);
            if (equip.Testa != null) somma += selettore(equip.Testa);
            if (equip.Corpo != null) somma += selettore(equip.Corpo);
            if (equip.Gambe != null) somma += selettore(equip.Gambe);
            if (equip.Accessorio != null) somma += selettore(equip.Accessorio);

            return somma;
        }
    }
}