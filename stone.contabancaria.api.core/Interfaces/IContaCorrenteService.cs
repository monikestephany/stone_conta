using stone.contabancaria.api.core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace stone.contabancaria.api.core.Interfaces
{
    public interface IContaCorrenteService
    {
        Task Depositar(string contaCorrente, decimal valor, DateTime dateTime);
        Task Sacar(string contaCorrente, decimal valor, DateTime dateTime);
        Task Transferir(string contaBeneficiario, string contaCorrente, decimal valor, DateTime dateTime);
        Task<ContaCorrente> NovaContaCorrente(ContaCorrente contaCorrente);
        Task<List<Transacao>> Extrato(string contaCorrente, DateTime inicio, DateTime fim);
    }
}
