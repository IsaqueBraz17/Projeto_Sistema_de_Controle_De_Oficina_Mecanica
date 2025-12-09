using System;
using Microsoft.Data.SqlClient;

namespace Projeto_Sistema_Oficina_Mecanica.Funcionalidades.Gerenciamentos
{
    internal class Services
    {
        // String de conexão com o banco de dados SQL Server
        private string connectionString = "Server=NOTEBOOK-ISAQUE;Database=DB_Mecanica;Trusted_Connection=True;TrustServerCertificate=True;";

        // Menu principal de serviços
        public void GerenciarServicos()
        {
            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("=== Gerenciamento de Serviços ===");
                Console.WriteLine("1 - Adicionar Serviço");
                Console.WriteLine("2 - Listar Serviços");
                Console.WriteLine("3 - Atualizar Serviço");
                Console.WriteLine("4 - Remover Serviço");
                Console.WriteLine("0 - Voltar ao Menu Principal");
                Console.Write("Opção: ");

                var input = Console.ReadLine();
                if (!int.TryParse(input, out int opcao))
                {
                    Console.WriteLine("Entrada inválida. Tente novamente.");
                    continue;
                }

                switch (opcao)
                {
                    case 1: AdicionarServico(); break;
                    case 2: ListarServicos(); break;
                    case 3: AtualizarServico(); break;
                    case 4: RemoverServico(); break;
                    case 0: return;
                    default: Console.WriteLine("Opção inválida."); break;
                }
            }
        }

        // ---------------- SERVIÇOS ----------------
        private void AdicionarServico()
        {
            Console.WriteLine("=== Adicionar Serviço ===");
            Console.Write("Nome: "); string nome = Console.ReadLine() ?? "";
            Console.Write("Descrição: "); string descricao = Console.ReadLine() ?? "";
            Console.Write("Preço: "); decimal preco = decimal.Parse(Console.ReadLine() ?? "0");

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string sql = @"INSERT INTO Servicos (Nome_Servico, Descricao_Servico, Preco_Servico)
                               VALUES (@Nome, @Descricao, @Preco)";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Nome", nome);
                    cmd.Parameters.AddWithValue("@Descricao", descricao);
                    cmd.Parameters.AddWithValue("@Preco", preco);
                    cmd.ExecuteNonQuery();
                }
            }
            Console.WriteLine("Serviço adicionado com sucesso!");
        }

        private void ListarServicos()
        {
            Console.WriteLine("=== Lista de Serviços ===");
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string sql = "SELECT * FROM Servicos";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine($"ID: {reader["Id_Servico"]}, Nome: {reader["Nome_Servico"]}, Descrição: {reader["Descricao_Servico"]}, Preço: {reader["Preco_Servico"]}");
                    }
                }
            }
        }

        private void AtualizarServico()
        {
            Console.WriteLine("=== Atualizar Serviço ===");
            Console.Write("Informe o ID do serviço: ");
            if (!int.TryParse(Console.ReadLine(), out int id)) return;

            Console.Write("Novo Nome: "); string nome = Console.ReadLine() ?? "";
            Console.Write("Nova Descrição: "); string descricao = Console.ReadLine() ?? "";
            Console.Write("Novo Preço: "); decimal preco = decimal.Parse(Console.ReadLine() ?? "0");

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string sql = @"UPDATE Servicos 
                               SET Nome_Servico=@Nome, Descricao_Servico=@Descricao, Preco_Servico=@Preco
                               WHERE Id_Servico=@Id";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Nome", nome);
                    cmd.Parameters.AddWithValue("@Descricao", descricao);
                    cmd.Parameters.AddWithValue("@Preco", preco);
                    cmd.Parameters.AddWithValue("@Id", id);
                    int rows = cmd.ExecuteNonQuery();
                    Console.WriteLine(rows > 0 ? "Serviço atualizado com sucesso!" : "Serviço não encontrado.");
                }
            }
        }

        private void RemoverServico()
        {
            Console.WriteLine("=== Remover Serviço ===");
            Console.Write("Informe o ID do serviço: ");
            if (!int.TryParse(Console.ReadLine(), out int id)) return;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string sql = "DELETE FROM Servicos WHERE Id_Servico=@Id";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    int rows = cmd.ExecuteNonQuery();
                    Console.WriteLine(rows > 0 ? "Serviço removido com sucesso!" : "Serviço não encontrado.");
                }
            }
        }
    }
}