using System;
using System.Text;


namespace XulambsFoods {    
    public class Pizza {

        /// <summary>
        /// Lembre-se:
        // ENTENDER O PROBLEMA!!!
        //Regra 0 -- não entre em pânico
        //Regra 1 -- não viaje
        //Regra 2 -- não interessa como vai funcionar
        //Regra 3 -- isto não é um cachimbo
        /// </summary>
        /// 

        #region constantes
        private const int MaxIngredientes = 8;
        private const double PrecoBase = 29d;
        private const double ValorPorAdicional = 5d;
        #endregion

        #region atributos
        private static int s_pizzasVendidas;

        private int _quantIngredientes;
        private string _descricao;
        #endregion

        #region construtores

        private void Init(int adicionais) {
            _descricao = "Pizza";
            AdicionarIngredientes(adicionais);
            s_pizzasVendidas++;
        }

        public Pizza() {
            Init(0);
        }

        /// <summary>
        /// Cria uma pizza com a quantidade de adicionais desejada. Em caso de erro, retorna uma pizza sem adicionais.
        /// </summary>
        /// <param name="adicionais">Quantidade de ingredientes da pizza. Deve ser >= 0 e <=8 </param>
        public Pizza(int adicionais) {
            Init(adicionais);
        }
        #endregion

        #region acesso
        public static int GetQuantidadeVendida() {
            return s_pizzasVendidas;
        }
        #endregion

        #region métodos privados
        private double ValorAdicionais() {
            return ValorPorAdicional * _quantIngredientes;
        }

        private void ModificarDescricao() {
            _descricao = $"Pizza com {_quantIngredientes} adicionais";
        }

        private bool PodeAdicionar(int quantos) {
            return (quantos >= 0 &&
                    quantos + _quantIngredientes <= MaxIngredientes);
        }
        #endregion

        #region métodos públicos
        public double CalcularValorFinal() {
            return PrecoBase + ValorAdicionais();
        }

        /// <summary>
        /// Tenta adicionar ingredientes à pizza. Faz a validação e, em caso de erros,
        /// não realiza a operação.
        /// </summary>
        /// <param name="quantos">Quantidade de ingredientes a ser adicionada. Deve ser maior ou igual a 0</param>
        /// <returns>A quantidade de ingredientes na pizza após a execução do método.</returns>
        public int AdicionarIngredientes(int quantos) {
            if (PodeAdicionar(quantos)) {
                _quantIngredientes += quantos;
                ModificarDescricao();
            }
            return _quantIngredientes;
        }

        /// <summary>
        /// Gera o cupom de venda da pizza. O cupom contém a descrição com a quantidade de adicionais,
        /// o valor base, o valor dos adicionais e o valor total a pagar.
        /// </summary>
        /// <returns>String com os dados acima</returns>
        public string GerarCupom() {
            StringBuilder cupom = new StringBuilder("Xulambs Pizza!!!\n");
            cupom.AppendLine("================");
            cupom.AppendLine($"{_descricao}");
            cupom.AppendLine($"\tPizza: {PrecoBase:C2}");
            cupom.AppendLine($"\t{_quantIngredientes} adicionais : {ValorAdicionais():C2}");
            cupom.AppendLine($"TOTAL: {CalcularValorFinal():C2}");
            cupom.Append("================");
            return cupom.ToString();
        }

        /* String -> valor imutável.
         * 
         *  cupom -----------------> "Xulambs Pizza!!!"
         *                              \---------------> "================"
         *                                                        |
         *                                                        |
         *                        _descricao<---------------------/
         */
        #endregion

    }
}