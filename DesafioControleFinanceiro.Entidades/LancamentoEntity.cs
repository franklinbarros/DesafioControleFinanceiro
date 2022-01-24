using System;
using System.Collections.Generic;
using System.Text;

namespace DesafioControleFinanceiro.Entidades
{
    public class LancamentoEntity
    {
        public int id_lancamento { get; set; }
        public decimal? valor { get; set; }
        public DateTime? data { get; set; }
        public int? id_subcategoria { get; set; }
        public string comentario { get; set; }
    }
}
