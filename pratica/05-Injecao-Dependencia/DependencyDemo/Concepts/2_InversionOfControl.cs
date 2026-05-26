using System;

namespace DependencyDemo.Concepts
{
    public static class InversionOfControlDemo
    {
        public static void RunDemo()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("===============================================================================");
            Console.WriteLine("                CONCEITO 2: INVERSÃO DE CONTROLE (IoC)                         ");
            Console.WriteLine("===============================================================================");
            Console.ResetColor();

            Console.WriteLine("\nInversão de Controle (IoC - Inversion of Control) não é uma biblioteca ou padrão");
            Console.WriteLine("de projeto específico, mas sim um PRINCÍPIO de design de software.");
            Console.WriteLine("\nNo fluxo tradicional, seu código customizado controla o fluxo do programa e chama");
            Console.WriteLine("bibliotecas úteis para realizar tarefas específicas (Ex: Ler banco -> Validar -> Salvar).");
            
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("\n[ Hollywood Principle: \"Don't call us, we'll call you\" (Não nos ligue, nós ligamos para você) ]");
            Console.ResetColor();
            Console.WriteLine("Com IoC, o fluxo é INVERTIDO: uma estrutura genérica (como um Framework, Container");
            Console.WriteLine("ou Classe Base) toma o controle do fluxo da aplicação e chama o SEU código nos pontos");
            Console.WriteLine("corretos de extensão.\n");

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("EXEMPLO PRÁTICO: Padrão Template Method");
            Console.ResetColor();
            Console.WriteLine("A classe base abstrata 'WorkflowProcessador' controla a ordem de execução do fluxo.");
            Console.WriteLine("O programador apenas escreve o comportamento customizado de cada etapa. A classe base");
            Console.WriteLine("decide QUANDO e COMO executar.\n");

            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("=== Executando Workflow 1: Importação de CSV ===");
            Console.ResetColor();
            WorkflowProcessador workflowCsv = new ProcessadorImportacaoCsv();
            workflowCsv.ExecutarProcesso(); // O controle do fluxo está na classe base!

            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("=== Executando Workflow 2: Processamento de API ===");
            Console.ResetColor();
            WorkflowProcessador workflowApi = new ProcessadorProcessamentoApi();
            workflowApi.ExecutarProcesso(); // Mesmo fluxo coordenado, comportamentos diferentes!

            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("MÉTODOS DE APLICAR IoC:");
            Console.ResetColor();
            Console.WriteLine("   - Padrão de Projeto de Template (como este exemplo).");
            Console.WriteLine("   - Padrão de Projeto Strategy.");
            Console.WriteLine("   - Eventos e Callbacks (onde o framework dispara quando o usuário clica).");
            Console.WriteLine("   - Injeção de Dependência (DI) - a forma mais famosa de IoC em arquiteturas web.\n");

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Pressione qualquer tecla para voltar ao menu principal...");
            Console.ResetColor();
            Console.ReadKey();
        }
    }

    // --- ESTRUTURA IoC (TEMPLATE METHOD) ---

    // A classe base é o "Framework". Ela tem o controle de fluxo.
    public abstract class WorkflowProcessador
    {
        // O controle de fluxo está selado aqui! O desenvolvedor não pode mudar a ordem das etapas.
        public void ExecutarProcesso()
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("[Controle Base] Iniciando o ciclo de processamento...");
            Console.ResetColor();

            CarregarDados();
            
            if (ValidarDados())
            {
                ProcessarDados();
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine("[Controle Base] Sucesso! Salvando alterações.");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("[Controle Base] Falha na validação. Encerrando.");
                Console.ResetColor();
            }

            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("[Controle Base] Finalizando recursos e encerrando.");
            Console.ResetColor();
        }

        // Passos abstratos ou virtuais que as subclasses customizam:
        protected abstract void CarregarDados();
        protected abstract bool ValidarDados();
        protected abstract void ProcessarDados();
    }

    // Subclasse 1: Customização do desenvolvedor
    public class ProcessadorImportacaoCsv : WorkflowProcessador
    {
        protected override void CarregarDados()
        {
            Console.WriteLine("   * CSV: Lendo linhas do arquivo 'clientes.csv'...");
        }

        protected override bool ValidarDados()
        {
            Console.WriteLine("   * CSV: Validando delimitadores e colunas...");
            return true; // Sucesso
        }

        protected override void ProcessarDados()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("   * CSV: Convertendo linhas em objetos do domínio C#.");
            Console.ResetColor();
        }
    }

    // Subclasse 2: Outra customização do desenvolvedor
    public class ProcessadorProcessamentoApi : WorkflowProcessador
    {
        protected override void CarregarDados()
        {
            Console.WriteLine("   * API: Realizando requisição HTTP GET para o servidor remoto...");
        }

        protected override bool ValidarDados()
        {
            Console.WriteLine("   * API: Validando payload JSON contra o schema esperado...");
            // Exemplo de falha de validação simulada:
            return false;
        }

        protected override void ProcessarDados()
        {
            // Este método nunca será chamado porque o controle base decidiu parar na validação!
            Console.WriteLine("   * API: Salvando respostas no cache.");
        }
    }
}
