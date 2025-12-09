using System;
using Microsoft.Data.SqlClient;

namespace Projeto_Sistema_Oficina_Mecanica.Funcionalidades.Função_Adm
{
    internal class Ordem_Services
    {
        // String de conexão com o banco de dados SQL Server
        private string connectionString = "Server=NOTEBOOK-ISAQUE;Database=DB_Mecanica;Trusted_Connection=True;TrustServerCertificate=True;";

        // Menu principal de Ordens de Serviço
        public void GerenciarOrdens()
        {
            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("=== Gerenciamento de Ordens de Serviço ===");
                Console.WriteLine("1 - Abrir Ordem de Serviço");
                Console.WriteLine("2 - Listar Ordens de Serviço");
                Console.WriteLine("3 - Atualizar Ordem de Serviço");
                Console.WriteLine("4 - Fechar Ordem de Serviço");
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
                    case 1: AbrirOrdemServico(); break;
                    case 2: ListarOrdensServico(); break;
                    case 3: AtualizarOrdemServico(); break;
                    case 4: FecharOrdemServico(); break;
                    case 0: return;
                    default: Console.WriteLine("Opção inválida."); break;
                }
            }
        }

        // ---------------- ORDEM DE SERVIÇO ----------------
        private void AbrirOrdemServico()
        {
            Console.WriteLine("=== Abrir Ordem de Serviço ===");
            Console.Write("ID Cliente: "); int idCliente = int.Parse(Console.ReadLine() ?? "0");
            Console.Write("ID Veículo: "); int idVeiculo = int.Parse(Console.ReadLine() ?? "0");
            Console.Write("ID Funcionário: "); int idFuncionario = int.Parse(Console.ReadLine() ?? "0");
            Console.Write("Status (Aberta/Em andamento/Concluída): "); string status = Console.ReadLine() ?? "";
            Console.Write("Valor Total: "); decimal valor = decimal.Parse(Console.ReadLine() ?? "0");

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string sql = @"INSERT INTO Ordem_Servico 
                               (Id_Cliente, Id_Veiculo, Id_Funcionario, Data_Abertura, Status_OS, Valor_Total)
                               VALUES (@Cliente, @Veiculo, @Funcionario, GETDATE(), @Status, @Valor)";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Cliente", idCliente);
                    cmd.Parameters.AddWithValue("@Veiculo", idVeiculo);
                    cmd.Parameters.AddWithValue("@Funcionario", idFuncionario);
                    cmd.Parameters.AddWithValue("@Status", status);
                    cmd.Parameters.AddWithValue("@Valor", valor);
                    cmd.ExecuteNonQuery();
                }
            }
            Console.WriteLine("Ordem de Serviço aberta com sucesso!");
        }

        private void ListarOrdensServico()
        {
            Console.WriteLine("=== Lista de Ordens de Serviço ===");
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string sql = "SELECT * FROM Ordem_Servico";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine($"ID OS: {reader["Id_OS"]}, Cliente: {reader["Id_Cliente"]}, Veículo: {reader["Id_Veiculo"]}, Funcionário: {reader["Id_Funcionario"]}, Data Abertura: {reader["Data_Abertura"]}, Data Fechamento: {reader["Data_Fechamento"]}, Status: {reader["Status_OS"]}, Valor: {reader["Valor_Total"]}");
                    }
                }
            }
        }

        private void AtualizarOrdemServico()
        {
            Console.WriteLine("=== Atualizar Ordem de Serviço ===");
            Console.Write("Informe o ID da OS: ");
            if (!int.TryParse(Console.ReadLine(), out int id)) return;

            Console.Write("Novo Status: "); string status = Console.ReadLine() ?? "";
            Console.Write("Novo Valor Total: "); decimal valor = decimal.Parse(Console.ReadLine() ?? "0");

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string sql = @"UPDATE Ordem_Servico 
                               SET Status_OS=@Status, Valor_Total=@Valor
                               WHERE Id_OS=@Id";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Status", status);
                    cmd.Parameters.AddWithValue("@Valor", valor);
                    cmd.Parameters.AddWithValue("@Id", id);
                    int rows = cmd.ExecuteNonQuery();
                    Console.WriteLine(rows > 0 ? "Ordem de Serviço atualizada com sucesso!" : "OS não encontrada.");
                }
            }
        }

        private void FecharOrdemServico()
        {
            Console.WriteLine("=== Fechar Ordem de Serviço ===");
            Console.Write("Informe o ID da OS: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("ID inválido.");
                return;
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string sql = @"UPDATE Ordem_Servico 
                               SET Status_OS = 'Concluída', Data_Fechamento = GETDATE()
                               WHERE Id_OS = @Id";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    int rows = cmd.ExecuteNonQuery();
                    Console.WriteLine(rows > 0 ? "Ordem de Serviço fechada com sucesso!" : "OS não encontrada.");
                }
            }
        }
    }
}