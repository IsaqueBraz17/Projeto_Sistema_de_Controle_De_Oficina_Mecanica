
using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace Projeto_Sistema_Oficina_Mecanica.Funcionalidades.Gerenciamento
{
    internal class Clientes
    {
        // String de conexão com o banco de dados SQL Server
        private string connectionString = "Server=NOTEBOOK-ISAQUE;Database=DB_Mecanica;Trusted_Connection=True;TrustServerCertificate=True;";

        // Método principal para gerenciar clientes (menu interativo)
        public void GerenciarClientes()
        {
            while (true) // Loop infinito até o usuário escolher sair
            {
                // Exibe opções do menu
                Console.WriteLine();
                Console.WriteLine("Gerenciamento de Clientes:");
                Console.WriteLine("1 - Adicionar Cliente");
                Console.WriteLine("2 - Listar Clientes");
                Console.WriteLine("3 - Atualizar Cliente");
                Console.WriteLine("4 - Remover Cliente");
                Console.WriteLine("0 - Voltar ao Menu Principal");
                Console.Write("Opção: ");

                // Lê entrada do usuário e valida se é número
                var input = Console.ReadLine();
                if (!int.TryParse(input, out int opcaoClientes))
                {
                    Console.WriteLine("Entrada inválida. Tente novamente.");
                    continue; // Volta ao início do loop
                }

                // Executa ação conforme opção escolhida
                switch (opcaoClientes)
                {
                    case 1:
                        AdicionarCliente(); // Chama método para adicionar cliente
                        break;
                    case 2:
                        ListarClientes();   // Chama método para listar clientes
                        break;
                    case 3:
                        AtualizarCliente(); // Chama método para atualizar cliente
                        break;
                    case 4:
                        RemoverCliente();   // Chama método para remover cliente
                        break;
                    case 0:
                        Console.WriteLine("Voltando ao Menu Principal.");
                        return; // Sai do método e volta ao menu principal
                    default:
                        Console.WriteLine("Opção inválida. Tente novamente.");
                        break;
                }
            }
        }

        // Método para adicionar um novo cliente no banco
        private void AdicionarCliente()
        {
            Console.WriteLine("=== Adicionar Cliente ===");

            // Solicita dados do cliente
            Console.Write("Nome: ");
            string nome = Console.ReadLine() ?? "";
            Console.Write("CPF (11 dígitos): ");
            string cpf = Console.ReadLine() ?? "";
            Console.Write("RG: ");
            string rg = Console.ReadLine() ?? "";
            Console.Write("Telefone: ");
            string telefone = Console.ReadLine() ?? "";
            Console.Write("Email: ");
            string email = Console.ReadLine() ?? "";
            Console.Write("CEP: ");
            string cep = Console.ReadLine() ?? "";

            // Conexão com o banco
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open(); // Abre conexão

                // Comando SQL para inserir cliente
                string sql = @"INSERT INTO Cadastro_Clientes 
                               (Nome_Cliente, CPF_Cliente, RG_Cliente, Telefone_Cliente, Email_Cliente, CEP_Cliente)
                               VALUES (@Nome, @CPF, @RG, @Telefone, @Email, @CEP)";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    // Passa parâmetros para evitar SQL Injection
                    cmd.Parameters.AddWithValue("@Nome", nome);
                    cmd.Parameters.AddWithValue("@CPF", cpf);
                    cmd.Parameters.AddWithValue("@RG", rg);
                    cmd.Parameters.AddWithValue("@Telefone", telefone);
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@CEP", cep);

                    // Executa comando
                    cmd.ExecuteNonQuery();
                }
            }
            Console.WriteLine("Cliente adicionado com sucesso!");
        }

        // Método para listar todos os clientes cadastrados
        private void ListarClientes()
        {
            Console.WriteLine("=== Lista de Clientes ===");

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open(); // Abre conexão

                // Comando SQL para selecionar clientes
                string sql = "SELECT Id_Cliente, Nome_Cliente, CPF_Cliente, RG_Cliente, Telefone_Cliente, Email_Cliente, CEP_Cliente FROM Cadastro_Clientes";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                using (SqlDataReader reader = cmd.ExecuteReader()) // Lê resultados
                {
                    while (reader.Read()) // Percorre cada linha retornada
                    {
                        // Exibe dados do cliente
                        Console.WriteLine($"ID: {reader["Id_Cliente"]}, Nome: {reader["Nome_Cliente"]}, CPF: {reader["CPF_Cliente"]}, RG: {reader["RG_Cliente"]}, Telefone: {reader["Telefone_Cliente"]}, Email: {reader["Email_Cliente"]}, CEP: {reader["CEP_Cliente"]}");
                    }
                }
            }
        }

        // Método para atualizar dados de um cliente existente
        private void AtualizarCliente()
        {
            Console.WriteLine("=== Atualizar Cliente ===");

            // Solicita ID do cliente
            Console.Write("Informe o ID do cliente: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("ID inválido.");
                return;
            }

            // Solicita novos dados
            Console.Write("Novo Nome: ");
            string nome = Console.ReadLine() ?? "";
            Console.Write("Novo Telefone: ");
            string telefone = Console.ReadLine() ?? "";
            Console.Write("Novo Email: ");
            string email = Console.ReadLine() ?? "";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open(); // Abre conexão

                // Comando SQL para atualizar cliente
                string sql = @"UPDATE Cadastro_Clientes 
                               SET Nome_Cliente=@Nome, Telefone_Cliente=@Telefone, Email_Cliente=@Email
                               WHERE Id_Cliente=@Id";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    // Passa parâmetros
                    cmd.Parameters.AddWithValue("@Nome", nome);
                    cmd.Parameters.AddWithValue("@Telefone", telefone);
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@Id", id);

                    // Executa comando e verifica se houve atualização
                    int rows = cmd.ExecuteNonQuery();
                    if (rows > 0)
                        Console.WriteLine("Cliente atualizado com sucesso!");
                    else
                        Console.WriteLine("Cliente não encontrado.");
                }
            }
        }

        // Método para remover um cliente pelo ID
        private void RemoverCliente()
        {
            Console.WriteLine("=== Remover Cliente ===");

            // Solicita ID do cliente
            Console.Write("Informe o ID do cliente: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("ID inválido.");
                return;
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open(); // Abre conexão

                // Comando SQL para deletar cliente
                string sql = "DELETE FROM Cadastro_Clientes WHERE Id_Cliente=@Id";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", id);

                    // Executa comando e verifica se houve remoção
                    int rows = cmd.ExecuteNonQuery();
                    if (rows > 0)
                        Console.WriteLine("Cliente removido com sucesso!");
                    else
                        Console.WriteLine("Cliente não encontrado.");
                }
            }
        }

        // Classe interna que representa um cliente (modelo de dados)
        private class Cliente
        {
            public int Id { get; set; }          // ID do cliente
            public string? Nome { get; set; }    // Nome do cliente
            public string? CPF { get; set; }     // CPF do cliente
            public string? RG { get; set; }      // RG do cliente
            public string? Telefone { get; set; }// Telefone do cliente
            public string? Email { get; set; }   // Email do cliente
            public string? CEP { get; set; }     // CEP do cliente
        }
    }
}