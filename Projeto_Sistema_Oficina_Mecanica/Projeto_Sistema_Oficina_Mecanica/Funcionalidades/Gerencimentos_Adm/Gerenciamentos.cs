using Projeto_Sistema_Oficina_Mecanica.Funcionalidades;
using Projeto_Sistema_Oficina_Mecanica.Funcionalidades.Gerenciamento;
using Projeto_Sistema_Oficina_Mecanica.Funcionalidades.Gerenciamentos.Gerenciamentos;
using System;
using System;

namespace Projeto_Sistema_Oficina_Mecanica.Funcionalidades.Gerenciamentos
{
    internal class Menu_Gerenciamento
    {
        // Método principal para abrir o menu geral
        public void MenuPrincipal()
        {
            while (true) // Loop infinito até o usuário escolher sair
            {
                Console.WriteLine();
                Console.WriteLine("=== Menu Principal ===");
                Console.WriteLine("1 - Gerenciar Clientes");
                Console.WriteLine("2 - Gerenciar Funcionários");
                Console.WriteLine("3 - Gerenciar Fornecedores");
                Console.WriteLine("4 - Gerenciar Veículos");
                Console.WriteLine("5 - Gerenciar Serviços");
                Console.WriteLine("6 - Gerenciar Ordens de Serviço");
                Console.WriteLine("7 - Gerenciar Estoque");
                Console.WriteLine("0 - Sair do Sistema");
                Console.Write("Opção: ");

                var input = Console.ReadLine();
                if (!int.TryParse(input, out int opcao))
                {
                    Console.WriteLine("Entrada inválida. Tente novamente.");
                    continue;
                }

                switch (opcao)
                {
                    case 1:
                        Clientes clientes = new Clientes();
                        clientes.GerenciarClientes();
                        break;

                    case 2:
                        Funcionarios funcionarios = new Funcionarios();
                        funcionarios.GerenciarFuncionarios();
                        break;

                    case 3:
                        Fornecedores fornecedores = new Fornecedores();
                        fornecedores.GerenciarFornecedores();
                        break;

                    case 4:
                        Veiculos veiculos = new Veiculos();
                        veiculos.GerenciarVeiculos();
                        break;

                    case 5:
                        Services servicos = new Services();
                        servicos.GerenciarServicos();
                        break;

                    case 6:
                        Services ordens = new Services();
                        ordens.GerenciarServicos(); 
                        break;

                    case 7:
                        Estoque estoque = new Estoque();
                        estoque.GerenciaEstoque();
                        break;

                    case 0:
                        Console.WriteLine("Encerrando o sistema...");
                        return;

                    default:
                        Console.WriteLine("Opção inválida. Tente novamente.");
                        break;
                }
            }
        }
    }
}