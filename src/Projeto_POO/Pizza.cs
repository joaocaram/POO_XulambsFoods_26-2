using System;
using System.Collections.Specialized;
using System.Text;


namespace XulambsFoods {    
    public class Pizza {

        /// <summary>
        /// Lembre-se:
        // ENTENDER O PROBLEMA!!!
        //Regra 0 -- não entre em pânico
        //Regra 1 -- não viaje
        /// </summary>
        /// 
        #region atributos
        int _maxIngredientes;
        double _precoBase;
        int _quantIngredientes;
        double _valorPorAdicional;
        string _descricao;
        #endregion

        #region construtores
        private void Init(int adicionais){
            _descricao = "Pizza";
            _maxIngredientes = 8;
            _precoBase = 29d;
            AdicionarIngredientes(adicionais);
            _valorPorAdicional = 5d;
        }

        public Pizza() {
            Init(0);
        }

        public Pizza(int adicionais) {
            Init(adicionais);
        }
        #endregion

        #region métodos privados
        private double ValorAdicionais() {
            return _quantIngredientes * _valorPorAdicional; 
        }

        private void ModificarDescricao() {
            _descricao = $"Pizza com {_quantIngredientes} adicionais";
        }

        private bool PodeAdicionar(int quantos) {
            return (quantos >= 0 && 
                    quantos + _quantIngredientes <= _maxIngredientes);
        }
        #endregion

        #region métodos públicos
        public double CalcularValorFinal() {
            return _precoBase + ValorAdicionais();
        }

        /// <summary>
        /// Tenta adicionar uma quantidade de ingredientes na pizza. Caso o valor seja inválido
        /// ou a quantidade seja negativa, ignora a operação.
        /// </summary>
        /// <param name="quantos">Quantidade de ingredientes a ser adicionada na pizza (int não negativo)</param>
        /// <returns>Quantidade de ingredientes da pizza após a execução do método</returns>
        public int AdicionarIngredientes(int quantos) {
            if (PodeAdicionar(quantos)) {
                _quantIngredientes = _quantIngredientes + quantos;
                ModificarDescricao();
            }
            return _quantIngredientes;
        }

        /// <summary>
        /// Gera o cupom de venda da pizza, que mostra sua descrição com a 
        /// quantidade de ingredientes, e o preço detalhado a ser pago.
        /// </summary>
        /// <returns>String com os dados descritos acima</returns>
        public string GerarCupom() {
            StringBuilder nota = new StringBuilder("Xulambs Pizza!!!\n");
            nota.AppendLine("================");
            nota.AppendLine($"{_descricao}");
            nota.AppendLine($"\tPreco inicial: {_precoBase:C2}");
            nota.AppendLine($"\tAdicionais: {ValorAdicionais():C2}");
            nota.AppendLine($"TOTAL: {CalcularValorFinal():C2}");
            nota.Append("================");
            return nota.ToString();
        }
        #endregion

    }
}