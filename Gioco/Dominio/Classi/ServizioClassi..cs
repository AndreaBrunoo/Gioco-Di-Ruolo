using Gioco.Dominio.Enum;
using Gioco.Dominio.Modelli;

namespace Gioco.Dominio.Classi
{
    public class ServizioClassi
    {
        public void ApplicaStatisticheBase(Personaggio personaggio)
        {
            var stats = personaggio.Statistiche;

            switch (personaggio.Classe)
            {
                case ClassePersonaggio.Guerriero:
                    stats.SaluteMassima = 120;
                    stats.Attacco = 20;
                    stats.Difesa = 15;
                    stats.Velocita = 10;
                    stats.ManaMassimo = 20;
                    break;

                case ClassePersonaggio.Mago:
                    stats.SaluteMassima = 80;
                    stats.Attacco = 10;
                    stats.Difesa = 8;
                    stats.Velocita = 12;
                    stats.ManaMassimo = 60;
                    break;

                case ClassePersonaggio.Ladro:
                    stats.SaluteMassima = 90;
                    stats.Attacco = 15;
                    stats.Difesa = 10;
                    stats.Velocita = 20;
                    stats.ManaMassimo = 25;
                    break;

                case ClassePersonaggio.Tank:
                    stats.SaluteMassima = 150;
                    stats.Attacco = 12;
                    stats.Difesa = 25;
                    stats.Velocita = 5;
                    stats.ManaMassimo = 15;
                    break;

                case ClassePersonaggio.Arcere:
                    stats.SaluteMassima = 100;
                    stats.Attacco = 18;
                    stats.Difesa = 10;
                    stats.Velocita = 18;
                    stats.ManaMassimo = 30;
                    break;
            }

            stats.SaluteAttuale = stats.SaluteMassima;
            stats.ManaAttuale = stats.ManaMassimo;
        }

        public List<Abilita> AbilitaIniziali(ClassePersonaggio classe)
        {
            return classe switch
            {
                ClassePersonaggio.Guerriero => new List<Abilita>
                {
                    new Abilita
                    {
                        Id = 100,
                        Nome = "Colpo Potente",
                        Tipo = TipoAbilita.Attiva,
                        Potenza = 35,
                        CostoMana = 5,
                        Descrizione = "Un colpo fisico devastante."
                    }
                },

                ClassePersonaggio.Mago => new List<Abilita>
                {
                    new Abilita
                    {
                        Id = 200,
                        Nome = "Dardo Magico",
                        Tipo = TipoAbilita.Attiva,
                        Potenza = 45,
                        CostoMana = 10,
                        Elemento = TipoElemento.Fuoco,
                        Descrizione = "Un proiettile magico che colpisce sempre."
                    }
                },

                ClassePersonaggio.Ladro => new List<Abilita>
                {
                    new Abilita
                    {
                        Id = 300,
                        Nome = "Pugnalata Rapida",
                        Tipo = TipoAbilita.Attiva,
                        Potenza = 25,
                        CostoMana = 4,
                        Descrizione = "Un attacco veloce con alta probabilità di critico."
                    }
                },

                ClassePersonaggio.Tank => new List<Abilita>
                {
                    new Abilita
                    {
                        Id = 400,
                        Nome = "Provocazione",
                        Tipo = TipoAbilita.Passiva,
                        BonusDifesa = 5,
                        Descrizione = "Aumenta la difesa e attira l'attenzione dei nemici."
                    }
                },

                ClassePersonaggio.Arcere => new List<Abilita>
                {
                    new Abilita
                    {
                        Id = 500,
                        Nome = "Freccia Penetrante",
                        Tipo = TipoAbilita.Attiva,
                        Potenza = 30,
                        CostoMana = 6,
                        Descrizione = "Una freccia che ignora parte della difesa."
                    }
                },

                _ => new List<Abilita>()
            };
        }

    }
}