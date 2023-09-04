USE CORE_BANKING;
CREATE TABLE Customers (
  customerId VARCHAR(255) PRIMARY KEY,
  name VARCHAR(255) NOT NULL,
  email VARCHAR(255) NOT NULL,
  password VARCHAR(255) NOT NULL
);

CREATE TABLE BankAccounts (
  accountId INT PRIMARY KEY,
  customerId VARCHAR(255) NOT NULL,
  balance DECIMAL(10,2) NOT NULL,
  FOREIGN KEY (customerId) REFERENCES Customers (customerId)
);

CREATE TABLE FinancialAssets (
  assetId INT PRIMARY KEY,
  name VARCHAR(50) NOT NULL,
  price DECIMAL(10,2) NOT NULL
);

CREATE TABLE FinancialTransactions (
  transactionId INT PRIMARY KEY,
  accountId INT NOT NULL,
  type VARCHAR(50) NOT NULL,
  assetId INT NOT NULL,
  quantity INT NOT NULL,
  totalValue DECIMAL(10,2),
  date Datetime NOT NULL,
  FOREIGN KEY (accountId) REFERENCES BankAccounts (accountId),
  FOREIGN KEY (assetId) REFERENCES FinancialAssets (assetId)
);