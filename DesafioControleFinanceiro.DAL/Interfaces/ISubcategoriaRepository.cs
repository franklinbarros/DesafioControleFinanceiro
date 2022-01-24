using DesafioControleFinanceiro.Entidades;
using System.Collections.Generic;

namespace DesafioControleFinanceiro.DAL.Interfaces
{
    public interface ISubcategoriaRepository
    {
        public IEnumerable<SubcategoriaEntity> ListarTodos();
        public SubcategoriaEntity ObterPorId(int id);
        public Retorno Inserir(SubcategoriaEntity subcategoria);
        public Retorno Atualizar(SubcategoriaEntity subcategoria);
        public Retorno Excluir(int id);
    }
}
