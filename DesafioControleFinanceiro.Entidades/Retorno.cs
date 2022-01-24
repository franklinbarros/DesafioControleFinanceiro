using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace DesafioControleFinanceiro.Entidades
{
    public class Retorno
    {
        [JsonIgnore]
        public bool ocorreuErro { get; set; }
        public string codigo { get; set; }
        public string mensagem { get; set; }

        [JsonIgnore]
        public dynamic Model { get; set; }
    }
}
