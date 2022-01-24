using DesafioControleFinanceiro.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace DesafioControleFinanceiro.DAL.Interfaces
{
    public interface ILancamentoRepository
    {
        public IEnumerable<LancamentoEntity> ListarTodos();
        public LancamentoEntity ObterPorId(int id);
        public Retorno Inserir(LancamentoEntity lancamento);
        public Retorno Atualizar(LancamentoEntity lancamento);
        public Retorno Excluir(int id);
    }
}
