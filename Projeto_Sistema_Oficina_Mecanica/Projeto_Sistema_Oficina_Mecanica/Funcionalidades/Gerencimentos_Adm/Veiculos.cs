// Importa namespaces necessários
using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;     // Biblioteca para conectar e executar comandos no SQL Server

namespace Projeto_Sistema_Oficina_Mecanica.Funcionalidades
{
    internal class Veiculos
    {
        // String de conexão com o banco de dados SQL Server
        private string connectionString = "Server=NOTEBOOK-ISAQUE;Database=DB_Mecanica;Trusted_Connection=True;TrustServerCertificate=True;";

        // Método principal para gerenciar veículos (menu interativo)
        public void GerenciarVeiculos()
        {
            while (true) // Loop infinito até o usuário escolher sair
            {
                // Exibe opções do menu
                Console.WriteLine();
                Console.WriteLine("Gerenciamento de Veículos:");
                Console.WriteLine("1 - Adicionar Veículo");
                Console.WriteLine("2 - Listar Veículos");
                Console.WriteLine("3 - Atualizar Veículo");
                Console.WriteLine("4 - Remover Veículo");
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
                    case 1: AdicionarVeiculo(); break;
                    case 2: ListarVeiculos(); break;
                    case 3: AtualizarVeiculo(); break;
                    case 4: RemoverVeiculo(); break;
                    case 0: return;
                    default: Console.WriteLine("Opção inválida."); break;
                }
            }
        }

        // Método para adicionar um novo veículo
        private void AdicionarVeiculo()
        {
            Console.WriteLine("=== Adicionar Veículo ===");
            Console.Write("Marca: "); string marca = Console.ReadLine() ?? "";
            Console.Write("Nome: "); string nome = Console.ReadLine() ?? "";
            Console.Write("Cor: "); string cor = Console.ReadLine() ?? "";
            Console.Write("Modelo: "); string modelo = Console.ReadLine() ?? "";
            Console.Write("Motor: "); string motor = Console.ReadLine() ?? "";
            Console.Write("Placa: "); string placa = Console.ReadLine() ?? "";
            Console.Write("Chassi: "); string chassi = Console.ReadLine() ?? "";
            Console.Write("Ano: "); int ano = int.Parse(Console.ReadLine() ?? "0");
            Console.Write("Km: "); int km = int.Parse(Console.ReadLine() ?? "0");

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string sql = @"INSERT INTO Cadastro_Veiculos 
                               (Marca_Veiculo, Nome_Veiculo, Cor_Veiculo, Modelo_Veiculo, Motor_Veiculo, Placa_Veiculo, Chassi_Veiculo, Ano_Veiculo, Km_Veiculo)
                               VALUES (@Marca, @Nome, @Cor, @Modelo, @Motor, @Placa, @Chassi, @Ano, @Km)";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Marca", marca);
                    cmd.Parameters.AddWithValue("@Nome", nome);
                    cmd.Parameters.AddWithValue("@Cor", cor);
                    cmd.Parameters.AddWithValue("@Modelo", modelo);
                    cmd.Parameters.AddWithValue("@Motor", motor);
                    cmd.Parameters.AddWithValue("@Placa", placa);
                    cmd.Parameters.AddWithValue("@Chassi", chassi);
                    cmd.Parameters.AddWithValue("@Ano", ano);
                    cmd.Parameters.AddWithValue("@Km", km);
                    cmd.ExecuteNonQuery();
                }
            }
            Console.WriteLine("Veículo adicionado com sucesso!");
        }

        // Método para listar todos os veículos
        private void ListarVeiculos()
        {
            Console.WriteLine("=== Lista de Veículos ===");
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string sql = "SELECT * FROM Cadastro_Veiculos";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine($"ID: {reader["Id_Veiculo"]}, Marca: {reader["Marca_Veiculo"]}, Nome: {reader["Nome_Veiculo"]}, Cor: {reader["Cor_Veiculo"]}, Modelo: {reader["Modelo_Veiculo"]}, Motor: {reader["Motor_Veiculo"]}, Placa: {reader["Placa_Veiculo"]}, Chassi: {reader["Chassi_Veiculo"]}, Ano: {reader["Ano_Veiculo"]}, Km: {reader["Km_Veiculo"]}");
                    }
                }
            }
        }

        // Método para atualizar dados de um veículo
        private void AtualizarVeiculo()
        {
            Console.WriteLine("=== Atualizar Veículo ===");
            Console.Write("Informe o ID do veículo: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("ID inválido.");
                return;
            }

            Console.Write("Novo Nome: "); string nome = Console.ReadLine() ?? "";
            Console.Write("Nova Cor: "); string cor = Console.ReadLine() ?? "";
            Console.Write("Novo Km: "); int km = int.Parse(Console.ReadLine() ?? "0");

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string sql = @"UPDATE Cadastro_Veiculos 
                               SET Nome_Veiculo=@Nome, Cor_Veiculo=@Cor, Km_Veiculo=@Km
                               WHERE Id_Veiculo=@Id";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Nome", nome);
                    cmd.Parameters.AddWithValue("@Cor", cor);
                    cmd.Parameters.AddWithValue("@Km", km);
                    cmd.Parameters.AddWithValue("@Id", id);
                    int rows = cmd.ExecuteNonQuery();
                    Console.WriteLine(rows > 0 ? "Veículo atualizado com sucesso!" : "Veículo não encontrado.");
                }
            }
        }

        // Método para remover um veículo
        private void RemoverVeiculo()
        {
            Console.WriteLine("=== Remover Veículo ===");
            Console.Write("Informe o ID do veículo: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("ID inválido.");
                return;
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string sql = "DELETE FROM Cadastro_Veiculos WHERE Id_Veiculo=@Id";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    int rows = cmd.ExecuteNonQuery();
                    Console.WriteLine(rows > 0 ? "Veículo removido com sucesso!" : "Veículo não encontrado.");
                }
            }
        }

        // Classe interna que representa um veículo (modelo de dados)
        private class Veiculo
        {
            public int Id { get; set; }              // ID do veículo
            public string? Marca { get; set; }       // Marca do veículo
            public string? Nome { get; set; }        // Nome do veículo
            public string? Cor { get; set; }         // Cor do veículo
            public string? Modelo { get; set; }      // Modelo do veículo
            public string? Motor { get; set; }       // Motor do veículo
            public string? Placa { get; set; }       // Placa do veículo
            public string? Chassi { get; set; }      // Chassi do veículo
            public int Ano { get; set; }             // Ano do veículo
            public int Km { get; set; }              // Quilometragem do veículo
        }
    }
}