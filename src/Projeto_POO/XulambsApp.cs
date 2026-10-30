

using System.Text;

namespace XulambsFoods
{
    public class XulambsApp
    {
        static LinkedList<Pedido> pedidos;
        static string versao = "0.5";

        #region utilidades
        static void Pausa()
        {
            Console.WriteLine("\nDigite enter para continuar.");
            Console.ReadKey();
        }

        static void Cabecalho()
        {
            Console.Clear();
            Console.WriteLine($"Xulambs Pizza v{versao}");
            Console.WriteLine("===================");
        }
        #endregion

        #region configuração
        static void Config()
        {
            pedidos = new LinkedList<Pedido>();

        }
        #endregion

        static int LerInteiro() {
            int opcao;
            try {
                opcao = int.Parse(Console.ReadLine());
            }
            catch (FormatException) {
                Console.WriteLine("Somente valores numéricos");
                opcao = 99;
            }
            return opcao;
        }
        #region menus
        static int MenuPrincipal()
        {
            Cabecalho();
            
            StringBuilder menu = new StringBuilder();
            menu.AppendLine("1 - Abrir um pedido");
            menu.AppendLine("2 - Alterar pedido");
            menu.AppendLine("3 - Relatório de pedido");
            menu.AppendLine("4 - Fechar pedido");
            menu.AppendLine("0 - Finalizar");
            menu.Append("Sua opção: ");
            Console.Write(menu.ToString());
            
            return LerInteiro();
        }

        static int MenuProdutos() {
            Cabecalho();
            Console.WriteLine("Quer comprar o quê?");
            StringBuilder menu = new StringBuilder();
            menu.AppendLine("1 - Pizzas");
            menu.AppendLine("2 - Sobremesas");
            menu.AppendLine("0 - Desistir");
            menu.Append("Sua opção: ");
            Console.Write(menu.ToString());

            return LerInteiro();
        }

        static int MenuSobremesas() {
            Cabecalho();
            Console.WriteLine("Escolha sua sobremesa");
            StringBuilder menu = new StringBuilder();
            menu.AppendLine("1 - Brigadeiro de Colher");
            menu.AppendLine("2 - Pudim de Leite Condensado");
            menu.AppendLine("3 - Doce de Leite no Pires");
            menu.AppendLine("0 - Desistir");
            menu.Append("Sua opção: ");
            Console.Write(menu.ToString());

            return LerInteiro();
        }
        #endregion

        #region relatorios
        static void ImprimirDados(object objeto)
        {
            Cabecalho();

            //null safety para impressão: se não existir, pega uma string "padrão"
            string msg = objeto?.ToString() ?? "Objeto não encontrado.";

            Console.WriteLine(msg);
        }

        #endregion

        #region borda
        public static void EscolherBorda(Pizza pizza)
        {
            string[] bordas = Enum.GetNames<EBorda>();
            Console.WriteLine("Escolha uma borda:");
            int i = 1;
            foreach (string nome in bordas)
            {
                Console.WriteLine($"{i} - {nome}");
                i++;
            }
            Console.Write("Sua escolha: ");
            int escolha = LerInteiro();
            EBorda borda = Enum.GetValues<EBorda>()[escolha - 1];

            pizza.AdicionarBorda(borda);
        }
        #endregion

        #region sobremesa
        static Produto ComprarSobremesa() {
            int opcao = MenuSobremesas();
            ESobremesa tipo = Enum.GetValues<ESobremesa>()[opcao - 1];
            return new Sobremesa(tipo);
        }

        #endregion

        #region pizza
        static int EscolherIngredientes()
        {
            Console.Write("Quantos ingredientes você deseja (0-8)? ");
            return LerInteiro();
        }

        static Pizza ComprarPizza()
        {
            Cabecalho();
            Console.WriteLine("Comprando uma pizza:");

            Pizza novaPizza = new Pizza();
            EscolherBorda(novaPizza);

            int quantos = EscolherIngredientes();
            try {
                novaPizza.AdicionarIngredientes(quantos);
                ImprimirDados(novaPizza);
            }
            catch (ArgumentException excecao) {
                Console.WriteLine(excecao.Message);
                Pausa();
                return ComprarPizza();
            }
            catch (InvalidOperationException excecao) {
                Console.WriteLine(excecao.Message);
                novaPizza = new Pizza(8);
            }
            

            return novaPizza;
        }
        #endregion

        #region produto
        static Produto ComprarProduto() {
            int opcao = MenuProdutos();
            return opcao switch {
                1 => ComprarPizza(),
                2 => ComprarSobremesa(),
                _ => null
            };

        }

        #endregion

        #region pedido

        static PedidoEntrega CriarPedidoEntrega()
        {
            Console.Write("Qual a distância? ");
            double distancia = double.Parse(Console.ReadLine());
            return new PedidoEntrega(distancia);
        }

        static Pedido EscolherTipoPedido()
        {
            Cabecalho();
            Console.WriteLine("Escolha o tipo de pedido: ");
            Console.WriteLine("1 - Local");
            Console.WriteLine("2 - Para entrega");
            Console.Write("Digite sua escolha: ");
            int opcao = LerInteiro();
            return opcao switch
            {
                1 => new PedidoLocal(),
                2 => CriarPedidoEntrega()
            };

        }
        static void CriarPedido()
        {
            string resp = "s";
            Pedido novoPedido = EscolherTipoPedido();
            do
            {
                Produto produto = ComprarProduto();
                novoPedido.Adicionar(produto);
                Console.Write("\n\nAlgo mais (s/n)? ");
                resp = Console.ReadLine();
            } while (resp.ToLower().Equals("s"));

            ImprimirDados(novoPedido);
            pedidos.AddLast(novoPedido);
        }

        static void RelatorioPedido()
        {
            Cabecalho();
            Console.Write("Número do pedido: ");
            int codigo = LerInteiro();
            Pedido pedido = Localizar(codigo) as Pedido;   //conversão segura: objeto ou nulo
            ImprimirDados(pedido);
        }

        static void FecharPedido()
        {
            Cabecalho();
            Console.Write("Número do pedido: ");
            int codigo = LerInteiro();

            Pedido pedido = Localizar(codigo) as Pedido;   //conversão segura: objeto ou nulo
            pedido?.FecharPedido();             //null safety: só executa se o pedido existir

            ImprimirDados(pedido);
        }

        static void AlterarPedido()
        {
            Cabecalho();
            Console.Write("Número do pedido: ");
            int codigo = LerInteiro();          
            Pedido pedido = Localizar(codigo) as Pedido;   //conversão segura: objeto ou nulo
            
            if (pedido != null)
            {
                try
                {
                    Pizza novaPizza = ComprarPizza();
                    pedido.Adicionar(novaPizza);
                    ImprimirDados(pedido);
                }
                catch (PedidoFechadoException fechado)
                {
                    Console.WriteLine(fechado.Message);
                }
                catch(PedidoCheioException cheio)
                {
                    Console.WriteLine(cheio.Message);
                    Console.Write("Quer fechar o pedido? ");
                    string resp = Console.ReadLine();
                    if (resp.Equals("s"))
                        cheio.FecharPedido();
                }
                catch (InvalidOperationException operacao)
                {
                    Console.WriteLine(operacao.Message);
                    AlterarPedido();
                }
            };
        }

        static object Localizar(int codigo)
        {
            object localizado = null;
            for (int i = 0; i < pedidos.Count && localizado == null; i++)
            {
                object candidato = pedidos.ElementAt(i);
                if (candidato.GetHashCode() == codigo)
                    localizado = candidato;
            }
            return localizado;
        }
        #endregion

        static void Main(string[] args)
        {
            int opcao;
            Config();
            do
            {
                opcao = MenuPrincipal();
                Action ac =
                opcao switch
                {
                    1 => () => CriarPedido(),
                    2 => () => AlterarPedido(),
                    3 => () => RelatorioPedido(),
                    4 => () => FecharPedido(),
                    _ => () => Pausa()
                };
                ac.Invoke();
                Pausa();
            } while (opcao != 0);
        }
    }
}