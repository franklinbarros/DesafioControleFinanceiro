using Dapper;
using DesafioControleFinanceiro.DAL.Interfaces;
using DesafioControleFinanceiro.Entidades;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace DesafioControleFinanceiro.DAL.Repositorio
{
    public class SubcategoriaRepository : ISubcategoriaRepository
    {
        private readonly string _connectionString;
        public SubcategoriaRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DesafioConcexao");
        }

        public Retorno Atualizar(SubcategoriaEntity subcategoria)
        {
            string comandoSql = "update SubCategoria set nome = @nome, id_categoria = @id_categoria where id_subcategoria = @id";
            using (var connection = new SqlConnection(_connectionString))
            {
                var qtdLinhasAfetadas = connection.Execute(comandoSql, new { nome = subcategoria.nome, id_categoria = subcategoria.id_categoria, id = subcategoria.id_subcategoria });

                if (qtdLinhasAfetadas == 0)
                    throw new Exception("Ocorreu erro ao realizar o update no SQL");
                else
                    return new Retorno() { ocorreuErro = false };
            }
        }

        public Retorno Excluir(int id)
        {
            string comandoSql = "delete from Subcategoria where id_subcategoria = @id";
            using (var connection = new SqlConnection(_connectionString))
            {
                var qtdLinhasAfetadas = connection.Execute(comandoSql, new { id = id });

                if (qtdLinhasAfetadas == 0)
                    throw new Exception("Ocorreu erro ao realizar o delete no SQL");
                else
                    return new Retorno() { ocorreuErro = false };
            }
        }

        public Retorno Inserir(SubcategoriaEntity subcategoria)
        {
            string comandoSql = "insert into Subcategoria (nome, id_categoria) values (@nome, @id_categoria)";
            using (var connection = new SqlConnection(_connectionString))
            {
                var qtdLinhasAfetadas = connection.Execute(comandoSql, new { nome = subcategoria.nome, id_categoria = subcategoria.id_categoria });

                if (qtdLinhasAfetadas == 0)
                    throw new Exception("Ocorreu erro ao realizar o insert no SQL");
                else
                    return new Retorno() { ocorreuErro = false };
            }
        }

        public IEnumerable<SubcategoriaEntity> ListarTodos()
        {
            string comandoSql = "select * from Subcategoria";
            using (var connection = new SqlConnection(_connectionString))
            {
                var subcategorias = connection.Query<SubcategoriaEntity>(comandoSql).ToList();

                if (subcategorias.Count == 0)
                    throw new Exception("Não possui subcategorias cadastradas para serem listadas.");
                else
                    return subcategorias;
            }
        }

        public SubcategoriaEntity ObterPorId(int id)
        {
            string comandoSql = "select * from Subcategoria where id_subcategoria = @id";
            using (var connection = new SqlConnection(_connectionString))
            {
                var categoria = connection.QuerySingleOrDefault<SubcategoriaEntity>(comandoSql, new { id = id });

                if (categoria == null)
                    throw new Exception("Não foi encontrada subcategoria para o ID informado.");
                else
                    return categoria;
            }
        }
    }
}
