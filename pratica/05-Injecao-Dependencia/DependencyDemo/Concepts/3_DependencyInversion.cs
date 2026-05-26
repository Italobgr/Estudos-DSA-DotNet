using System;

namespace DependencyDemo.Concepts
{
    public static class DependencyInversionDemo
    {
        public static void RunDemo()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("===============================================================================");
            Console.WriteLine("          CONCEITO 3: PRINCÍPIO DE INVERSÃO DE DEPENDÊNCIA (DIP)               ");
            Console.WriteLine("===============================================================================");
            Console.ResetColor();

            Console.WriteLine("\nO Princípio de Inversão de Dependência (DIP - o 'D' do SOLID) dita que:\n");
            
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("   1. Módulos de alto nível não devem depender de módulos de baixo nível.");
            Console.WriteLine("      Ambos devem depender de ABSTRAÇÕES.");
            Console.WriteLine("   2. Abstrações não devem depender de detalhes. Detalhes devem depender de abstrações.");
            Console.ResetColor();

            Console.WriteLine("\nVamos contrastar o cenário ANTES (Violando o DIP) com o cenário DEPOIS (Aplicando o DIP):\n");

            // ==========================================
            // CENÁRIO 1: VIOLANDO O DIP (Acoplamento Forte)
            // ==========================================
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("--- CENÁRIO ANTES (Violando o DIP) ---");
            Console.ResetColor();
            Console.WriteLine("   * A classe de negócio 'ProcessadorDePedidosTightlyCoupled' instancia diretamente");
            Console.WriteLine("     a classe concreta 'EnviadorDeEmail' usando 'new'.");
            Console.WriteLine("   * Se quisermos mudar o envio para SMS ou WhatsApp, precisaremos alterar e recompilar");
            Console.WriteLine("     a nossa classe principal de alto nível!\n");

            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("[Executando Cenário Acoplado...]");
            Console.ResetColor();
            
            var processadorAcoplado = new ProcessadorDePedidosTightlyCoupled();
            processadorAcoplado.ProcessarPedido(1001, "cliente@email.com");

            Console.WriteLine();

            // ==========================================
            // CENÁRIO 2: APLICANDO O DIP (Acoplamento Fraco)
            // ==========================================
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("--- CENÁRIO DEPOIS (Aplicando o DIP) ---");
            Console.ResetColor();
            Console.WriteLine("   * Criamos a abstração (Interface): 'IServicoNotificacao'.");
            Console.WriteLine("   * A classe de alto nível 'ProcessadorDePedidosDecoupled' depende APENAS da interface.");
            Console.WriteLine("   * As classes de baixo nível ('ServicoEmail', 'ServicoSms') dependem e implementam a interface.");
            Console.WriteLine("   * Invertemos a dependência! Agora a classe de alto nível não se importa com QUEM");
            Console.WriteLine("     está enviando, desde que ele obedeça ao contrato da interface.\n");

            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("[Executando Cenário Desacoplado 1: Enviando por Email]");
            Console.ResetColor();
            
            // Injetamos o Email no Processador (DIP + DI)
            IServicoNotificacao servicoEmail = new ServicoEmail();
            var processadorDecoupled1 = new ProcessadorDePedidosDecoupled(servicoEmail);
            processadorDecoupled1.ProcessarPedido(2002, "cliente@email.com");

            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("[Executando Cenário Desacoplado 2: Trocando para SMS sem alterar a Classe de Pedido!]");
            Console.ResetColor();

            // Trocamos a dependência para SMS! A classe ProcessadorDePedidosDecoupled continua exatamente igual.
            IServicoNotificacao servicoSms = new ServicoSms();
            var processadorDecoupled2 = new ProcessadorDePedidosDecoupled(servicoSms);
            processadorDecoupled2.ProcessarPedido(3003, "+55 11 99999-9999");

            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("RESUMO:");
            Console.ResetColor();
            Console.WriteLine("   Ao usar o DIP, tornamos nosso sistema ALTAMENTE EXTENSÍVEL.");
            Console.WriteLine("   Para adicionar uma notificação por WhatsApp, basta criar uma classe 'ServicoWhatsApp'");
            Console.WriteLine("   que implementa 'IServicoNotificacao' e passá-la. A classe de Pedido não muda 1 linha!\n");

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Pressione qualquer tecla para voltar ao menu principal...");
            Console.ResetColor();
            Console.ReadKey();
        }
    }

    // =========================================================================
    // --- 1. ESTRUTURA DO ANTES (VIOLANDO DIP - ACOPLADO) ---
    // =========================================================================

    // Classe de Baixo Nível (Detalhe)
    public class EnviadorDeEmail
    {
        public void Enviar(string destinatario, string mensagem)
        {
            Console.WriteLine($"      [EMAIL] Enviando para {destinatario}: {mensagem}");
        }
    }

    // Classe de Alto Nível (Negócio)
    public class ProcessadorDePedidosTightlyCoupled
    {
        // ERRO DO DIP: Acoplamento direto com a classe concreta!
        private EnviadorDeEmail _enviadorEmail = new EnviadorDeEmail();

        public void ProcessarPedido(int pedidoId, string emailCliente)
        {
            Console.WriteLine($"   * Processando pedido #{pedidoId}...");
            // Lógica de negócio...
            
            // Enviando notificação
            _enviadorEmail.Enviar(emailCliente, $"Seu pedido #{pedidoId} foi processado!");
        }
    }

    // =========================================================================
    // --- 2. ESTRUTURA DO DEPOIS (APLICANDO DIP - DESACOPLADO) ---
    // =========================================================================

    // A Abstração (Contrato)
    public interface IServicoNotificacao
    {
        void EnviarNotificacao(string destinatario, string mensagem);
    }

    // Baixo Nível - Detalhe 1 (Email)
    public class ServicoEmail : IServicoNotificacao
    {
        public void EnviarNotificacao(string destinatario, string mensagem)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"      [EMAIL - DECOUPLED] Enviando para {destinatario}: {mensagem}");
            Console.ResetColor();
        }
    }

    // Baixo Nível - Detalhe 2 (SMS)
    public class ServicoSms : IServicoNotificacao
    {
        public void EnviarNotificacao(string destinatario, string mensagem)
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine($"      [SMS - DECOUPLED] Enviando para {destinatario}: {mensagem}");
            Console.ResetColor();
        }
    }

    // Alto Nível - Negócio
    public class ProcessadorDePedidosDecoupled
    {
        // A classe não sabe que serviço está usando, apenas conhece a Abstração!
        private readonly IServicoNotificacao _servicoNotificacao;

        // A dependência é INJETADA no construtor (Injeção de Dependência)
        public ProcessadorDePedidosDecoupled(IServicoNotificacao servicoNotificacao)
        {
            _servicoNotificacao = servicoNotificacao;
        }

        public void ProcessarPedido(int pedidoId, string destinatario)
        {
            Console.WriteLine($"   * [DIP] Processando pedido #{pedidoId}...");
            // Lógica de negócio...
            
            // Dependência sendo chamada via Interface
            _servicoNotificacao.EnviarNotificacao(destinatario, $"Seu pedido #{pedidoId} foi processado via DIP!");
        }
    }
}
