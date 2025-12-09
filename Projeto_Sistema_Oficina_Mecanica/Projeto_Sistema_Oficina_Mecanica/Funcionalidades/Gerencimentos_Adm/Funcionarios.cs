// Importa namespaces necessários
using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace Projeto_Sistema_Oficina_Mecanica.Funcionalidades.Gerenciamentos.Gerenciamentos
{
    internal class Funcionarios
    {
        // String de conexão com o banco de dados SQL Server
        private string connectionString = "Server=NOTEBOOK-ISAQUE;Database=DB_Mecanica;Trusted_Connection=True;TrustServerCertificate=True;";

        // Método principal para gerenciar funcionários (menu interativo)
        public void GerenciarFuncionarios()
        {
            while (true) // Loop infinito até o usuário escolher sair
            {
                // Exibe opções do menu
                Console.WriteLine();
                Console.WriteLine("Gerenciamento de Funcionários:");
                Console.WriteLine("1 - Adicionar Funcionário");
                Console.WriteLine("2 - Listar Funcionários");
                Console.WriteLine("3 - Atualizar Funcionário");
                Console.WriteLine("4 - Remover Funcionário");
                Console.WriteLine("0 - Voltar ao Menu Principal");
                Console.Write("Opção: ");

                // Lê entrada do usuário e valida se é número
                var input = Console.ReadLine();
                if (!int.TryParse(input, out int opcao))
                {
                    Console.WriteLine("Entrada inválida. Tente novamente.");
                    continue;
                }

                // Executa ação conforme opção escolhida
                switch (opcao)
                {
                    case 1: AdicionarFuncionario(); break;
                    case 2: ListarFuncionarios(); break;
                    case 3: AtualizarFuncionario(); break;
                    case 4: RemoverFuncionario(); break;
                    case 0: return;
                    default: Console.WriteLine("Opção inválida."); break;
                }
            }
        }

        // Método para adicionar um novo funcionário
        private void AdicionarFuncionario()
        {
            Console.WriteLine("=== Adicionar Funcionário ===");
            Console.Write("Nome: "); string nome = Console.ReadLine() ?? "";
            Console.Write("Cargo: "); string cargo = Console.ReadLine() ?? "";
            Console.Write("Telefone: "); string telefone = Console.ReadLine() ?? "";
            Console.Write("Email: "); string email = Console.ReadLine() ?? "";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string sql = @"INSERT INTO Funcionarios 
                               (Nome_Funcionario, Cargo_Funcionario, Telefone_Funcionario, Email_Funcionario)
                               VALUES (@Nome, @Cargo, @Telefone, @Email)";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Nome", nome);
                    cmd.Parameters.AddWithValue("@Cargo", cargo);
                    cmd.Parameters.AddWithValue("@Telefone", telefone);
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.ExecuteNonQuery();
                }
            }
            Console.WriteLine("Funcionário adicionado com sucesso!");
        }

        // Método para listar todos os funcionários
        private void ListarFuncionarios()
        {
            Console.WriteLine("=== Lista de Funcionários ===");
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string sql = "SELECT Id_Funcionario, Nome_Funcionario, Cargo_Funcionario, Telefone_Funcionario, Email_Funcionario FROM Funcionarios";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine($"ID: {reader["Id_Funcionario"]}, Nome: {reader["Nome_Funcionario"]}, Cargo: {reader["Cargo_Funcionario"]}, Telefone: {reader["Telefone_Funcionario"]}, Email: {reader["Email_Funcionario"]}");
                    }
                }
            }
        }

        // Método para atualizar dados de um funcionário
        private void AtualizarFuncionario()
        {
            Console.WriteLine("=== Atualizar Funcionário ===");
            Console.Write("Informe o ID do funcionário: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("ID inválido.");
                return;
            }

            Console.Write("Novo Nome: "); string nome = Console.ReadLine() ?? "";
            Console.Write("Novo Cargo: "); string cargo = Console.ReadLine() ?? "";
            Console.Write("Novo Telefone: "); string telefone = Console.ReadLine() ?? "";
            Console.Write("Novo Email: "); string email = Console.ReadLine() ?? "";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string sql = @"UPDATE Funcionarios 
                               SET Nome_Funcionario=@Nome, Cargo_Funcionario=@Cargo, Telefone_Funcionario=@Telefone, Email_Funcionario=@Email
                               WHERE Id_Funcionario=@Id";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Nome", nome);
                    cmd.Parameters.AddWithValue("@Cargo", cargo);
                    cmd.Parameters.AddWithValue("@Telefone", telefone);
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@Id", id);
                    int rows = cmd.ExecuteNonQuery();
                    Console.WriteLine(rows > 0 ? "Funcionário atualizado com sucesso!" : "Funcionário não encontrado.");
                }
            }
        }

        // Método para remover um funcionário
        private void RemoverFuncionario()
        {
            Console.WriteLine("=== Remover Funcionário ===");
            Console.Write("Informe o ID do funcionário: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("ID inválido.");
                return;
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string sql = "DELETE FROM Funcionarios WHERE Id_Funcionario=@Id";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    int rows = cmd.ExecuteNonQuery();
                    Console.WriteLine(rows > 0 ? "Funcionário removido com sucesso!" : "Funcionário não encontrado.");
                }
            }
        }

        // Classe interna que representa um funcionário (modelo de dados)
        private class Funcionario
        {
            public int Id { get; set; }              // ID do funcionário
            public string? Nome { get; set; }        // Nome do funcionário
            public string? Cargo { get; set; }       // Cargo do funcionário
            public string? Telefone { get; set; }    // Telefone do funcionário
            public string? Email { get; set; }       // Email do funcionário
        }
    }
}