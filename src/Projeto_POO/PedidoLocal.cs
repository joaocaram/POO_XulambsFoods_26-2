using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XulambsFoods {
    public class PedidoLocal : Pedido {

        private const double Servico = 0.1;
        public override double PrecoAPagar() {
            return ValorItens() + TaxaServico();
        }

        private double TaxaServico() {
            return ValorItens() * Servico;
        }

        protected override string RodapePedido() {
            StringBuilder relat = new StringBuilder();
            relat.AppendLine($"SERVIÇO (10%) : {TaxaServico():C2}");
            relat.Append($"TOTAL DO PEDIDO: {PrecoAPagar():C2}");
            return relat.ToString();
        }
    }
}
