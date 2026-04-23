using GiocoRuolo.Models;
using GiocoRuolo.Helpers;

namespace GiocoRuolo.Combat
{
    public static class CalcoloDanniHelper
    {
        public static RisultatoAttacco CalcolaDanno(
            string attaccanteId,
            string bersaglioId,
            Statistiche attStats,
            Statistiche defStats,
            Attacco attacco,
            bool attaccanteIsBoss = false,
            bool armaRara = false)
        {
            var risultato = new RisultatoAttacco
            {
                AttaccanteId = attaccanteId,
                BersaglioId = bersaglioId,
                AttaccoUsato = attacco
            };

            // 1) Probabilità di successo
            if (!RandomHelper.Probabilità(attacco.ProbabilitaSuccesso))
            {
                risultato.Esito = EsitoTurno.Fallito;
                risultato.Log = $"{attaccanteId} ha fallito l'attacco!";
                return risultato;
            }

            // 2) Danno base
            int danno = attacco.DannoBase + attStats.Attacco;

            // 3) Modificatore tipo attacco
            danno = attacco.Tipo switch
            {
                TipoAttacco.Magico => (int)(danno * 1.2),
                TipoAttacco.Elementale => (int)(danno * 1.3),
                TipoAttacco.Distanza => (int)(danno * 0.9),
                TipoAttacco.Critico => (int)(danno * 1.5),
                TipoAttacco.Debuff => (int)(danno * 0.5),
                _ => danno
            };

            // 4) Boss / Arma rara
            if (attaccanteIsBoss)
                danno = (int)(danno * 1.25);

            if (armaRara)
                danno = (int)(danno * 1.15);

            // 5) Difesa
            danno -= defStats.Difesa / 2;
            if (danno < 1) danno = 1;

            // 6) Variazione ±10%
            double var = 1 + (RandomHelper.NextDouble() * 0.2 - 0.1);
            danno = (int)(danno * var);

            // 7) Critico
            bool critico = RandomHelper.Probabilità(attStats.Critico);
            risultato.ColpoCritico = critico;

            if (critico)
                danno = (int)(danno * 1.5);

            // 8) Elemento → Resistenze
            if (attacco.Elemento != Elemento.Nessuno)
                danno = ApplicaResistenze(danno, attacco.Elemento, defStats.Resistenze);

            // 9) Debuff
            if (attacco.Tipo == TipoAttacco.Debuff)
                defStats.Difesa = (int)(defStats.Difesa * 0.9);

            // 10) Effetti di stato
            if (attacco.EffettoApplicato != TipoEffetto.Nessuno)
                defStats.EffettoAttivo = new EffettoStato { Tipo = attacco.EffettoApplicato, Durata = 3 };

            // 11) Risultato finale
            risultato.DanniInflitti = danno;
            risultato.Esito = critico ? EsitoTurno.Critico : EsitoTurno.Successo;

            risultato.Log = critico
                ? $"{attaccanteId} infligge un colpo critico da {danno} danni!"
                : $"{attaccanteId} infligge {danno} danni.";

            return risultato;
        }

        private static int ApplicaResistenze(int danno, Elemento elemento, Resistenze res)
        {
            int mod = elemento switch
            {
                Elemento.Fuoco => res.Fuoco,
                Elemento.Ghiaccio => res.Ghiaccio,
                Elemento.Fulmine => res.Fulmine,
                Elemento.Veleno => res.Veleno,
                _ => 0
            };

            return (int)(danno * (1 - mod / 100.0));
        }
    }
}