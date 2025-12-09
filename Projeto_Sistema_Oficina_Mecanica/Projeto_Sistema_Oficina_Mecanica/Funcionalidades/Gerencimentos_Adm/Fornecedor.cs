// Importa namespaces necessários
using System;                       // Fornece classes básicas como Console, String, etc.
using System.Collections.Generic;   // Permite uso de coleções genéricas
using Microsoft.Data.SqlClient;

namespace Projeto_Sistema_Oficina_Mecanica.Funcionalidades.Gerenciamentos.Gerenciamentos
{
    internal class Fornecedores
    {
        // String de conexão com o banco de dados SQL Server
        private string connectionString = "Server=NOTEBOOK-ISAQUE;Database=DB_Mecanica;Trusted_Connection=True;TrustServerCertificate=True;";

        // Método principal para gerenciar fornecedores (menu interativo)
        public void GerenciarFornecedores()
        {
            while (true) // Loop infinito até o usuário escolher sair
            {
                // Exibe opções do menu
                Console.WriteLine();
                Console.WriteLine("Gerenciamento de Fornecedores:");
                Console.WriteLine("1 - Adicionar Fornecedor");
                Console.WriteLine("2 - Listar Fornecedores");
                Console.WriteLine("3 - Atualizar Fornecedor");
                Console.WriteLine("4 - Remover Fornecedor");
                Console.WriteLine("0 - Voltar ao Menu Principal");
                Console.Write("Opção: ");

                // Lê entrada do usuário e valida se é número
                var input = Console.ReadLine();
                if (!int.TryParse(input, out int opcaoFornecedores))
                {
                    Console.WriteLine("Entrada inválida. Tente novamente.");
                    continue; // Volta ao início do loop
                }

                // Executa ação conforme opção escolhida
                switch (opcaoFornecedores)
                {
                    case 1:
                        AdicionarFornecedor(); // Chama método para adicionar fornecedor
                        break;
                    case 2:
                        ListarFornecedores();  // Chama método para listar fornecedores
                        break;
                    case 3:
                        AtualizarFornecedor(); // Chama método para atualizar fornecedor
                        break;
                    case 4:
                        RemoverFornecedor();   // Chama método para remover fornecedor
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

        // Método para adicionar um novo fornecedor no banco
        private void AdicionarFornecedor()
        {
            Console.WriteLine("=== Adicionar Fornecedor ===");

            // Solicita dados do fornecedor
            Console.Write("Nome: ");
            string nome = Console.ReadLine() ?? "";
            Console.Write("CEP: ");
            string cep = Console.ReadLine() ?? "";
            Console.Write("Categoria: ");
            string categoria = Console.ReadLine() ?? "";
            Console.Write("Estado (UF): ");
            string estado = Console.ReadLine() ?? "";
            Console.Write("Cidade: ");
            string cidade = Console.ReadLine() ?? "";
            Console.Write("Email: ");
            string email = Console.ReadLine() ?? "";
            Console.Write("Telefone: ");
            string telefone = Console.ReadLine() ?? "";

            // Conexão com o banco
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open(); // Abre conexão

                // Comando SQL para inserir fornecedor
                string sql = @"INSERT INTO Fornecedores 
                               (Nome_Fornecedor, CEP_Fornecedor, Categoria_Fornecedor, Estado_Fornecedor, Cidade_Fornecedor, Email_Fornecedor, Telefone_Fornecedor)
                               VALUES (@Nome, @CEP, @Categoria, @Estado, @Cidade, @Email, @Telefone)";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    // Passa parâmetros para evitar SQL Injection
                    cmd.Parameters.AddWithValue("@Nome", nome);
                    cmd.Parameters.AddWithValue("@CEP", cep);
                    cmd.Parameters.AddWithValue("@Categoria", categoria);
                    cmd.Parameters.AddWithValue("@Estado", estado);
                    cmd.Parameters.AddWithValue("@Cidade", cidade);
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@Telefone", telefone);

                    // Executa comando
                    cmd.ExecuteNonQuery();
                }
            }
            Console.WriteLine("Fornecedor adicionado com sucesso!");
        }

        // Método para listar todos os fornecedores cadastrados
        private void ListarFornecedores()
        {
            Console.WriteLine("=== Lista de Fornecedores ===");

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open(); // Abre conexão

                // Comando SQL para selecionar fornecedores
                string sql = "SELECT Id_Fornecedor, Nome_Fornecedor, CEP_Fornecedor, Categoria_Fornecedor, Estado_Fornecedor, Cidade_Fornecedor, Email_Fornecedor, Telefone_Fornecedor FROM Fornecedores";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                using (SqlDataReader reader = cmd.ExecuteReader()) // Lê resultados
                {
                    while (reader.Read()) // Percorre cada linha retornada
                    {
                        // Exibe dados do fornecedor
                        Console.WriteLine($"ID: {reader["Id_Fornecedor"]}, Nome: {reader["Nome_Fornecedor"]}, CEP: {reader["CEP_Fornecedor"]}, Categoria: {reader["Categoria_Fornecedor"]}, Estado: {reader["Estado_Fornecedor"]}, Cidade: {reader["Cidade_Fornecedor"]}, Email: {reader["Email_Fornecedor"]}, Telefone: {reader["Telefone_Fornecedor"]}");
                    }
                }
            }
        }

        // Método para atualizar dados de um fornecedor existente
        private void AtualizarFornecedor()
        {
            Console.WriteLine("=== Atualizar Fornecedor ===");

            // Solicita ID do fornecedor
            Console.Write("Informe o ID do fornecedor: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("ID inválido.");
                return;
            }

            // Solicita novos dados
            Console.Write("Novo Nome: ");
            string nome = Console.ReadLine() ?? "";
            Console.Write("Novo Email: ");
            string email = Console.ReadLine() ?? "";
            Console.Write("Novo Telefone: ");
            string telefone = Console.ReadLine() ?? "";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open(); // Abre conexão

                // Comando SQL para atualizar fornecedor
                string sql = @"UPDATE Fornecedores 
                               SET Nome_Fornecedor=@Nome, Email_Fornecedor=@Email, Telefone_Fornecedor=@Telefone
                               WHERE Id_Fornecedor=@Id";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    // Passa parâmetros
                    cmd.Parameters.AddWithValue("@Nome", nome);
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@Telefone", telefone);
                    cmd.Parameters.AddWithValue("@Id", id);

                    // Executa comando e verifica se houve atualização
                    int rows = cmd.ExecuteNonQuery();
                    if (rows > 0)
                        Console.WriteLine("Fornecedor atualizado com sucesso!");
                    else
                        Console.WriteLine("Fornecedor não encontrado.");
                }
            }
        }

        // Método para remover um fornecedor pelo ID
        private void RemoverFornecedor()
        {
            Console.WriteLine("=== Remover Fornecedor ===");

            // Solicita ID do fornecedor
            Console.Write("Informe o ID do fornecedor: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("ID inválido.");
                return;
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open(); // Abre conexão

                // Comando SQL para deletar fornecedor
                string sql = "DELETE FROM Fornecedores WHERE Id_Fornecedor=@Id";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", id);

                    // Executa comando e verifica se houve remoção
                    int rows = cmd.ExecuteNonQuery();
                    if (rows > 0)
                        Console.WriteLine("Fornecedor removido com sucesso!");
                    else
                        Console.WriteLine("Fornecedor não encontrado.");
                }
            }
        }

        // Classe interna que representa um fornecedor (modelo de dados)
        private class Fornecedor
        {
            public int Id { get; set; }              // ID do fornecedor
            public string? Nome { get; set; }        // Nome do fornecedor
            public string? CEP { get; set; }         // CEP do fornecedor
            public string? Categoria { get; set; }   // Categoria do fornecedor
            public string? Estado { get; set; }      // Estado (UF) do fornecedor
            public string? Cidade { get; set; }      // Cidade do fornecedor
            public string? Email { get; set; }       // Email do fornecedor
            public string? Telefone { get; set; }    // Telefone do fornecedor
        }
    }
}