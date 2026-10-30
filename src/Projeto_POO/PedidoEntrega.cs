using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XulambsFoods {
    public class PedidoEntrega : Pedido {
        private const int MaxEntrega = 8;

        private ETaxaEntrega _taxaEntrega;
        private double _distanciaEntrega;

       
        public PedidoEntrega(double distancia) : base()
        {
            if (distancia <= 0)
                distancia = 0.01;
            _distanciaEntrega = distancia;
            _taxaEntrega = _distanciaEntrega.DefinirEntrega();
        }

        protected override void PodeAdicionar() { //override: modificação da regra original
            base.PodeAdicionar();
            if (_produtos.Count == MaxEntrega)
                throw new PedidoCheioException(this);
        }

        private double ValorTaxa() {
            return _taxaEntrega.ValorTaxa();
        }

        public override double PrecoAPagar() {
            return ValorItens() + ValorTaxa();
        }

        protected override string RodapePedido()
        {
            StringBuilder relat = new StringBuilder();
            relat.AppendLine($"TAXA DE ENTREGA: {ValorTaxa():C2} ({_distanciaEntrega}km).");
            relat.Append($"TOTAL DO PEDIDO: {PrecoAPagar():C2}");
            return relat.ToString();
        }
    }
}