using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace stone.contabancaria.api.application.Models
{
    public class ContaCorrentePostModel
    {
        [JsonProperty("numeroContaCorrente")]
        public string NumeroContaCorrente { get; set; }

        [JsonProperty("saldo")]
        public decimal Saldo { get; set; }

        [JsonProperty("documento")]
        public string Documento { get; set; }
    }
}
