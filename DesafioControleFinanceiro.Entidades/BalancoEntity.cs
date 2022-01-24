using Newtonsoft.Json;

namespace DesafioControleFinanceiro.Entidades
{
    public class BalancoEntity
    {
        public decimal receita { get; set; }
        public decimal despesa { get; set; }
        public decimal saldo { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public CategoriaEntity categoria { get; set; }

    }
}
