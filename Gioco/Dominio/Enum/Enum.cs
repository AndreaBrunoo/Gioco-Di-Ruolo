namespace Gioco.Dominio.Enum
{
    public enum Rarita
    {
        Comune,
        NonComune,
        Raro,
        Epico,
        Leggendario
    }

    public enum TipoAbilita
    {
        Attiva,
        Passiva
    }

    public enum StatoQuest
    {
        NonIniziata,
        InCorso,
        Completata,
        Consegnata
    }

    public enum ClassePersonaggio
    {
        Guerriero,
        Mago,
        Ladro,
        Tank,
        Arcere,
    }

    public enum TipoElemento
    {
        Neutro,
        Fuoco,
        Ghiaccio,
        Veleno,
        Sacro,
        Ombra
    }

    public enum TipoAttacco
    {
        CorpoACorpo,
        Distanza,
        Magico
    }

    public enum TipoOggetto
    {
        Arma,
        Armatura,
        Consumabile,
        Varie
    }

    public enum SlotEquipaggiamento
    {
        Nessuno,
        Arma,
        Testa,
        Corpo,
        Gambe,
        Accessorio
    }

    public enum TipoStatus
    {
        Avvelenato,
        Scottato,
        Stordito,
        Sanguinamento,
        Rallentato
    }

    public enum TipoCella
    {
        // Mappa 1

        Piazza,
        Taverna,
        Mercante,
        Tempio,
        Uscita,

        // Mappa 2

        Strada,
        BoscoFitto,
        Radura,
        Grotta,
        Arena,

        // Mappa 3

        Campi,
        Accampamento,
        Rovine,

        // Mappa 4

        SentieroMontano,
        Caverna,
        Ponte,

        // Mappa 5

        Corridoio,
        Sala,
        Altare,

        // Per test

        Villaggio,
        Dungeon,
    }
}