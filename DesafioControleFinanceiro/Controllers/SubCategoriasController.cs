using DesafioControleFinanceiro.DAL.Interfaces;
using DesafioControleFinanceiro.Entidades;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DesafioControleFinanceiro.Controllers
{
    public class SubCategoriasController : BaseController
    {
        private readonly ISubcategoriaRepository _subcategoriaRepository;

        public SubCategoriasController(ISubcategoriaRepository subcategoriaRepository)
        {
            _subcategoriaRepository = subcategoriaRepository;
        }

        [HttpGet("{id}")]
        public ActionResult Get([FromRoute] int id)
        {
            try
            {
                var retorno = _subcategoriaRepository.ObterPorId(id);
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
                var retorno = _subcategoriaRepository.ListarTodos();
                return Ok(retorno);
            }
            catch (Exception ex)
            {
                return BadRequest(new Retorno() { codigo = "erro", mensagem = ex.Message });
            }
        }

        [HttpPost]
        public ActionResult Post([FromBody] SubcategoriaEntity subcategoria)
        {
            try
            {
                List<Retorno> validacoes = new List<Retorno>();
                if (String.IsNullOrEmpty(subcategoria.nome))
                    validacoes.Add(new Retorno()
                    {
                        ocorreuErro = true,
                        codigo = "erro_validacao",
                        mensagem = "O nome é obrigatório"
                    });

                if (!subcategoria.id_categoria.HasValue)
                    validacoes.Add(new Retorno()
                    {
                        ocorreuErro = true,
                        codigo = "erro_validacao",
                        mensagem = "O id_categoria é obrigatório"
                    });

                if (validacoes.Count > 0)
                    return StatusCode(StatusCodes.Status400BadRequest, validacoes);

                var retorno = _subcategoriaRepository.Inserir(subcategoria);
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
        public ActionResult Put([FromBody] SubcategoriaEntity subcategoria, [FromRoute] int id)
        {
            try
            {
                List<Retorno> validacoes = new List<Retorno>();
                if (String.IsNullOrEmpty(subcategoria.nome))
                    validacoes.Add(new Retorno()
                    {
                        ocorreuErro = true,
                        codigo = "erro_validacao",
                        mensagem = "O nome é obrigatório"
                    });

                if (!subcategoria.id_categoria.HasValue)
                    validacoes.Add(new Retorno()
                    {
                        ocorreuErro = true,
                        codigo = "erro_validacao",
                        mensagem = "O id_categoria é obrigatório"
                    });

                if (validacoes.Count > 0)
                    return StatusCode(StatusCodes.Status400BadRequest, validacoes);

                subcategoria.id_subcategoria = id;
                var retorno = _subcategoriaRepository.Atualizar(subcategoria);
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
                var retorno = _subcategoriaRepository.Excluir(id);
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
