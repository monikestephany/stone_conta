using stone.contabancaria.api.core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace stone.contabancaria.api.core.Interfaces
{
    public interface ITransacaoRepository
    {
        Task<Transacao> InserirTransacao(Transacao transacao);
        Task<List<Transacao>> ListarTransacoes(string conta, DateTime dataInicio, DateTime dataFim);
    }
}
