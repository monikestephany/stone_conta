using System;
using System.Collections.Generic;
using System.Text;

namespace stone.contabancaria.api.core.Contants
{
    public static class Erros
    {
        public const string SaldoInvalido = "Voce nao possui saldo para efetuar essa transacao.";
        public const string CPFInvalido = "O numero de CPF e invalido.";
        public const string ContaInvalida = "O numero de conta nao existe.";
        public const string OperacaoInvalida = "Nao foi possivel concluir a operacao.";
    }
}
