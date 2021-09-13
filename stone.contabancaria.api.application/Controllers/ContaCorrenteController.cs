using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using stone.contabancaria.api.application.Models;
using stone.contabancaria.api.core.Entities;
using stone.contabancaria.api.core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace stone.contabancaria.api.application.Controllers
{
    [Route("conta-corrente")]
    [ApiController]
    public class ContaCorrenteController : ControllerBase
    {
        private readonly IContaCorrenteService _contaCorrenteService;
        private readonly IMapper _mapper;

        public ContaCorrenteController(IContaCorrenteService contaCorrenteService, IMapper mapper)
        {
            _contaCorrenteService = contaCorrenteService;
            _mapper = mapper;
        }

        // POST api/values
        [HttpPost("depositar")]
        public async Task<IActionResult> Depositar([FromBody] DepositarModel depositarModel)
        {
            try
            {
                await _contaCorrenteService.Depositar(depositarModel.ContaCorrente, depositarModel.Valor, DateTime.Now);       
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();

        }
        [HttpPost("sacar")]
        public async Task<IActionResult> Sacar([FromBody] SacarModel sacarModel)
        {
            try
            {
                await _contaCorrenteService.Sacar(sacarModel.ContaCorrente, sacarModel.Valor, DateTime.Now);        
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();
        }
        [HttpPost("transferir")]
        public async Task<IActionResult> Transferir([FromBody] TransferirModel transferirModel)
        {
            try
            {
                await _contaCorrenteService.Transferir(transferirModel.ContaBeneficiario, transferirModel.ContaCorrente, transferirModel.Valor, DateTime.Now);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok();
        }
        [HttpGet("extrato/{contaCorrente}")]
        public async Task<IActionResult> Extrato(string contaCorrente, DateTime inicio, DateTime fim)
        {
            List<Transacao> extrato;
            try
            {
                 extrato = await _contaCorrenteService.Extrato(contaCorrente, inicio, fim);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
           
            return Ok(extrato);
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ContaCorrentePostModel depositarModel)
        {
            ContaCorrente contaCorrente;
            try
            {
                var conta = _mapper.Map<ContaCorrente>(depositarModel);
                contaCorrente = await _contaCorrenteService.NovaContaCorrente(conta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
           
            return Ok(contaCorrente);
        }

    }
}
