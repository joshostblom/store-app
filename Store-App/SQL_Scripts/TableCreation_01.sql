DROP TABLE IF EXISTS Person;
DROP TABLE IF EXISTS ProductToCategory;
DROP TABLE IF EXISTS ProductToCart;
DROP TABLE IF EXISTS Product;
DROP TABLE IF EXISTS Sale;
DROP TABLE IF EXISTS Payment;
DROP TABLE IF EXISTS Category;
DROP TABLE IF EXISTS Cart;
DROP TABLE IF EXISTS Addresses;

CREATE TABLE Addresses (
  AddressID int NOT NULL IDENTITY(1,1) PRIMARY KEY,
  Street varchar(255) NOT NULL,
  City varchar(255) NOT NULL,
  Country varchar(255) NOT NULL,
  PostalCode varchar(255) NOT NULL
);

CREATE TABLE Cart (
  CartID int NOT NULL IDENTITY(1,1) PRIMARY KEY,
  Total FLOAT NOT NULL
);

CREATE TABLE Category (
  CategoryID int NOT NULL IDENTITY(1,1) PRIMARY KEY,
  Name varchar(255) NOT NULL
);

CREATE TABLE Payment (
	PaymentID int NOT NULL IDENTITY(1,1) PRIMARY KEY,
	CardLastName varchar(255) NOT NULL,
	CardFirstName varchar(255) NOT NULL,
	CardNumber varchar(255) NOT NULL UNIQUE, --store as encrypted string
	CVV int NOT NULL,
	ExpirationDate DATE NOT NULL
);

CREATE TABLE Sale (
	SaleID int NOT NULL IDENTITY(1,1) PRIMARY KEY,
	StartDate DATETIME NOT NULL,
	EndDate DATETIME NOT NULL,
	PercentOff DECIMAL(5, 2) NOT NULL CHECK (PercentOff >= 0)
);

CREATE TABLE Product (
	ProductID int NOT NULL IDENTITY(1,1) PRIMARY KEY,
	ProductName varchar(255) NOT NULL,
	Price FLOAT,
	ImageURL VARBINARY(MAX), 
	SaleID int,
	Sku varchar(255) NOT NULL UNIQUE,
	Rating FLOAT NOT NULL,
	Descript varchar(255),
	ManufacturerInformation varchar(255),
	ProdHeight FLOAT,
	ProdWidth FLOAT,
	ProdLength FLOAT,
	ProdWeight FLOAT
	--FOREIGN KEY (SaleID) REFERENCES Sale(SaleID)
);

CREATE TABLE ProductToCart (
  ProdToCartID int NOT NULL IDENTITY(1,1) PRIMARY KEY,
  CartID int, 
  ProductID int,
  FOREIGN KEY (CartID) REFERENCES Cart(CartID),
  FOREIGN KEY (ProductID) REFERENCES Product(ProductID)
); 

CREATE TABLE ProductToCategory (
  ProdToCatID int NOT NULL IDENTITY(1,1) PRIMARY KEY,
  CategoryID int, 
  ProductID int,
  FOREIGN KEY (CategoryID) REFERENCES Category(CategoryID),
  FOREIGN KEY (ProductID) REFERENCES Product(ProductID)
);

CREATE TABLE Person (
    PersonID int NOT NULL IDENTITY(1,1) PRIMARY KEY,
    LastName varchar(255) NOT NULL,
    FirstName varchar(255) NOT NULL,
	Email varchar (255) NOT NULL UNIQUE,
    Password varchar(255) NOT NULL,
    AddressID int,
	PaymentID int,
	CartID int,
	FOREIGN KEY (AddressID) REFERENCES Addresses(AddressID),
	FOREIGN KEY (PaymentID) REFERENCES Payment(PaymentID),
	FOREIGN KEY (CartID) REFERENCES Cart(CartID)
);