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
    public class ContaCorrenteRepository: IContaCorrenteRepository
    {
        private readonly IMongoCollection<ContaCorrente> _contaCorrente;

        public ContaCorrenteRepository(IDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _contaCorrente = database.GetCollection<ContaCorrente>("conta_corrente");

        }
        public async Task<ContaCorrente> CriarContaCorrenteAsync(ContaCorrente contaCorrente)
        {
             await _contaCorrente.InsertOneAsync(contaCorrente);
             return contaCorrente;
        }
        public async Task<ContaCorrente> BuscarContaCorrenteAsync(string conta) =>
            (await _contaCorrente.FindAsync(cta => cta.NumeroContaCorrente == conta)).FirstOrDefault();

        public async Task<bool> VerificarContaCorrenteAsync(string conta) =>
          (await _contaCorrente.FindAsync(cta => cta.NumeroContaCorrente == conta)).Any();

        public async Task AtualizarContaCorrente(ContaCorrente conta) =>
           await _contaCorrente.ReplaceOneAsync(cta => cta.Id == conta.Id, conta);
    }
}
