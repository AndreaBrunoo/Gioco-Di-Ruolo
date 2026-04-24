using System.Collections.Generic;
using Gioco.Dominio.Enum;

namespace Gioco.Dominio.Modelli
{
    public class Personaggio
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;

        public TipoClassePersonaggio Classe { get; set; }

        public int Livello { get; set; }
        public int Esperienza { get; set; }

        public Statistiche Statistiche { get; set; } = new();

        public Dictionary<TipoElemento, double> MoltiplicatoriElementali { get; set; } = new();

        public List<EffettoStatus> StatusAttivi { get; set; } = new();

        public List<Attacco> Attacchi { get; set; } = new();

        public Inventario Inventario { get; set; } = new();
        public Equipaggiamento Equipaggiamento { get; set; } = new();

        public int Monete { get; set; }

        public int PosX { get; set; }
        public int PosY { get; set; }
        public int IdMappa { get; set; }
    }
}