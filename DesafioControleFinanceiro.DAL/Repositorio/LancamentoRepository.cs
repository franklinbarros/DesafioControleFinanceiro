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
    public class LancamentoRepository : ILancamentoRepository
    {
        private readonly string _connectionString;
        public LancamentoRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DesafioConcexao");
        }

        public Retorno Atualizar(LancamentoEntity lancamento)
        {
            string comandoSql = "update Lancamento set valor = @valor, data = @data, id_subcategoria = @id_subcategoria, comentario = @comentario where id_lancamento = @id";
            using (var connection = new SqlConnection(_connectionString))
            {
                var qtdLinhasAfetadas = connection.Execute(comandoSql, 
                    new 
                    { 
                        valor = lancamento.valor.Value,
                        id_subcategoria = lancamento.id_subcategoria.Value, 
                        comentario = lancamento.comentario ,
                        data = lancamento.data.Value,
                        id = lancamento.id_lancamento
                    });

                if (qtdLinhasAfetadas == 0)
                    throw new Exception("Ocorreu erro ao realizar o update no SQL");
                else
                    return new Retorno() { ocorreuErro = false };
            }
        }

        public Retorno Excluir(int id)
        {
            string comandoSql = "delete from Lancamento where id_lancamento = @id";
            using (var connection = new SqlConnection(_connectionString))
            {
                var qtdLinhasAfetadas = connection.Execute(comandoSql, new { id = id });

                if (qtdLinhasAfetadas == 0)
                    throw new Exception("Ocorreu erro ao realizar o delete no SQL");
                else
                    return new Retorno() { ocorreuErro = false };
            }
        }

        public Retorno Inserir(LancamentoEntity lancamento)
        {
            string comandoSql = "insert into Lancamento (valor,data, id_subcategoria, comentario) values (@valor, @data, @id_subcategoria, @comentario)";
            using (var connection = new SqlConnection(_connectionString))
            {
                var qtdLinhasAfetadas = connection.Execute(comandoSql, new { valor = lancamento.valor, data = lancamento.data.Value, id_subcategoria = lancamento.id_subcategoria.Value, comentario = lancamento.comentario });

                if (qtdLinhasAfetadas == 0)
                    throw new Exception("Ocorreu erro ao realizar o insert no SQL");
                else
                    return new Retorno() { ocorreuErro = false };
            }
        }

        public IEnumerable<LancamentoEntity> ListarTodos()
        {
            string comandoSql = "select * from lancamento";
            using (var connection = new SqlConnection(_connectionString))
            {
                var lancamentos = connection.Query<LancamentoEntity>(comandoSql).ToList();

                if (lancamentos.Count == 0)
                    throw new Exception("Não possui lancamentos cadastrados para serem listadas.");
                else
                    return lancamentos;
            }
        }

        public LancamentoEntity ObterPorId(int id)
        {
            string comandoSql = "select * from Lancamento where id_lancamento = @id";
            using (var connection = new SqlConnection(_connectionString))
            {
                var lancamento = connection.QuerySingleOrDefault<LancamentoEntity>(comandoSql, new { id = id });

                if (lancamento == null)
                    throw new Exception("Não foi encontrado lançamento para o ID informado.");
                else
                    return lancamento;
            }
        }
    }
}
