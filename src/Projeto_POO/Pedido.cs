using System.Text;

namespace XulambsFoods {
    public class Pedido {
        private static int s_ultimoPedido=0;
        private int _idPedido;
        private DateOnly _data;
        private LinkedList<Pizza> _pizzas;
        private bool _aberto;

       public Pedido() {
            _pizzas = new LinkedList<Pizza>();
            _aberto = true;
            _data =  DateOnly.FromDateTime(DateTime.Now);
            s_ultimoPedido++;
            _idPedido = _data.Day*100 + s_ultimoPedido;
        }

        private bool PodeAdicionar() {
            return _aberto;
        }

        public int GetID() {
            return _idPedido;
        }

        public int Adicionar(Pizza pizza) {
            if (PodeAdicionar() && pizza != null)
                _pizzas.AddLast(pizza);
            return _pizzas.Count;
        }

        public void FecharPedido() {
            _aberto = false;
        }

        public double PrecoAPagar() {
            double preco = 0d;
            foreach (Pizza pizza in _pizzas) {
                preco += pizza.CalcularValorFinal();
            }
            return preco;
        }

        public string Relatorio() {
            StringBuilder relat = new StringBuilder($"Pedido nº{_idPedido} - {_data}\n");
            string estado = "fechado";
            if (_aberto)
                estado = "aberto";
            relat.AppendLine($"Pedido {estado}.");
            int i = 0;
            foreach(Pizza pizza in _pizzas) {
                relat.AppendLine($"{++i:D2} {pizza.GerarCupom()}");
                relat.AppendLine("===============");
            }
            relat.AppendLine($"\nTOTAL DO PEDIDO: {PrecoAPagar():C2}");
            return relat.ToString();
        }
    }
}
