

namespace XulambsFoods {
    public class XulambsApp
    {
        static LinkedList<Pedido> pedidos;
static string versao = "0.2";

#region utilidades
static void Pausa() {
    Console.WriteLine("\nDigite enter para continuar.");
    Console.ReadKey();
}
static void Cabecalho() {
    Console.Clear();
    Console.WriteLine($"Xulambs Pizza v{versao}");
    Console.WriteLine("===================");
}
#endregion

#region configuração
static void Config() {
    pedidos = new LinkedList<Pedido>();
}

#endregion
#region menus
static int MenuPrincipal() {
    Cabecalho();            
    StringBuilder menu = new StringBuilder();
    menu.AppendLine("1 - Abrir um pedido");
    menu.AppendLine("2 - Relatório de um pedido");
    menu.AppendLine("3 - Incluir pizza em pedido");
    menu.AppendLine("4 - Fechar pedido");
    menu.AppendLine("0 - Finalizar");
    menu.Append("Sua opção: ");
    Console.Write(menu.ToString());
    return int.Parse(Console.ReadLine());
}
#endregion

#region relatorios
static void ImprimirDadosPedido(Pedido pedido) {
    Cabecalho();
    string msg = pedido?.Relatorio() ?? "Pedido não encontrado.";
    Console.WriteLine(msg);
}

static void ImprimirDadosPizza(Pizza pizza) {
    Console.WriteLine();
    Console.WriteLine("Pizza comprada:\n ");
    Console.WriteLine(pizza.GerarCupom());
    Pausa();
}
#endregion

#region pizza
static Pizza ComprarPizza() {
    Cabecalho();

    Console.WriteLine("Comprando uma pizza:");
    Console.Write("Quantos ingredientes você deseja (0-8)? ");
    int quantos = int.Parse(Console.ReadLine());

    Pizza novaPizza = new Pizza(quantos);
    
    ImprimirDadosPizza(novaPizza);

    return novaPizza;
}

#endregion

#region pedido
static void CriarPedido() {
    string resp = "s";
    Pedido novoPedido = new Pedido();
    do {
        Pizza novaPizza = ComprarPizza();
        novoPedido.Adicionar(novaPizza);
        Console.Write("\n\nMais pizza? ");
        resp = Console.ReadLine();
    } while (resp.ToLower().Equals("s"));
    pedidos.AddLast(novoPedido);
    ImprimirDadosPedido(novoPedido);
}



static void RelatorioPedido() {
    Cabecalho();
    Console.Write("Número do pedido: ");
    int codigo = int.Parse(Console.ReadLine());
    ImprimirDadosPedido(LocalizarPedido(codigo));
}


static void FecharPedido() {
    Cabecalho();
    Console.Write("Número do pedido: ");
    int codigo = int.Parse(Console.ReadLine());

    Pedido pedido = LocalizarPedido(codigo);
    pedido?.FecharPedido();
    ImprimirDadosPedido(pedido);
    
}

static void AlterarPedido() {
    Cabecalho();
    Pizza novaPizza = ComprarPizza();

    Console.Write("Número do pedido: ");
    int codigo = int.Parse(Console.ReadLine());

    Pedido pedido = LocalizarPedido(codigo);
    if (pedido != null)
        pedido.Adicionar(novaPizza);

    ImprimirDadosPedido(pedido);
    
}

static Pedido LocalizarPedido(int codigo) {
    Pedido localizado = null;
    for (int i = 0; i < pedidos.Count && localizado == null; i++) {
        Pedido candidato = pedidos.ElementAt(i);
        if (candidato.GetID() == codigo)
            localizado = candidato;
    }
    return localizado;
}
#endregion

static void Main(string[] args) {
    int opcao;
    Config();
    do {
        opcao = MenuPrincipal();
        Action ac =
        opcao switch {
            1 => () => CriarPedido(),
            2 => () => RelatorioPedido(),
            3 => () => AlterarPedido(),
            4 => () => FecharPedido(),
            _ => () => Pausa()
        };
        ac.Invoke();
        Pausa();
    } while (opcao != 0);
}
       
    }
}
