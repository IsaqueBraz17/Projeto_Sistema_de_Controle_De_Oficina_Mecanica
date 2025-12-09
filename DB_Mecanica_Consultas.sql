Use DB_Mecanica

-- Clientes
SELECT * FROM Cadastro_Clientes;
SELECT Nome_Cliente, Telefone_Cliente FROM Cadastro_Clientes;

-- Veículos
SELECT * FROM Cadastro_Veiculos;
SELECT Marca_Veiculo, Nome_Veiculo, Placa_Veiculo FROM Cadastro_Veiculos;

-- Produtos
SELECT * FROM Estoque_Produtos;
SELECT Nome_Produto, QTD_Produto, Preco_Produto FROM Estoque_Produtos;

-- Fornecedores
SELECT * FROM Fornecedores;
SELECT Nome_Fornecedor, Cidade_Fornecedor FROM Fornecedores;

-- Funcionários
SELECT * FROM Funcionarios;
SELECT Nome_Funcionario, Cargo_Funcionario FROM Funcionarios;

-- Serviços
SELECT * FROM Servicos;
SELECT Nome_Servico, Preco_Servico FROM Servicos;

-- Pagamentos
SELECT * FROM Pagamentos;
SELECT Id_OS, Forma_Pagamento, Valor_Pago FROM Pagamentos;

-- Relação OS x Serviços
SELECT * FROM Ordem_Servico_Servicos;
SELECT Id_OS, Id_Servico, Quantidade FROM Ordem_Servico_Servicos;

-- JOINs úteis
-- Clientes e seus veículos
SELECT c.Nome_Cliente, v.Marca_Veiculo, v.Nome_Veiculo, v.Placa_Veiculo
FROM Cadastro_Clientes c
INNER JOIN Cadastro_Veiculos v ON c.Id_Cliente = v.Id_Veiculo;

-- OS com cliente e funcionário
SELECT os.Id_OS, c.Nome_Cliente, v.Placa_Veiculo, f.Nome_Funcionario, os.Status_OS, os.Valor_Total
FROM Ordem_Servico os
INNER JOIN Cadastro_Clientes c ON os.Id_Cliente = c.Id_Cliente
INNER JOIN Cadastro_Veiculos v ON os.Id_Veiculo = v.Id_Veiculo
LEFT JOIN Funcionarios f ON os.Id_Funcionario = f.Id_Funcionario;


