using DesafioControleFinanceiro.DAL.Interfaces;
using DesafioControleFinanceiro.Entidades;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace DesafioControleFinanceiro.Tests.Repositorio
{
    public class SubcategoriaRepositoryTests
    {
        Mock<ISubcategoriaRepository> subcategoriaRepositoryMock;

        public SubcategoriaRepositoryTests()
        {
            subcategoriaRepositoryMock = new Mock<ISubcategoriaRepository>();
        }

        [Fact]
        public void CadastrarSubcategoriaTest()
        {
            var subcategoria = new SubcategoriaEntity()
            {
                nome = "subcategoria teste",
                id_categoria = 1
            };

            subcategoriaRepositoryMock.Setup(x => x.Inserir(subcategoria)).Returns(new Retorno() { ocorreuErro = false });
            var retorno = subcategoriaRepositoryMock.Object.Inserir(subcategoria);
            Assert.False(retorno.ocorreuErro);
        }

        [Fact]
        public void AtualizarSubcategoriaTest()
        {
            var subcategoria = new SubcategoriaEntity()
            {
                id_subcategoria = 1,
                nome = "subcategoria teste",
                id_categoria = 1
            };

            subcategoriaRepositoryMock.Setup(x => x.Atualizar(subcategoria)).Returns(new Retorno() { ocorreuErro = false }) ;
            var retorno = subcategoriaRepositoryMock.Object.Atualizar(subcategoria);
            Assert.False(retorno.ocorreuErro);
        }

        [Fact]
        public void ExcluirSubcategoriaTest()
        {
            subcategoriaRepositoryMock.Setup(x => x.Excluir(It.IsAny<int>())).Returns(new Retorno() { ocorreuErro = false });
            var retorno = subcategoriaRepositoryMock.Object.Excluir(1);
            Assert.False(retorno.ocorreuErro);
        }
    }
}
