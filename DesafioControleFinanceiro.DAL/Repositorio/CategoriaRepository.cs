using DesafioControleFinanceiro.DAL.Interfaces;
using DesafioControleFinanceiro.Entidades;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using Dapper;
using System.Data.SqlClient;
using System.Linq;

namespace DesafioControleFinanceiro.DAL.Repositorio
{
    public class CategoriaRepository : ICategoriaRepository
    {
        private readonly string _connectionString;

        public CategoriaRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DesafioConcexao");
        }
        public Retorno Atualizar(CategoriaEntity categoria)
        {
            string comandoSql = "update Categoria set nome = @nome where id_categoria = @id";
            using (var connection = new SqlConnection(_connectionString))
            {
                var qtdLinhasAfetadas = connection.Execute(comandoSql, new { nome = categoria.nome, id = categoria.id_categoria });

                if (qtdLinhasAfetadas == 0)
                    throw new Exception("Ocorreu erro ao realizar o update no SQL");
                else
                    return new Retorno() { ocorreuErro = false };
            }
        }

        public Retorno Excluir(int id)
        {
            string comandoSql = "delete from Categoria where id_categoria = @id";
            using (var connection = new SqlConnection(_connectionString))
            {
                var qtdLinhasAfetadas = connection.Execute(comandoSql, new { id = id });

                if (qtdLinhasAfetadas == 0)
                    throw new Exception("Ocorreu erro ao realizar o delete no SQL");
                else
                    return new Retorno() { ocorreuErro = false };
            }
        }

        public Retorno Inserir(string nome)
        {           
            string comandoSql = "insert into Categoria (nome) values (@nome)"; 
            using (var connection = new SqlConnection(_connectionString))
            {
                var qtdLinhasAfetadas = connection.Execute(comandoSql, new {nome = nome });

                if (qtdLinhasAfetadas == 0)
                    throw new Exception("Ocorreu erro ao realizar o insert no SQL");
                else
                    return new Retorno() { ocorreuErro = false };
            }
        }

        public IEnumerable<CategoriaEntity> ListarTodos()
        {
            string comandoSql = "select * from Categoria";
            using (var connection = new SqlConnection(_connectionString))
            {
                var categorias = connection.Query<CategoriaEntity>(comandoSql).ToList();

                if (categorias.Count == 0)
                    throw new Exception("Não possui categorias cadastradas para serem listadas.");
                else
                    return categorias;
            }
        }

        public CategoriaEntity ObterPorId(int id)
        {
            string comandoSql = "select * from Categoria where id_categoria = @id";
            using (var connection = new SqlConnection(_connectionString))
            {
                var categoria = connection.QuerySingleOrDefault<CategoriaEntity>(comandoSql, new { id = id});

                if (categoria == null)
                    throw new Exception("Não foi encontrada categoria para o ID informado.");
                else
                    return categoria;
            }
        }
    }
}
