using DesafioControleFinanceiro.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace DesafioControleFinanceiro.DAL.Interfaces
{
    public interface ICategoriaRepository
    {
        public IEnumerable<CategoriaEntity> ListarTodos();
        public CategoriaEntity ObterPorId(int id);
        public Retorno Inserir(string nome);
        public Retorno Atualizar(CategoriaEntity categoria);
        public Retorno Excluir(int id);
    }
}
