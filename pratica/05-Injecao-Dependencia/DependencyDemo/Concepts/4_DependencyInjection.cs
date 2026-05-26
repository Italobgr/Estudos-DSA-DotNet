using System;
using Microsoft.Extensions.DependencyInjection;

namespace DependencyDemo.Concepts
{
    public static class DependencyInjectionDemo
    {
        public static void RunDemo()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("===============================================================================");
            Console.WriteLine("          CONCEITO 4: INJEÇÃO DE DEPENDÊNCIA (DI) E TEMPOS DE VIDA              ");
            Console.WriteLine("===============================================================================");
            Console.ResetColor();

            Console.WriteLine("\nInjeção de Dependência (DI) é a implementação técnica do DIP e IoC.");
            Console.WriteLine("Em vez de o objeto gerenciar e instanciar suas dependências, elas são 'injetadas'");
            Console.WriteLine("nele (geralmente via Construtor).\n");

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("1. TIPOS DE INJEÇÃO:");
            Console.ResetColor();
            Console.WriteLine("   - Injeção por Construtor (Constructor Injection): O tipo mais comum e recomendado.");
            Console.WriteLine("   - Injeção por Propriedade / Setter (Property Injection): Usado para dependências opcionais.");
            Console.WriteLine("   - Injeção por Método (Method Injection): Usado quando a dependência varia a cada chamada.\n");

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("2. TEMPOS DE VIDA DE SERVIÇOS (Service Lifetimes) no .NET:");
            Console.ResetColor();
            Console.WriteLine("   Quando usamos um Container de DI (como o padrão do ASP.NET Core), registramos");
            Console.WriteLine("   nossos serviços com diferentes ciclos de vida:\n");
            
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("   - TRANSIENT (Transitório): ");
            Console.ResetColor();
            Console.WriteLine("Uma nova instância é criada TODA VEZ que o serviço é solicitado.");

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("   - SCOPED (Escopado): ");
            Console.ResetColor();
            Console.WriteLine("Uma única instância é criada POR ESCOPO (ex: por requisição HTTP).");

            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write("   - SINGLETON: ");
            Console.ResetColor();
            Console.WriteLine("Uma única instância é criada UMA VEZ para toda a vida útil da aplicação.\n");

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("--- SIMULAÇÃO DE LIFETIMES COM CONTAINER DE DI DO .NET ---");
            Console.ResetColor();
            Console.WriteLine("   Configurando o Microsoft.Extensions.DependencyInjection...");

            // 1. Configurando o Container DI
            var colecaoServicos = new ServiceCollection();
            
            // Registrando com seus respectivos Lifetimes
            colecaoServicos.AddTransient<IServicoTransient, ServicoExemplo>();
            colecaoServicos.AddScoped<IServicoScoped, ServicoExemplo>();
            colecaoServicos.AddSingleton<IServicoSingleton, ServicoExemplo>();

            // Construindo o Provedor de Serviços (o Container)
            var provedor = colecaoServicos.BuildServiceProvider();

            // 2. Executando em dois Escopos diferentes
            ExecutarNoEscopo(provedor, 1);
            Console.WriteLine();
            ExecutarNoEscopo(provedor, 2);

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\nANÁLISE DOS RESULTADOS:");
            Console.ResetColor();
            Console.WriteLine("   * Repare nos IDs (GUIDs) gerados:\n");
            
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("   - Transient (Amarelo): Mudou em cada solicitação, mesmo no mesmo escopo.");
            
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("   - Scoped (Ciano): Foi o mesmo ID dentro do Escopo 1, mas mudou inteiramente no Escopo 2.");
            
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("   - Singleton (Magenta): Permaneceu absolutamente o mesmo ID em todos os testes e escopos!");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\nPressione qualquer tecla para voltar ao menu principal...");
            Console.ResetColor();
            Console.ReadKey();
        }

        private static void ExecutarNoEscopo(ServiceProvider provedor, int numeroEscopo)
        {
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine($"\n>>> INICIANDO ESCOPO #{numeroEscopo} <<<");
            Console.ResetColor();

            using (var escopo = provedor.CreateScope())
            {
                var provedorDoEscopo = escopo.ServiceProvider;

                // Fazemos 2 solicitações de cada serviço no mesmo escopo
                
                // --- TRANSIENT ---
                var transient1 = provedorDoEscopo.GetRequiredService<IServicoTransient>();
                var transient2 = provedorDoEscopo.GetRequiredService<IServicoTransient>();
                
                // --- SCOPED ---
                var scoped1 = provedorDoEscopo.GetRequiredService<IServicoScoped>();
                var scoped2 = provedorDoEscopo.GetRequiredService<IServicoScoped>();

                // --- SINGLETON ---
                var singleton1 = provedorDoEscopo.GetRequiredService<IServicoSingleton>();
                var singleton2 = provedorDoEscopo.GetRequiredService<IServicoSingleton>();

                // Exibindo os IDs
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"   [Transient] Req 1 ID: {transient1.Id.ToString()[..8]}...");
                Console.WriteLine($"   [Transient] Req 2 ID: {transient2.Id.ToString()[..8]}...");

                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"   [Scoped]    Req 1 ID: {scoped1.Id.ToString()[..8]}...");
                Console.WriteLine($"   [Scoped]    Req 2 ID: {scoped2.Id.ToString()[..8]}...");

                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine($"   [Singleton] Req 1 ID: {singleton1.Id.ToString()[..8]}...");
                Console.WriteLine($"   [Singleton] Req 2 ID: {singleton2.Id.ToString()[..8]}...");
                Console.ResetColor();
            }
        }
    }

    // --- INTERFACES PARA OS TEMPOS DE VIDA ---

    public interface IServicoLifetime
    {
        Guid Id { get; }
    }

    public interface IServicoTransient : IServicoLifetime { }
    public interface IServicoScoped : IServicoLifetime { }
    public interface IServicoSingleton : IServicoLifetime { }

    // --- CLASSE CONCRETA ÚNICA QUE GERARÁ UM ID ÚNICO NO CONSTRUTOR ---
    public class ServicoExemplo : IServicoTransient, IServicoScoped, IServicoSingleton
    {
        public Guid Id { get; }

        public ServicoExemplo()
        {
            Id = Guid.NewGuid();
        }
    }
}
