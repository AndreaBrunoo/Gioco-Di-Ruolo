namespace Gioco.Dominio.Enum
{
    public enum TipoCella
    {
        Strada,
        Villaggio,
        Dungeon,
        StanzaBoss,
        Minigioco
    }

    public enum TipoClassePersonaggio
    {
        Guerriero,
        Mago,
        Ladro,
        Chierico,
        Ranger,
        Paladino,
        Barbaro
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
}