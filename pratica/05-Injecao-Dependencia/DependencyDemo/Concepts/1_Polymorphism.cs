using System;

namespace DependencyDemo.Concepts
{
    public static class PolymorphismDemo
    {
        public static void RunDemo()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("===============================================================================");
            Console.WriteLine("                CONCEITO 1: POLIMORFISMO (Muitas Formas)                        ");
            Console.WriteLine("===============================================================================");
            Console.ResetColor();

            Console.WriteLine("\nPolimorfismo permite que objetos de diferentes classes sejam tratados como");
            Console.WriteLine("objetos de uma classe base comum. Divide-se principalmente em dois tipos:\n");

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("1. POLIMORFISMO ESTÁTICO (Sobrecarga de Métodos / Overloading)");
            Console.ResetColor();
            Console.WriteLine("   Ocorre em tempo de compilação. Mesma assinatura de método, mas parâmetros diferentes.");
            
            // Demonstração da Sobrecarga (Polimorfismo Estático)
            Calculadora calc = new Calculadora();
            Console.WriteLine($"   * calc.Somar(2, 3)     => Resultado: {calc.Somar(2, 3)} (Inteiros)");
            Console.WriteLine($"   * calc.Somar(2.5, 3.7) => Resultado: {calc.Somar(2.5, 3.7)} (Doubles)");

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n2. POLIMORFISMO DINÂMICO (Sobrescrita de Métodos / Overriding)");
            Console.ResetColor();
            Console.WriteLine("   Ocorre em tempo de execução. Permite que uma classe filha dê uma implementação");
            Console.WriteLine("   específica para um método definido na classe pai usando 'virtual' e 'override'.\n");

            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("--- REGRA DE OURO EM C#: override VS new (Sobrescrita vs Ocultação) ---");
            Console.ResetColor();
            Console.WriteLine("   - 'override': Substitui de fato o comportamento da classe base.");
            Console.WriteLine("   - 'new': Apenas oculta o método da classe base. O comportamento original ainda");
            Console.WriteLine("            pode ser executado se o objeto for referenciado pelo tipo da classe base.\n");

            // Exemplo prático de override vs new
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("[Instanciando como seus próprios tipos]");
            Console.ResetColor();
            Desenvolvedor devOriginal = new Desenvolvedor();
            Gerente gerOriginal = new Gerente();
            devOriginal.Trabalhar(); // Chama o override
            gerOriginal.Trabalhar(); // Chama o new

            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("[Polimorfismo em Ação: Tratando ambos como a classe base 'Funcionario']");
            Console.ResetColor();

            Funcionario funcDev = new Desenvolvedor();
            Funcionario funcGer = new Gerente();

            Console.Write("   * funcDev.Trabalhar(): ");
            funcDev.Trabalhar(); // Vai chamar o método do Desenvolvedor (override)
            
            Console.Write("   * funcGer.Trabalhar(): ");
            funcGer.Trabalhar(); // Vai chamar o método do Funcionario! (new ocultou, mas a referência é Funcionario)

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n3. INTERFACES VS CLASSES ABSTRATAS");
            Console.ResetColor();
            Console.WriteLine("   - Classe Abstrata (Abstract Class): Define uma identidade (o que a classe É).");
            Console.WriteLine("     Pode conter estado (campos/atributos) e código comum já implementado.");
            Console.WriteLine("     Uma classe só pode herdar de uma única classe (Herança Única).");
            Console.WriteLine("   - Interface: Define um contrato ou comportamento (o que a classe CONSEGUE FAZER).");
            Console.WriteLine("     Não deve conter estado. Uma classe pode implementar múltiplas interfaces.\n");

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Pressione qualquer tecla para voltar ao menu principal...");
            Console.ResetColor();
            Console.ReadKey();
        }
    }

    // --- CLASSES DE SUPORTE PARA A DEMONSTRAÇÃO ---

    // 1. Exemplo de Polimorfismo Estático (Sobrecarga)
    public class Calculadora
    {
        public int Somar(int a, int b) => a + b;
        public double Somar(double a, double b) => a + b;
    }

    // 2. Exemplo de Polimorfismo Dinâmico (Sobrescrita vs Ocultação)
    public class Funcionario
    {
        public string Nome { get; set; } = "Funcionário Comum";

        public virtual void Trabalhar()
        {
            Console.WriteLine("Funcionário está realizando tarefas gerais.");
        }
    }

    public class Desenvolvedor : Funcionario
    {
        // OVERRIDE: Substitui completamente o comportamento original.
        public override void Trabalhar()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Desenvolvedor está escrevendo código C# elegante!");
            Console.ResetColor();
        }
    }

    public class Gerente : Funcionario
    {
        // NEW: Oculta o método original (Method Hiding).
        // Se referenciado como Funcionario, chamará o método da base.
        // Se referenciado como Gerente, chamará este método.
        public new void Trabalhar()
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("Gerente está gerenciando a equipe e reuniões.");
            Console.ResetColor();
        }
    }
}
