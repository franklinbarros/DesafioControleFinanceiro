using DesafioControleFinanceiro.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace DesafioControleFinanceiro.DAL.Interfaces
{
    public interface IBalancoRepository
    {
        public BalancoEntity ObterBalanco(DateTime dataInicio, DateTime dataFim, int? idCategoria);
    }
}
