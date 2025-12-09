CREATE DATABASE DB_Mecanica;
GO

USE DB_Mecanica;
GO

-- Cadastro de Clientes
CREATE TABLE Cadastro_Clientes (
    Id_Cliente INT PRIMARY KEY IDENTITY(1,1),
    Nome_Cliente VARCHAR(100) NOT NULL,
    CPF_Cliente CHAR(11) NOT NULL UNIQUE,
    RG_Cliente VARCHAR(15),
    Telefone_Cliente VARCHAR(20),
    Email_Cliente VARCHAR(100),
    CEP_Cliente CHAR(8)
);

-- Cadastro de Veículos
CREATE TABLE Cadastro_Veiculos (
    Id_Veiculo INT PRIMARY KEY IDENTITY(1,1),
    Marca_Veiculo VARCHAR(50) NOT NULL,
    Nome_Veiculo VARCHAR(100) NOT NULL,
    Cor_Veiculo VARCHAR(30),
    Modelo_Veiculo VARCHAR(50),
    Motor_Veiculo VARCHAR(50),
    Placa_Veiculo CHAR(7) UNIQUE,
    Chassi_Veiculo CHAR(17) UNIQUE,
    Ano_Veiculo INT,
    Km_Veiculo INT
);

-- Estoque de Produtos
CREATE TABLE Estoque_Produtos (
    Id_Produto INT PRIMARY KEY IDENTITY(1,1),
    Nome_Produto VARCHAR(100) NOT NULL,
    QTD_Produto INT NOT NULL,
    Preco_Produto DECIMAL(10,2) NOT NULL,
    Marca_Produto VARCHAR(50),
    Categoria_Produto VARCHAR(50)
);

-- Fornecedores
CREATE TABLE Fornecedores (
    Id_Fornecedor INT PRIMARY KEY IDENTITY(1,1),
    Nome_Fornecedor VARCHAR(100) NOT NULL,
    CEP_Fornecedor CHAR(8),
    Categoria_Fornecedor VARCHAR(50),
    Estado_Fornecedor CHAR(2),
    Cidade_Fornecedor VARCHAR(50),
    Email_Fornecedor VARCHAR(100),
    Telefone_Fornecedor VARCHAR(20)
);

-- Tabela de Funcionários
CREATE TABLE Funcionarios (
    Id_Funcionario INT PRIMARY KEY IDENTITY(1,1),
    Nome_Funcionario VARCHAR(100) NOT NULL,
    Cargo_Funcionario VARCHAR(50) NOT NULL, -- Mecânico, Atendente, etc.
    Telefone_Funcionario VARCHAR(20),
    Email_Funcionario VARCHAR(100)
);

-- Tabela de Serviços
CREATE TABLE Servicos (
    Id_Servico INT PRIMARY KEY IDENTITY(1,1),
    Nome_Servico VARCHAR(100) NOT NULL, -- Ex: Troca de óleo
    Descricao_Servico VARCHAR(255),
    Preco_Servico DECIMAL(10,2) NOT NULL
);

-- Tabela de Ordem de Serviço
CREATE TABLE Ordem_Servico (
    Id_OS INT PRIMARY KEY IDENTITY(1,1),
    Id_Cliente INT NOT NULL FOREIGN KEY REFERENCES Cadastro_Clientes(Id_Cliente),
    Id_Veiculo INT NOT NULL FOREIGN KEY REFERENCES Cadastro_Veiculos(Id_Veiculo),
    Id_Funcionario INT FOREIGN KEY REFERENCES Funcionarios(Id_Funcionario),
    Data_Abertura DATE NOT NULL,
    Data_Fechamento DATE,
    Status_OS VARCHAR(20) NOT NULL, -- Aberta, Em andamento, Concluída
    Valor_Total DECIMAL(10,2)
);

-- Tabela de Pagamentos
CREATE TABLE Pagamentos (
    Id_Pagamento INT PRIMARY KEY IDENTITY(1,1),
    Id_OS INT NOT NULL FOREIGN KEY REFERENCES Ordem_Servico(Id_OS),
    Forma_Pagamento VARCHAR(20) NOT NULL, -- Dinheiro, Cartão, Pix
    Data_Pagamento DATE NOT NULL,
    Valor_Pago DECIMAL(10,2) NOT NULL
);

-- Tabela de relação entre Ordem de Serviço e Serviços (N:N)
CREATE TABLE Ordem_Servico_Servicos (
    Id_OS INT NOT NULL FOREIGN KEY REFERENCES Ordem_Servico(Id_OS),
    Id_Servico INT NOT NULL FOREIGN KEY REFERENCES Servicos(Id_Servico),
    Quantidade INT DEFAULT 1,
    PRIMARY KEY (Id_OS, Id_Servico)
);
