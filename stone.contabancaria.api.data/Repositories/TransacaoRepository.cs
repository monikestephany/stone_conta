using MongoDB.Driver;
using stone.contabancaria.api.core.Configuration;
using stone.contabancaria.api.core.Entities;
using stone.contabancaria.api.core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace stone.contabancaria.api.data.Repositories
{
    public class TransacaoRepository : ITransacaoRepository
    {
        private readonly IMongoCollection<Transacao> _transacao;

        public TransacaoRepository(IDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _transacao = database.GetCollection<Transacao>("transacoes");
        }

        public async Task<Transacao> InserirTransacao(Transacao transacao)
        {
            await _transacao.InsertOneAsync(transacao);
            return transacao;
        }

        public async Task<List<Transacao>> ListarTransacoes(string conta, DateTime dataInicio, DateTime dataFim) =>
          (await _transacao.FindAsync(cta => cta.ContaCorrente == conta && cta.Data >= dataInicio && cta.Data <= dataFim)).ToList();
    }
}
