using System;
using DependencyDemo.Concepts;

namespace DependencyDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            // Define o encoding do console para UTF8 para evitar problemas de caracteres especiais
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            
            bool rodando = true;

            while (rodando)
            {
                MostrarMenuPrincipal();
                
                string? opcao = Console.ReadLine();

                switch (opcao)
                {
                    case "1":
                        PolymorphismDemo.RunDemo();
                        break;
                    case "2":
                        InversionOfControlDemo.RunDemo();
                        break;
                    case "3":
                        DependencyInversionDemo.RunDemo();
                        break;
                    case "4":
                        DependencyInjectionDemo.RunDemo();
                        break;
                    case "5":
                        rodando = false;
                        MostrarDespedida();
                        break;
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\nOpção inválida! Escolha um número de 1 a 5. Pressione qualquer tecla para continuar...");
                        Console.ResetColor();
                        Console.ReadKey();
                        break;
                }
            }
        }

        static void MostrarMenuPrincipal()
        {
            Console.Clear();
            
            // Desenho de um belo Banner ASCII com gradiente de cor no console
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(@"  _____  _                 _   _  _____  _    _ 
 |  __ \(_)               | \ | |/ ____|| |  | |
 | |  | |_  ___   ___     |  \| | |  __ | |__| |
 | |  | | |/ _ \ / _ \    | . ` | | |_ ||  __  |
 | |__| | | (_) | (_) |   | |\  | |__| || |  | |
 |_____/|_|\___/ \___/    |_| \_|\_____||_|  |_|");
            
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("===============================================================================");
            Console.WriteLine("        GUIA INTERATIVO: POLIMORFISMO E INVERSÃO DE DEPENDÊNCIA EM C#          ");
            Console.WriteLine("===============================================================================");
            Console.ResetColor();

            Console.WriteLine("\nBem-vindo ao laboratório prático de Programação Orientada a Objetos e Arquitetura!");
            Console.WriteLine("Selecione um dos conceitos fundamentais abaixo para aprender e ver o código rodando:\n");

            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("  [1] "); 
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Polimorfismo (Estático, Dinâmico, override vs new, Interfaces/Abstracts)");

            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("  [2] "); 
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Inversão de Controle (IoC - O fluxo invertido usando Template Method)");

            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("  [3] "); 
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Princípio de Inversão de Dependência (DIP - Acoplamento Forte vs Acoplamento Fraco)");

            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("  [4] "); 
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Injeção de Dependência & Lifetimes (DI - Transient, Scoped, Singleton)");

            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("  [5] "); 
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Sair do Guia de Estudos");

            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("\n===============================================================================");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(" Digite sua opção (1-5): ");
            Console.ResetColor();
        }

        static void MostrarDespedida()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("===============================================================================");
            Console.WriteLine("       Obrigado por estudar! Lembre-se: dependa sempre de abstrações! 😉        ");
            Console.WriteLine("===============================================================================");
            Console.ResetColor();
            System.Threading.Thread.Sleep(1500);
        }
    }
}
