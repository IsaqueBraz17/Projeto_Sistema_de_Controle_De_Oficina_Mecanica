using Microsoft.Data.SqlClient; // necessário para conexão
using Projeto_Sistema_Oficina_Mecanica.Funcionalidades;
using Projeto_Sistema_Oficina_Mecanica.Funcionalidades.Gerenciamentos;
using Projeto_Sistema_Oficina_Mecanica.Funcionalidades.Gerenciamentos.Gerenciamentos;
using System;

namespace Projeto_Sistema_Oficina_Mecanica
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Seja bem-vindo ao Sistema de Oficina Mecânica!");

            // Teste de conexão com o banco
            string connectionString = "Server=NOTEBOOK-ISAQUE;Database=DB_Mecanica;Trusted_Connection=True;TrustServerCertificate=True;";
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    Console.WriteLine("✅ Conectado ao banco de dados com sucesso!");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("❌ Erro ao conectar ao banco de dados: " + ex.Message);
            }

            while (true) // Loop principal
            {
                Console.WriteLine();
                Console.WriteLine("=== Menu Principal ===");
                Console.WriteLine("1 - Gerenciamentos de Cadastros");
                Console.WriteLine("2 - Agendamentos de Serviços");
                Console.WriteLine("3 - Consultar Estoque de Peças");
                Console.WriteLine("0 - Sair");
                Console.Write("Opção: ");

                var input = Console.ReadLine();
                if (!int.TryParse(input, out int menuPrincipal))
                {
                    Console.WriteLine("Entrada inválida. Tente novamente.");
                    continue;
                }

                switch (menuPrincipal)
                {
                    case 1:
                        // Chama o menu de Gerenciamentos (Clientes, Funcionários, Fornecedores, Veículos, Serviços, Ordens de Serviço, Estoque)
                        Menu_Gerenciamento menu = new Menu_Gerenciamento();
                        menu.MenuPrincipal();
                        break;

                    case 2:
                        // Aqui você pode chamar diretamente o módulo de Serviços/Ordens de Serviço
                        Services servicos = new Services();
                        servicos.GerenciarServicos();
                        break;

                    case 3:
                        // Chama o módulo de Estoque
                        Estoque estoque = new Estoque();
                        estoque.GerenciaEstoque();
                        break;

                    case 0:
                        Console.WriteLine("Saindo do sistema...");
                        return; // Encerra o programa

                    default:
                        Console.WriteLine("Opção inválida. Tente novamente.");
                        break;
                }
            }
        }
    }
}