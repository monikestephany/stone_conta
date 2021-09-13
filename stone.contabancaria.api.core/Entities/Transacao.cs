using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using stone.contabancaria.api.core.Validations;
using System;
using System.Collections.Generic;
using System.Text;

namespace stone.contabancaria.api.core.Entities
{
    public class Transacao
    {
        private DateTime _date;
        private string _tipoOperacao;
        private string _modalidade;
        private string _contaBeneficiario;
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public decimal ValorOriginal { get; set; }
        public string ContaCorrente { get; set; }
        public string ContaBeneficiario
        {
            get => _contaBeneficiario;
            set
            {
                _contaBeneficiario = value;
            }
        }
        public string Modalidade
        {
            get => _modalidade;
            set
            {
                _modalidade = value;
            }
        }
        public string TipoOperacao
        {
            get => _tipoOperacao;
            set
            {
                _tipoOperacao = value;
            }
        }
        public decimal ValorCalculado { get; set; }
        public DateTime Data
        {
            get => _date;
             set
            {
                _date = value;
            }
        }
        public Transacao GerarTransacao(string contaCorrente, decimal valorOriginal, decimal valorCalculado, string contaBeneficiario, string modalidade, string tipoOperacao, DateTime dateTime)
        {
            var transacao = new Transacao
            {
                ContaCorrente = contaCorrente,
                ValorOriginal = valorOriginal,
                ValorCalculado = valorCalculado,
                ContaBeneficiario = contaBeneficiario,
                TipoOperacao = tipoOperacao,
                Modalidade = modalidade,
                Data = dateTime
            };
            return transacao;
        }
        public Transacao GerarTransacao(string contaCorrente, decimal valorOriginal, decimal valorCalculado, string modalidade, string tipoOperacao, DateTime dateTime)
        {
            var transacao = new Transacao
            {
                ContaCorrente = contaCorrente,
                ValorOriginal = valorOriginal,
                ValorCalculado = valorCalculado,
                Modalidade = modalidade,
                Data = dateTime,
                TipoOperacao = tipoOperacao
            };
            return transacao;
        }
    }
   
}
