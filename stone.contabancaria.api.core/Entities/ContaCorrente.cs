using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using stone.contabancaria.api.core.Contants;
using stone.contabancaria.api.core.Validations;
using System;
using System.Collections.Generic;
using System.Text;

namespace stone.contabancaria.api.core.Entities
{
    public class ContaCorrente
    {
        private decimal _saldo;
        private string _documento;
        private decimal _valorCalculado;
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string NumeroContaCorrente { get; set; }
        public string Documento
        {
            get => _documento;
            set
            {
                if(!ValidacoesContaCorrente.ValidaCPF(value))
                {
                    throw new Exception(Erros.CPFInvalido);
                }
                _documento = value;
            }
        }
        public decimal Saldo
        {
            get => _saldo;
            set
            {
                if (value < 0)
                    throw new Exception(Erros.SaldoInvalido);
                _saldo = value;
            }
        }
        public ContaCorrente Depositar(decimal valor)
        {
            _valorCalculado = valor - (new decimal(0.01) * valor);      
            Saldo += _valorCalculado;
            return this;
        }
        public ContaCorrente Sacar(decimal valor)
        {
            _valorCalculado = valor + (new decimal(0.04) * valor);
            Saldo -= _valorCalculado;
            return this;
        }
        public ContaCorrente Transferir(decimal valor, string tipoOperacao)
        {
            _valorCalculado = tipoOperacao == "D" ? valor + (new decimal(0.01) * valor) : valor;
            Saldo = tipoOperacao == "D"? Saldo - _valorCalculado : valor;
            return this;
        }
        public decimal ValorCalculado => _valorCalculado;
    }
}
