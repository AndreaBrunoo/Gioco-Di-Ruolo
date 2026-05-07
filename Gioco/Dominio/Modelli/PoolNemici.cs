using System.Collections.Generic;

namespace Gioco.Dominio.Modelli
{
    public class PoolNemici
    {
        public int Id { get; set; }
        public string Nome { get; set; } = "";
        public List<Nemico> Nemici { get; set; } = new();
    }
}