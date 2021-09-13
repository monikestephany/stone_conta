using stone.contabancaria.api.core.Contants;
using stone.contabancaria.api.core.Entities;
using stone.contabancaria.api.core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace stone.contabancaria.api.core.Services
{
    public class ContaCorrenteService : IContaCorrenteService
    {
        private readonly ITransacaoRepository _transacaoRepository;
        private readonly IContaCorrenteRepository _correnteRepository;
        public ContaCorrenteService(ITransacaoRepository transacaoRepository, IContaCorrenteRepository correnteRepository)
        {
            _correnteRepository = correnteRepository;
            _transacaoRepository = transacaoRepository;
        }

        public async Task Depositar(string contaCorrente, decimal valor, DateTime dateTime)
        {
            var conta = await _correnteRepository.BuscarContaCorrenteAsync(contaCorrente);
            if (conta == null)
            {
                throw new Exception(Erros.ContaInvalida);
            }
            conta.Depositar(valor);
            await _correnteRepository.AtualizarContaCorrente(conta);
            var transacao = new Transacao().GerarTransacao(contaCorrente, valor, conta.ValorCalculado, Operacoes.Deposito, Operacoes.Credito, dateTime);
            await _transacaoRepository.InserirTransacao(transacao);

        }
        public async Task Sacar(string contaCorrente, decimal valor, DateTime dateTime)
        {
            var conta = await _correnteRepository.BuscarContaCorrenteAsync(contaCorrente);
            if (conta == null)
            {
                throw new Exception(Erros.ContaInvalida);
            }
            conta.Sacar(valor);
            await _correnteRepository.AtualizarContaCorrente(conta);
            var transacao = new Transacao().GerarTransacao(contaCorrente, valor, conta.ValorCalculado, Operacoes.Sacar, Operacoes.Debito, dateTime);
            await _transacaoRepository.InserirTransacao(transacao);
        }
        public async Task<ContaCorrente> NovaContaCorrente(ContaCorrente contaCorrente)
        {
            return await _correnteRepository.CriarContaCorrenteAsync(contaCorrente);
        }
        public async Task Transferir(string contaBeneficiario, string contaCorrente, decimal valor, DateTime dateTime)
        {
            var transacao = await EnviaTranferencia(contaBeneficiario, contaCorrente, valor, dateTime);

            await RecebeTranferencia(contaCorrente, contaBeneficiario, valor, transacao.ValorCalculado, dateTime);        
        }
        public async Task<List<Transacao>> Extrato(string contaCorrente, DateTime inicio, DateTime fim)
        {
            return await _transacaoRepository.ListarTransacoes(contaCorrente, inicio, fim);
        }
        private async Task<Transacao> EnviaTranferencia(string contaBeneficiario, string contaCorrente, decimal valor, DateTime dateTime)
        {
            try
            {
                var contaEnvia = await _correnteRepository.BuscarContaCorrenteAsync(contaCorrente);
                if (contaEnvia == null)
                {
                    throw new Exception(Erros.ContaInvalida);
                }
                contaEnvia.Transferir(valor, "D");
                await _correnteRepository.AtualizarContaCorrente(contaEnvia);
                var transacao = new Transacao().GerarTransacao(contaCorrente, valor, contaEnvia.ValorCalculado, contaBeneficiario, Operacoes.Transferir, Operacoes.Debito, dateTime);
                return await _transacaoRepository.InserirTransacao(transacao);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private async Task RecebeTranferencia(string contaCorrente, string contaBeneficiario, decimal valor, decimal valorCalculado, DateTime dateTime)
        {
            try
            {
                var contaRecebe = await _correnteRepository.BuscarContaCorrenteAsync(contaBeneficiario);
                if (contaRecebe == null)
                {
                    throw new Exception(Erros.ContaInvalida);
                }
                contaRecebe.Transferir(valor, "C");
                await _correnteRepository.AtualizarContaCorrente(contaRecebe);
                var transacao = new Transacao().GerarTransacao(contaBeneficiario, valor, contaRecebe.ValorCalculado, Operacoes.Transferir, Operacoes.Credito, dateTime);
                await _transacaoRepository.InserirTransacao(transacao);
            }
            catch (Exception)
            {
                await EstornaTranferencia(valorCalculado, contaCorrente, dateTime);
                throw new Exception(Erros.OperacaoInvalida);
            }
        }
        private async Task EstornaTranferencia(decimal valorCalculado, string contaCorrente, DateTime dateTime)
        {
            var conta = await _correnteRepository.BuscarContaCorrenteAsync(contaCorrente);
            conta.Transferir(valorCalculado, "C");
            await _correnteRepository.AtualizarContaCorrente(conta);
            var transacao = new Transacao().GerarTransacao(contaCorrente, valorCalculado, conta.ValorCalculado, Operacoes.Estorno, Operacoes.Credito, dateTime);
            await _transacaoRepository.InserirTransacao(transacao);
        }
    }
}
