USE DB_Mecanica;

INSERT INTO Cadastro_Clientes (Nome_Cliente, CPF_Cliente, RG_Cliente, Telefone_Cliente, Email_Cliente, CEP_Cliente)
VALUES 
('João Silva', '12345678901', '1234567', '(11)91234-5678', 'joao.silva@email.com', '07700000'),
('Maria Oliveira', '98765432100', '7654321', '(11)99876-5432', 'maria.oliveira@email.com', '07810000');

INSERT INTO Cadastro_Veiculos (Marca_Veiculo, Nome_Veiculo, Cor_Veiculo, Modelo_Veiculo, Motor_Veiculo, Placa_Veiculo, Chassi_Veiculo, Ano_Veiculo, Km_Veiculo)
VALUES
('Ford', 'Fiesta', 'Prata', 'Hatch', '1.6 Flex', 'ABC1234', '9BWZZZ377VT004251', 2015, 85000),
('Chevrolet', 'Onix', 'Preto', 'Sedan', '1.0 Turbo', 'XYZ5678', '8ABZZZ123CD456789', 2020, 32000);

INSERT INTO Estoque_Produtos (Nome_Produto, QTD_Produto, Preco_Produto, Marca_Produto, Categoria_Produto)
VALUES
('Óleo 5W30', 50, 35.90, 'Shell', 'Lubrificante'),
('Filtro de Óleo', 30, 25.00, 'Fram', 'Filtros'),
('Pneu Aro 15', 20, 320.00, 'Goodyear', 'Pneus');

INSERT INTO Fornecedores (Nome_Fornecedor, CEP_Fornecedor, Categoria_Fornecedor, Estado_Fornecedor, Cidade_Fornecedor, Email_Fornecedor, Telefone_Fornecedor)
VALUES
('Auto Peças SP', '01000000', 'Peças Automotivas', 'SP', 'São Paulo', 'contato@autopecassp.com', '(11)4000-1234'),
('Lubrificantes Brasil', '20000000', 'Óleos e Lubrificantes', 'RJ', 'Rio de Janeiro', 'vendas@lubribrasil.com', '(21)3000-5678');

INSERT INTO Funcionarios (Nome_Funcionario, Cargo_Funcionario, Telefone_Funcionario, Email_Funcionario)
VALUES
('Pedro Santos', 'Mecânico', '(11)95555-1111', 'pedro.santos@mecanica.com'),
('Ana Costa', 'Atendente', '(11)96666-2222', 'ana.costa@mecanica.com');

INSERT INTO Servicos (Nome_Servico, Descricao_Servico, Preco_Servico)
VALUES
('Troca de Óleo', 'Substituição do óleo do motor e filtro', 120.00),
('Alinhamento', 'Ajuste da geometria das rodas', 80.00),
('Balanceamento', 'Correção do equilíbrio das rodas', 70.00);