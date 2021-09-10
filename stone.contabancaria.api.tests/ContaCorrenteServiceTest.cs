using Moq;
using stone.contabancaria.api.core.Contants;
using stone.contabancaria.api.core.Entities;
using stone.contabancaria.api.core.Interfaces;
using stone.contabancaria.api.core.Services;
using System;
using System.Threading.Tasks;
using Xunit;

namespace stone.contabancaria.api.tests
{
    public class ContaCorrenteServiceTest
    {
        private readonly ContaCorrenteService _contaCorrenteService;
        private readonly Mock<IContaCorrenteRepository> _mockContaCorrenteRepository;
        private readonly Mock<ITransacaoRepository> _mockTransacaoRepository;
        private ContaCorrente ContaCorrente;
        private Transacao TransacaoSaque;
        private Transacao TransacaoDeposito;
        private Transacao TransacaoTranferenciaEnviar;
        private Transacao TransacaoTranferenciaReceber;
        public ContaCorrenteServiceTest()
        {
            _mockContaCorrenteRepository = new Mock<IContaCorrenteRepository>();
            _mockTransacaoRepository = new Mock<ITransacaoRepository>();
            ContaCorrente = new ContaCorrente
            {
                Documento = "30576930083",
                NumeroContaCorrente = "123456",
                Saldo = 1000
            };
            TransacaoSaque = new Transacao 
            { 
                ContaCorrente = "123456",
                Modalidade = Operacoes.Sacar,
                ValorCalculado = 520,
                ValorOriginal = 500,
                TipoOperacao = Operacoes.Debito,
                Data = DateTime.Now
            };
            TransacaoDeposito = new Transacao
            {
                ContaCorrente = "123456",
                Modalidade = Operacoes.Deposito,
                ValorCalculado = 495,
                ValorOriginal = 500,
                TipoOperacao = Operacoes.Credito,
                Data = DateTime.Now
            };
            TransacaoTranferenciaEnviar = new Transacao
            {
                ContaCorrente = "123456",
                Modalidade = Operacoes.Transferir,
                ValorCalculado = 495,
                ValorOriginal = 500,
                TipoOperacao = Operacoes.Debito,
                Data = DateTime.Now
            };
            TransacaoTranferenciaReceber = new Transacao
            {
                ContaCorrente = "123456",
                Modalidade = Operacoes.Transferir,
                ValorCalculado = 500,
                ValorOriginal = 500,
                TipoOperacao = Operacoes.Debito,
                Data = DateTime.Now
            };

            _contaCorrenteService = new ContaCorrenteService(_mockTransacaoRepository.Object,_mockContaCorrenteRepository.Object);
        }

        [Theory]
        [InlineData("123456", 1200)]
        public async Task SacarSaldoInsuficienteERRO(string conta, decimal valor)
        {
            _mockContaCorrenteRepository.Setup(p => p.BuscarContaCorrenteAsync(conta)).ReturnsAsync(ContaCorrente);
            var result = await Assert.ThrowsAsync<Exception>(() =>  _contaCorrenteService.Sacar(conta, valor, TransacaoSaque.Data));
            Assert.Equal(result.Message, Erros.SaldoInvalido);
        }
        [Theory]
        [InlineData("122222", 500)]
        public async Task SacarContaInvalidaERRO(string conta, decimal valor)
        {
            _mockContaCorrenteRepository.Setup(p => p.BuscarContaCorrenteAsync("123456")).ReturnsAsync(ContaCorrente);
            var result = await Assert.ThrowsAsync<Exception>(() => _contaCorrenteService.Sacar(conta, valor, TransacaoSaque.Data));
            Assert.Equal(result.Message, Erros.ContaInvalida);
        }


        [Theory]
        [InlineData("122222", 500)]
        public async Task DepositoContaInvalidaERRO(string conta, decimal valor)
        {
            _mockContaCorrenteRepository.Setup(p => p.BuscarContaCorrenteAsync("123456")).ReturnsAsync(ContaCorrente);
            var result = await Assert.ThrowsAsync<Exception>(() => _contaCorrenteService.Depositar(conta, valor, TransacaoDeposito.Data));
            Assert.Equal(result.Message, Erros.ContaInvalida);
        }

        [Theory]
        [InlineData("123456", 1200)]
        public async Task TransferirSaldoInsuficienteERRO(string conta, decimal valor)
        {
            _mockContaCorrenteRepository.Setup(p => p.BuscarContaCorrenteAsync(conta)).ReturnsAsync(ContaCorrente);
            var result = await Assert.ThrowsAsync<Exception>(() => _contaCorrenteService.Transferir("123458", conta, valor, TransacaoSaque.Data));
            Assert.Equal(result.Message, Erros.SaldoInvalido);
        }
        [Theory]
        [InlineData("122222", 500)]
        public async Task TransferirContaInvalidaERRO(string conta, decimal valor)
        {
            _mockContaCorrenteRepository.Setup(p => p.BuscarContaCorrenteAsync("123456")).ReturnsAsync(ContaCorrente);
            var result = await Assert.ThrowsAsync<Exception>(() => _contaCorrenteService.Transferir(conta, "123458", valor, TransacaoSaque.Data));
            Assert.Equal(result.Message, Erros.ContaInvalida);
        }

    }
}
