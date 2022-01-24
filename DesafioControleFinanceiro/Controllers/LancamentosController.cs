using DesafioControleFinanceiro.DAL.Interfaces;
using DesafioControleFinanceiro.Entidades;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace DesafioControleFinanceiro.Controllers
{
    public class LancamentosController : BaseController
    {
        private readonly ILancamentoRepository _lancamentoRepository;

        public LancamentosController(ILancamentoRepository lancamentoRepository)
        {
            _lancamentoRepository = lancamentoRepository;
        }

        [HttpGet("{id}")]
        public ActionResult Get([FromRoute] int id)
        {
            try
            {
                var retorno = _lancamentoRepository.ObterPorId(id);
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
                var retorno = _lancamentoRepository.ListarTodos();
                return Ok(retorno);
            }
            catch (Exception ex)
            {
                return BadRequest(new Retorno() { codigo = "erro", mensagem = ex.Message });
            }
        }

        [HttpPost]
        public ActionResult Post([FromBody] LancamentoEntity lancamento)
        {
            try
            {
                List<Retorno> validacoes = new List<Retorno>();
                if (!lancamento.valor.HasValue)
                    validacoes.Add(new Retorno()
                    {
                        ocorreuErro = true,
                        codigo = "erro_validacao",
                        mensagem = "O valor é obrigatório"
                    });

                if (!lancamento.id_subcategoria.HasValue)
                    validacoes.Add(new Retorno()
                    {
                        ocorreuErro = true,
                        codigo = "erro_validacao",
                        mensagem = "O id_subcategoria é obrigatório"
                    });

                if (validacoes.Count > 0)
                    return StatusCode(StatusCodes.Status400BadRequest, validacoes);

                if (!lancamento.data.HasValue)
                {
                    lancamento.data = DateTime.Today;
                }

                var retorno = _lancamentoRepository.Inserir(lancamento);
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
        public ActionResult Put([FromBody] LancamentoEntity lancamento, [FromRoute] int id)
        {
            try
            {
                List<Retorno> validacoes = new List<Retorno>();
                if (!lancamento.valor.HasValue)
                    validacoes.Add(new Retorno()
                    {
                        ocorreuErro = true,
                        codigo = "erro_validacao",
                        mensagem = "O valor é obrigatório"
                    });

                if (!lancamento.id_subcategoria.HasValue)
                    validacoes.Add(new Retorno()
                    {
                        ocorreuErro = true,
                        codigo = "erro_validacao",
                        mensagem = "O id_subcategoria é obrigatório"
                    });

                if (validacoes.Count > 0)
                    return StatusCode(StatusCodes.Status400BadRequest, validacoes);

                if (!lancamento.data.HasValue)
                {
                    lancamento.data = DateTime.Today;
                }

                if (validacoes.Count > 0)
                    return StatusCode(StatusCodes.Status400BadRequest, validacoes);

                lancamento.id_lancamento = id;
                var retorno = _lancamentoRepository.Atualizar(lancamento);
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
                var retorno = _lancamentoRepository.Excluir(id);
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
