using stone.contabancaria.api.core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace stone.contabancaria.api.core.Interfaces
{
    public interface IContaCorrenteRepository
    {
        Task<ContaCorrente> BuscarContaCorrenteAsync(string conta);
        Task AtualizarContaCorrente(ContaCorrente conta);
        Task<bool> VerificarContaCorrenteAsync(string conta);
        Task<ContaCorrente> CriarContaCorrenteAsync(ContaCorrente contaCorrente);
    }
}
