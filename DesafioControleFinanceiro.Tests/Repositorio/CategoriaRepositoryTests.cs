using DesafioControleFinanceiro.DAL.Interfaces;
using DesafioControleFinanceiro.Entidades;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace DesafioControleFinanceiro.Tests.Repositorio
{
    public class CategoriaRepositoryTests
    {
        Mock<ICategoriaRepository> categoriaRepositoryMock;

        public CategoriaRepositoryTests()
        {
            categoriaRepositoryMock = new Mock<ICategoriaRepository>();
        }

        [Fact]
        public void CadastrarCategoriaTest()
        {
            var retorno = new Retorno();
            categoriaRepositoryMock.Setup(x => x.Inserir(It.IsAny<string>())).Returns(retorno);
            var retorno2 = categoriaRepositoryMock.Object.Inserir("teste");
            Assert.False(retorno2.ocorreuErro);
        }

        [Fact]
        public void AtualizarCategoriaTest()
        {
            var categoria = new CategoriaEntity()
            {
                nome = "teste",
                id_categoria = 1
            };

            categoriaRepositoryMock.Setup(x => x.Atualizar(categoria)).Returns(new Retorno() { ocorreuErro = false }) ;
            var retorno = categoriaRepositoryMock.Object.Atualizar(categoria);
            Assert.False(retorno.ocorreuErro);
        }

        [Fact]
        public void ExcluirCategoriaTest()
        {
            categoriaRepositoryMock.Setup(x => x.Excluir(It.IsAny<int>())).Returns(new Retorno() { ocorreuErro = false });
            var retorno = categoriaRepositoryMock.Object.Excluir(1);
            Assert.False(retorno.ocorreuErro);
        }
    }
}
