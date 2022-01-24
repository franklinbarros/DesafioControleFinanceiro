using Dapper;
using DesafioControleFinanceiro.DAL.Interfaces;
using DesafioControleFinanceiro.Entidades;
using Microsoft.Extensions.Configuration;
using System;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace DesafioControleFinanceiro.DAL.Repositorio
{
    public class BalancoRepository : IBalancoRepository
    {
        private readonly string _connectionString;
        public BalancoRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DesafioConcexao");
        }
        public BalancoEntity ObterBalanco(DateTime dataInicio, DateTime dataFim, int? idCategoria)
        {
            StringBuilder comandoSql = new StringBuilder();
            comandoSql.Append(" select despesa = (select sum(valor) from Lancamento l inner join Subcategoria s on s.id_subcategoria = l.id_subcategoria inner join categoria c on c.id_categoria = s.id_categoria where (@id_categoria is null or c.id_categoria = @id_categoria) and data between @dataInicio and @dataFim and valor < 0 ),");
            comandoSql.Append(" receita = (select sum(valor) from Lancamento l inner join Subcategoria s on s.id_subcategoria = l.id_subcategoria inner join categoria c on c.id_categoria = s.id_categoria where (@id_categoria is null or c.id_categoria = @id_categoria) and data between @dataInicio and @dataFim and valor > 0 );");
            comandoSql.Append(" select * from Categoria where id_categoria = @id_categoria");

            using (var connection = new SqlConnection(_connectionString))
            {
                using (var multi = connection.QueryMultiple(comandoSql.ToString(), new { id_categoria = idCategoria, dataInicio = dataInicio, dataFim = dataFim }))
                {
                    var balanco = multi.Read<BalancoEntity>().First();
                    if (idCategoria != null)
                    {
                        var categoria = multi.Read<CategoriaEntity>().First();
                        balanco.categoria = categoria;
                    }
                    balanco.saldo = balanco.receita - Math.Abs(balanco.despesa);

                    if (balanco == null)
                        throw new Exception("Não foi encontrado lançamento para o ID informado.");
                    else
                        return balanco;
                }
            }
        }
    }
}
