// Importa namespaces necessários
using System;                       // Fornece classes básicas como Console, String, etc.
using System.Collections.Generic;   // Permite uso de coleções genéricas
using Microsoft.Data.SqlClient;

namespace Projeto_Sistema_Oficina_Mecanica.Funcionalidades.Gerenciamentos.Gerenciamentos
{
    internal class Estoque
    {
        // String de conexão com o banco de dados SQL Server
        private string connectionString = "Server=NOTEBOOK-ISAQUE;Database=DB_Mecanica;Trusted_Connection=True;TrustServerCertificate=True;";

        // Método principal para gerenciar estoque (menu interativo)
        public void GerenciaEstoque()
        {
            while (true) // Loop infinito até o usuário escolher sair
            {
                // Exibe opções do menu
                Console.WriteLine();
                Console.WriteLine("Gerenciamento de Estoque:");
                Console.WriteLine("1 - Adicionar Produto");
                Console.WriteLine("2 - Listar Produtos");
                Console.WriteLine("3 - Atualizar Produto");
                Console.WriteLine("4 - Remover Produto");
                Console.WriteLine("5 - Abrir Menu de Fornecedores"); // <<< NOVA OPÇÃO
                Console.WriteLine("0 - Voltar ao Menu Principal");
                Console.Write("Opção: ");

                // Lê entrada do usuário e valida se é número
                var input = Console.ReadLine();
                if (!int.TryParse(input, out int opcaoEstoque))
                {
                    Console.WriteLine("Entrada inválida. Tente novamente.");
                    continue; // Volta ao início do loop
                }

                // Executa ação conforme opção escolhida
                switch (opcaoEstoque)
                {
                    case 1:
                        AdicionarProduto(); // Chama método para adicionar produto
                        break;
                    case 2:
                        ListarProdutos();   // Chama método para listar produtos
                        break;
                    case 3:
                        AtualizarProduto(); // Chama método para atualizar produto
                        break;
                    case 4:
                        RemoverProduto();   // Chama método para remover produto
                        break;
                    case 5:
                        // Instancia a classe Fornecedores e abre o menu
                        Fornecedores fornecedores = new Fornecedores();
                        fornecedores.GerenciarFornecedores();
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

        // Método para adicionar um novo produto no estoque
        private void AdicionarProduto()
        {
            Console.WriteLine("=== Adicionar Produto ===");

            // Solicita dados do produto
            Console.Write("Nome: ");
            string nome = Console.ReadLine() ?? "";
            Console.Write("Quantidade: ");
            int qtd = int.Parse(Console.ReadLine() ?? "0");
            Console.Write("Preço: ");
            decimal preco = decimal.Parse(Console.ReadLine() ?? "0");
            Console.Write("Marca: ");
            string marca = Console.ReadLine() ?? "";
            Console.Write("Categoria: ");
            string categoria = Console.ReadLine() ?? "";

            // Conexão com o banco
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open(); // Abre conexão

                // Comando SQL para inserir produto
                string sql = @"INSERT INTO Estoque_Produtos 
                               (Nome_Produto, QTD_Produto, Preco_Produto, Marca_Produto, Categoria_Produto)
                               VALUES (@Nome, @QTD, @Preco, @Marca, @Categoria)";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    // Passa parâmetros para evitar SQL Injection
                    cmd.Parameters.AddWithValue("@Nome", nome);
                    cmd.Parameters.AddWithValue("@QTD", qtd);
                    cmd.Parameters.AddWithValue("@Preco", preco);
                    cmd.Parameters.AddWithValue("@Marca", marca);
                    cmd.Parameters.AddWithValue("@Categoria", categoria);

                    // Executa comando
                    cmd.ExecuteNonQuery();
                }
            }
            Console.WriteLine("Produto adicionado com sucesso!");
        }

        // Método para listar todos os produtos cadastrados
        private void ListarProdutos()
        {
            Console.WriteLine("=== Lista de Produtos ===");

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open(); // Abre conexão

                // Comando SQL para selecionar produtos
                string sql = "SELECT Id_Produto, Nome_Produto, QTD_Produto, Preco_Produto, Marca_Produto, Categoria_Produto FROM Estoque_Produtos";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                using (SqlDataReader reader = cmd.ExecuteReader()) // Lê resultados
                {
                    while (reader.Read()) // Percorre cada linha retornada
                    {
                        // Exibe dados do produto
                        Console.WriteLine($"ID: {reader["Id_Produto"]}, Nome: {reader["Nome_Produto"]}, Quantidade: {reader["QTD_Produto"]}, Preço: {reader["Preco_Produto"]}, Marca: {reader["Marca_Produto"]}, Categoria: {reader["Categoria_Produto"]}");
                    }
                }
            }
        }

        // Método para atualizar dados de um produto existente
        private void AtualizarProduto()
        {
            Console.WriteLine("=== Atualizar Produto ===");

            // Solicita ID do produto
            Console.Write("Informe o ID do produto: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("ID inválido.");
                return;
            }

            // Solicita novos dados
            Console.Write("Novo Nome: ");
            string nome = Console.ReadLine() ?? "";
            Console.Write("Nova Quantidade: ");
            int qtd = int.Parse(Console.ReadLine() ?? "0");
            Console.Write("Novo Preço: ");
            decimal preco = decimal.Parse(Console.ReadLine() ?? "0");

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open(); // Abre conexão

                // Comando SQL para atualizar produto
                string sql = @"UPDATE Estoque_Produtos 
                               SET Nome_Produto=@Nome, QTD_Produto=@QTD, Preco_Produto=@Preco
                               WHERE Id_Produto=@Id";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    // Passa parâmetros
                    cmd.Parameters.AddWithValue("@Nome", nome);
                    cmd.Parameters.AddWithValue("@QTD", qtd);
                    cmd.Parameters.AddWithValue("@Preco", preco);
                    cmd.Parameters.AddWithValue("@Id", id);

                    // Executa comando e verifica se houve atualização
                    int rows = cmd.ExecuteNonQuery();
                    if (rows > 0)
                        Console.WriteLine("Produto atualizado com sucesso!");
                    else
                        Console.WriteLine("Produto não encontrado.");
                }
            }
        }

        // Método para remover um produto pelo ID
        private void RemoverProduto()
        {
            Console.WriteLine("=== Remover Produto ===");

            // Solicita ID do produto
            Console.Write("Informe o ID do produto: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("ID inválido.");
                return;
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open(); // Abre conexão

                // Comando SQL para deletar produto
                string sql = "DELETE FROM Estoque_Produtos WHERE Id_Produto=@Id";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", id);

                    // Executa comando e verifica se houve remoção
                    int rows = cmd.ExecuteNonQuery();
                    if (rows > 0)
                        Console.WriteLine("Produto removido com sucesso!");
                    else
                        Console.WriteLine("Produto não encontrado.");
                }
            }
        }

        // Classe interna que representa um produto (modelo de dados)
        private class Produto
        {
            public int Id { get; set; }              // ID do produto
            public string? Nome { get; set; }        // Nome do produto
            public int Quantidade { get; set; }      // Quantidade em estoque
            public decimal Preco { get; set; }       // Preço do produto
            public string? Marca { get; set; }       // Marca do produto
            public string? Categoria { get; set; }   // Categoria do produto
        }
    }
}