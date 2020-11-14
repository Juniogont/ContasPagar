using System.ComponentModel.DataAnnotations;

namespace TesteContasPagar.Models
{
    public class RegraAtraso
    {
        public int Id { get; set; }
        public int DiasAtraso { get; set; }
        [StringLength(50)]
        public string Descricao { get; set; }
        public decimal Multa { get; set; }
        public decimal JurosDia { get; set; }
    }
}
