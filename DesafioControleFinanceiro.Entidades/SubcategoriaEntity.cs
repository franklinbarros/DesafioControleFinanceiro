using System;
using System.Collections.Generic;
using System.Text;

namespace DesafioControleFinanceiro.Entidades
{
    public class SubcategoriaEntity
    {
        public int id_subcategoria { get; set; }
        public string nome { get; set; }
        public int? id_categoria { get; set; }
    }
}
