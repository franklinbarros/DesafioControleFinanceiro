using DesafioControleFinanceiro.DAL.Interfaces;
using DesafioControleFinanceiro.Entidades;
using Microsoft.AspNetCore.Mvc;
using System;

namespace DesafioControleFinanceiro.Controllers
{
    public class BalancoController : BaseController
    {
        private readonly IBalancoRepository _balancoRepository;

        public BalancoController(IBalancoRepository balancoRepository)
        {
            _balancoRepository = balancoRepository;
        }

        [HttpGet]
        public ActionResult Get([FromQuery] int? id_categoria, DateTime dataInicio, DateTime dataFim)
        {
            try
            {
                var retorno = _balancoRepository.ObterBalanco(dataInicio,dataFim, id_categoria);
                return Ok(retorno);
            }
            catch (Exception ex)
            {
                return BadRequest(new Retorno() { codigo = "erro", mensagem = ex.Message });
            }
        }
    }
}
