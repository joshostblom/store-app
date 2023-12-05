-- Insert dummy data into Addresses table
INSERT INTO Addresses (Street, City, [State], Country, PostalCode)
VALUES ('123 Main St', 'City1', 'State1', 'Country1', '12345'),
       ('456 Elm St', 'City2', 'State2', 'Country2', '67890');

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
VALUES ('Lenovo IdeaPad 3 Laptop', 450.43, CAST('https://pisces.bbystatic.com/image2/BestBuy_US/images/products/6461/6461977ld.jpg' AS varbinary(max)), 1, 'SKU123', 4.5, 'New, Touchscreen, Comes with Charger and Pen, 16GB RAM', 'Lenovo Group of China', 15.0, 3.0, 6.0, 2.0),
       ('iPhone 14', 145.29, CAST('https://support.apple.com/library/APPLE/APPLECARE_ALLGEOS/SP873/iphone-14_1.png' AS varbinary(max)), 2, 'SKU456', 4.2, 'New iPhone 14 Unlocked, Comes with Charger and Screen Protector, Comes with 5 Year Warranty', 'Apple', 4.5, 2.5, 5.5, 1.8),
       ('PlayStation 5', 69.00, CAST('https://m.media-amazon.com/images/I/41sN+-1hRsL._AC_UF894,1000_QL80_.jpg' AS varbinary(max)), 1, 'SKU567', 5, 'Comes with Controller and Charging Cable, 2TB of Storage', 'Sony Interactive Entertainment LLC', 4.0, 3.5, 7.0, 6.0),
	   ('Set of 2 Queen Size Pillow Case', 10.00, CAST('https://m.media-amazon.com/images/I/61J9BrgLZNL.__AC_SX300_SY300_QL70_FMwebp_.jpg' AS varbinary(max)), 1, 'SKU678', 4.3, 'Set of 2 Grey Pillow Cases for Queen Size/Style Pillows', 'Bedsure', 12.0, 3.5, 25.0, 1.0),
	   ('Art of Cardistry Playing Cards', 9.99, CAST('https://m.media-amazon.com/images/I/71jsAU53kYL._AC_SL1500_.jpg' AS varbinary(max)), 1, 'SKU789', 5, '54 Playing Card Deck with an artistic design', 'USPCC', 3.0, 2.0, 0.5, 0.5),
	   ('Gundam Barbatos', 49.99, CAST('https://m.media-amazon.com/images/I/71H1t0xq+FL._AC_SL1200_.jpg' AS varbinary(max)), 2, 'SKU778', 5, '1/100th Plastic Model Kit from the series Iron Blooded Orphans', 'Bandai', 3.0, 3.0, 7.0, 0.5),
	   ('Silicone Kitchen Utensil Set', 23.45, CAST('https://m.media-amazon.com/images/I/71zMNUBd5kL._AC_SL1500_.jpg' AS varbinary(max)), 1, 'SKU777', 3.5, '14 Piece Silicone Kitchen Utensil Set', 'Oannao', 14.0, 14.0, 2.0, 1),
	   ('Nanoblock Snorlax', 11.45, CAST('https://m.media-amazon.com/images/I/61rsOTqMrTL._AC_SL1500_.jpg' AS varbinary(max)), 1, 'SKU453', 4.2, 'Nanoblock Building Kit of the Pokemon Snorlax', 'Nanoblock', 2.0, 1.0, 2.0, 1),
	   ('Babish Kitchen Knife', 23.98, CAST('https://m.media-amazon.com/images/I/51+hEeLZXFS._AC_SL1500_.jpg' AS varbinary(max)), 2, 'SKU513', 4.1, 'High-Carnon German Steel Kitchen Knife', 'Babish', 7.5, 2.0, 0.5, 1.5),
	   ('Bulk Scotch Tap', 6.98, CAST('https://m.media-amazon.com/images/I/61MBvz3nbLL._AC_SL1500_.jpg' AS varbinary(max)), 2, 'SKU514', 3.0, '6-Pack of Transparent Scotch Tape', 'OWLKELA', 2.5, 2.0, 1.0, 0.5),
	   ('Wall Hooks', 21.99, CAST('https://m.media-amazon.com/images/I/61hJ3mnCfbS._AC_SL1500_.jpg' AS varbinary(max)), 1, 'SKU515', 4.7, '6-Pack of Cute Animal Adhesive Wall Hooks', 'RELABTABY', 2.0, 2.0, 2.0, 0.5),
	   ('Minecraft Foam Diamond Sword', 8.99, CAST('https://m.media-amazon.com/images/I/81oMNC8PbIL._AC_SL1500_.jpg' AS varbinary(max)), 1, 'SKU516', 4.3, 'Foam Diamond Sword For Cosplay and Kids', 'Disguise', 5.0, 12.0, 2.0, 0.5);

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