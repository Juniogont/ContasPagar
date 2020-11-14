using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TesteContasPagar.Models
{
    [Table("ContaPagar")]
    public class ContaPagar
    {
        public int Id { get; set; }
        [StringLength(100)]
        public string Nome { get; set; }
        public decimal ValorOriginal { get; set; }
        public decimal ValorCorrigido { get; set; }
        public DateTime DataVencimento { get; set; }
        public DateTime DataPagamento { get; set; }
        public int DiasAtraso { get; set; }
        public RegraAtraso RegraAtraso { get; set; }
        public int? RegraAtrasoId { get; set; }

    }
}
