using DesafioControleFinanceiro.API.Atributos;
using DesafioControleFinanceiro.DAL.Interfaces;
using DesafioControleFinanceiro.Entidades;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace DesafioControleFinanceiro.Controllers
{
    [ApiKeyAttibute]
    public class CategoriasController : BaseController
    {
        private readonly ICategoriaRepository _categoriaRepository;

        public CategoriasController(ICategoriaRepository categoriaRepository)
        {
            _categoriaRepository = categoriaRepository;
        }

        [HttpGet("{id}")]
        public ActionResult Get([FromRoute] int id)
        {
            try
            {
                var retorno = _categoriaRepository.ObterPorId(id);
                return Ok(retorno);
            }
            catch (Exception ex)
            {
                return BadRequest(new Retorno() { codigo = "erro", mensagem = ex.Message });
            }
        }

        [HttpGet]
        public ActionResult Get()
        {
            try
            {
                var retorno = _categoriaRepository.ListarTodos();
                return Ok(retorno);
            }
            catch (Exception ex)
            {
                return BadRequest(new Retorno() { codigo = "erro", mensagem = ex.Message });
            }
        }

        [HttpPost]
        public ActionResult Post([FromBody] CategoriaEntity categoria)
        {
            try
            {
                if (String.IsNullOrEmpty(categoria.nome))
                    return StatusCode(StatusCodes.Status400BadRequest, new Retorno()
                    {
                        ocorreuErro = true,
                        codigo = "erro_validacao",
                        mensagem = "O nome é obrigatório"
                    });

                var retorno = _categoriaRepository.Inserir(categoria.nome);
                if (retorno.ocorreuErro)
                    return BadRequest(retorno);
                else
                    return StatusCode(StatusCodes.Status201Created);
            }
            catch (Exception ex)
            {
                return BadRequest(new Retorno() { codigo = "erro", mensagem = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public ActionResult Put([FromBody] CategoriaEntity categoria, [FromRoute] int id)
        {
            try
            {
                if (String.IsNullOrEmpty(categoria.nome))
                    return StatusCode(StatusCodes.Status400BadRequest, new Retorno()
                    {
                        ocorreuErro = true,
                        codigo = "erro_validacao",
                        mensagem = "O nome é obrigatório"
                    });

                categoria.id_categoria = id;
                var retorno = _categoriaRepository.Atualizar(categoria);
                if (retorno.ocorreuErro)
                    return BadRequest(retorno);
                else
                    return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new Retorno() { codigo = "erro", mensagem = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public ActionResult Delete([FromRoute] int id)
        {
            try
            {
                var retorno = _categoriaRepository.Excluir(id);
                if (retorno.ocorreuErro)
                    return BadRequest(retorno);
                else
                    return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new Retorno() { codigo = "erro", mensagem = ex.Message });
            }
        }

    }
}
