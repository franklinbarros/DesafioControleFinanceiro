using DesafioControleFinanceiro.DAL.Interfaces;
using DesafioControleFinanceiro.Entidades;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace DesafioControleFinanceiro.Tests.Repositorio
{
    public class LancamentoRepositoryTests
    {
        Mock<ILancamentoRepository> lancamentoRepositoryMock;

        public LancamentoRepositoryTests()
        {
            lancamentoRepositoryMock = new Mock<ILancamentoRepository>();
        }

        [Fact]
        public void CadastrarSubcategoriaTest()
        {
            var lancamento = new LancamentoEntity()
            {
                id_subcategoria = 1,
                comentario = "subcategoria teste",
                id_lancamento = 1,
                valor = 10.00M,
                data = DateTime.Today
            };

            lancamentoRepositoryMock.Setup(x => x.Inserir(lancamento)).Returns(new Retorno() { ocorreuErro = false });
            var retorno = lancamentoRepositoryMock.Object.Inserir(lancamento);
            Assert.False(retorno.ocorreuErro);
        }

        [Fact]
        public void AtualizarSubcategoriaTest()
        {
            var lancamento = new LancamentoEntity()
            {
                id_subcategoria = 1,
                comentario = "subcategoria teste",
                id_lancamento = 1,
                valor = 10.00M,
                data = DateTime.Today
            };

            lancamentoRepositoryMock.Setup(x => x.Atualizar(lancamento)).Returns(new Retorno() { ocorreuErro = false }) ;
            var retorno = lancamentoRepositoryMock.Object.Atualizar(lancamento);
            Assert.False(retorno.ocorreuErro);
        }

        [Fact]
        public void ExcluirSubcategoriaTest()
        {
            lancamentoRepositoryMock.Setup(x => x.Excluir(It.IsAny<int>())).Returns(new Retorno() { ocorreuErro = false });
            var retorno = lancamentoRepositoryMock.Object.Excluir(1);
            Assert.False(retorno.ocorreuErro);
        }
    }
}
