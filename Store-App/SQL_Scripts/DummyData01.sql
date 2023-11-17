-- Insert dummy data into Addresses table
INSERT INTO Addresses (Street, City, Country, PostalCode)
VALUES ('123 Main St', 'City1', 'Country1', '12345'),
       ('456 Elm St', 'City2', 'Country2', '67890');

-- Insert dummy data into Cart table
INSERT INTO Cart (Total)
VALUES (100.50),
       (75.25);

-- Insert dummy data into Category table
INSERT INTO Category (Name)
VALUES ('Category A'),
       ('Category B');

-- Insert dummy data into Payment table
INSERT INTO Payment (CardLastName, CardFirstName, CardNumber, CVV, ExpirationDate)
VALUES ('Doe', 'John', '1234567890123456', 123, '2024-12-31'),
       ('Smith', 'Jane', '9876543210987654', 456, '2025-06-30');

-- Insert dummy data into Sale table
INSERT INTO Sale (StartDate, EndDate, PercentOff)
VALUES ('2023-01-01 00:00:00', '2023-02-28 23:59:59', 10.00),
       ('2023-03-01 00:00:00', '2023-03-31 23:59:59', 15.00);

-- Insert dummy data into Product table
INSERT INTO Product (ProductName, Price, ImageURL, SaleID, Sku, Rating, Descript, ManufacturerInformation, ProdHeight, ProdWidth, ProdLength, ProdWeight)
VALUES ('Product 1', 50.00, NULL, 1, 'SKU123', 4.5, 'Description for Product 1', 'Manufacturer Info 1', 5.0, 3.0, 6.0, 2.0),
       ('Product 2', 75.00, NULL, 2, 'SKU456', 4.2, 'Description for Product 2', 'Manufacturer Info 2', 4.5, 2.5, 5.5, 1.8);

-- Insert dummy data into ProductToCart table
INSERT INTO ProductToCart (CartID, ProductID)
VALUES (1, 1),
       (1, 2),
       (2, 2);

-- Insert dummy data into ProductToCategory table
INSERT INTO ProductToCategory (CategoryID, ProductID)
VALUES (1, 1),
       (2, 2);

-- Insert dummy data into Person table
INSERT INTO Person (LastName, FirstName, Email, Password, AddressID, PaymentID, CartID)
VALUES ('Doe', 'John', 'john.doe@example.com', 'password123', 1, 1, 1),
       ('Smith', 'Jane', 'jane.smith@example.com', 'pass456', 2, 2, 2);