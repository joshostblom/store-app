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
VALUES ('Electronics'),
       ('Home Appliances'),
       ('Clothing'),
       ('Toys'),
       ('Furniture'),
       ('Kitchenware'),
       ('Sports & Outdoors');

-- Insert dummy data into Payment table
INSERT INTO Payment (CardLastName, CardFirstName, CardNumber, CVV, ExpirationDate)
VALUES ('Doe', 'John', '1234567890123456', 123, '2024-12-31'),
       ('Smith', 'Jane', '9876543210987654', 456, '2025-06-30');

-- Insert dummy data into Sale table
INSERT INTO Sale (StartDate, EndDate, PercentOff)
VALUES ('2023-01-01 00:00:00', '2024-02-28 23:59:59', 10.00),
       ('2023-03-01 00:00:00', '2024-03-31 23:59:59', 15.00);

-- Insert dummy data into Product table
INSERT INTO Product (ProductName, Price, ImageURL, SaleID, Sku, Rating, Descript, ManufacturerInformation, ProdHeight, ProdWidth, ProdLength, ProdWeight)
VALUES ('Lenovo IdeaPad 3 Laptop', 450.43, CAST('https://pisces.bbystatic.com/image2/BestBuy_US/images/products/6461/6461977ld.jpg' AS varbinary(max)), (SELECT SaleID from Sale where PercentOff = 10.00), 'SKU123', 4.5, 'New, Touchscreen, Comes with Charger and Pen, 16GB RAM', 'Lenovo Group of China', 15.0, 3.0, 6.0, 2.0),
       ('iPhone 14', 145.29, CAST('https://support.apple.com/library/APPLE/APPLECARE_ALLGEOS/SP873/iphone-14_1.png' AS varbinary(max)), (SELECT SaleID from Sale where PercentOff = 10.00), 'SKU456', 4.2, 'New iPhone 14 Unlocked, Comes with Charger and Screen Protector, Comes with 5 Year Warranty', 'Apple', 4.5, 2.5, 5.5, 1.8),
       ('PlayStation 5', 69.00, CAST('https://m.media-amazon.com/images/I/41sN+-1hRsL._AC_UF894,1000_QL80_.jpg' AS varbinary(max)), (SELECT SaleID from Sale where PercentOff = 10.00), 'SKU567', 5, 'Comes with Controller and Charging Cable, 2TB of Storage', 'Sony Interactive Entertainment LLC', 4.0, 3.5, 7.0, 6.0),
       ('Samsung 4K Smart TV', 799.99, CAST('https://images.samsung.com/is/image/samsung/latin-en-uhdtv-nu7100-un50nu7100pxpa-frontblack-121432508?$1300_1038_PNG$' AS varbinary(max)), (SELECT SaleID from Sale where PercentOff = 10.00), 'SKU789', 4.8, '50-inch 4K UHD Smart TV, HDR, Crystal Display', 'Samsung Electronics', 27.0, 4.0, 44.0, 32.0),
       ('Canon EOS Rebel T7i DSLR Camera', 649.95, CAST('https://www.canon.ca/dam/products/BUSINESS-UNIT/ITCG/Cameras/EOS-Cameras/DSLR/EOS-Rebel-T7i/Canon_EOS-Rebel-T7i-_Front_580x580.png' AS varbinary(max)), NULL, 'SKU101', 4.7, '24.2 Megapixel CMOS (APS-C) sensor, Dual Pixel CMOS AF, Wi-Fi, NFC', 'Canon Inc.', 3.8, 5.2, 4.0, 1.2),
       ('KitchenAid Stand Mixer', 349.99, CAST('https://cb.scene7.com/is/image/Crate/KitchenAidArtStMxACAVSSS21_VND/$web_pdp_main_carousel_med$/210323153457/kitchenaid-artisan-series-5-quart-tilt-head-almond-cream-stand-mixer.jpg' AS varbinary(max)), (SELECT SaleID from Sale where PercentOff = 15.00), 'SKU112', 4.9, '5-Quart Tilt-Head Stand Mixer, Stainless Steel Bowl, 10-Speed Settings', 'Whirlpool Corporation', 14.1, 8.7, 14.1, 25.0),
       ('Apple AirPods Pro', 249.00, CAST('https://store.storeimages.cdn-apple.com/4982/as-images.apple.com/is/refurb-airpods-2022?wid=572&hei=572&fmt=jpeg&qlt=95&.v=1673992683197' AS varbinary(max)), (SELECT SaleID from Sale where PercentOff = 10.00), 'SKU202', 4.9, 'Active Noise Cancellation, Sweat and Water Resistant, Wireless Charging Case', 'Apple', 1.2, 0.9, 2.4, 0.38),
       ('Dyson V11 Torque Drive Cordless Vacuum', 699.99, CAST('https://crdms.images.consumerreports.org/f_auto,w_600/prod/products/cr/models/398145-stick-vacuums-greater-than-6-lbs-dyson-v11-torque-drive-10005020.png' AS varbinary(max)), NULL, 'SKU315', 4.7, 'Powerful Suction, LCD Screen, Whole Machine Filtration', 'Dyson Ltd.', 10.3, 9.8, 50.6, 6.7),
       ('Adidas Ultraboost Running Shoes', 180.00, CAST('https://i.ebayimg.com/images/g/T10AAOSwevBjS-Mo/s-l500.png' AS varbinary(max)), NULL, 'SKU428', 4.6, 'Responsive Running Shoes with Boost Cushioning', 'Adidas', 4.0, 7.5, 12.0, 1.0),
       ('LEGO Star Wars Millennium Falcon', 159.99, CAST('https://shop-newyork.legoland.com/cdn/shop/products/75257_1024x1024@2x.jpg?v=1614449862' AS varbinary(max)),NULL, 'SKU537', 4.8, '1,351 Pieces, Buildable Millennium Falcon Set', 'The LEGO Group', 8.0, 33.2, 22.8, 4.0),
       ('Instant Pot Duo 7-in-1 Electric Pressure Cooker', 89.95, CAST('https://i.ebayimg.com/images/g/EEIAAOSw5wFifnSj/s-l500.png' AS varbinary(max)), NULL, 'SKU642', 4.7, '7-in-1 Multi-Use Programmable Cooker, 6 Quart', 'Double Insight Inc.', 12.6, 12.2, 13.1, 11.8),
       ('Nike Golf Mens Dri-FIT Polo Shirt', 45.00, CAST('https://storagemedia.corporategear.com/storagemedia/1/mastercatalog/attributeimages/ci4470-448.jpg' AS varbinary(max)), NULL, 'SKU753', 4.5, 'Moisture-Wicking, Breathable, Dri-FIT Technology', 'Nike Inc.', NULL, NULL, NULL, NULL),
       ('Kindle Paperwhite E-reader', 129.99, CAST('https://assets-global.website-files.com/60feafdac3e33ed6180205f3/618fea956c2e5705bca9c497_3eafacac.png' AS varbinary(max)), (SELECT SaleID from Sale where PercentOff = 10.00), 'SKU864', 4.5, '6-inch High-Resolution Display, Waterproof, 8GB', 'Amazon', 6.6, 4.6, 0.3, 6.4),
       ('Breville Smart Oven', 299.95, CAST('https://crdms.images.consumerreports.org/f_auto,w_600/prod/products/cr/models/392664-toasterovens-breville-smartovenairconvectionbov900bssusc.png' AS varbinary(max)), (SELECT SaleID from Sale where PercentOff = 15.00), 'SKU902', 4.9, '1800W Convection Toaster Oven, Element IQ Technology', 'Breville', 11.0, 18.5, 16.2, 20.0),
       ('Nike Air Zoom Pegasus 38 Running Shoes', 120.00, CAST('https://static.nike.com/a/images/t_PDP_1728_v1/f_auto,q_auto:eco/968b68b3-826d-47b4-a714-e5594ace613d/air-zoom-pegasus-38-mens-road-running-shoes-lq7PZZ.png' AS varbinary(max)), NULL, 'SKU903', 4.7, 'Responsive Running Shoes with Zoom Air Cushioning', 'Nike Inc.', 4.3, 7.8, 11.7, 0.65),
       ('LEGO Technic Bugatti Chiron', 349.99, CAST('https://i.ebayimg.com/images/g/Mz0AAOSw5m5kUEgf/s-l1600.jpg' AS varbinary(max)), NULL, 'SKU904', 4.8, '3,599 Pieces, Buildable Bugatti Chiron Set', 'The LEGO Group', 5.8, 22.9, 14.6, 9.8),
       ('Sony WH-1000XM4 Wireless Noise-Canceling Headphones', 348.00, CAST('https://i.ebayimg.com/images/g/TiAAAOSwnIRkgLG0/s-l1600.png' AS varbinary(max)), (SELECT SaleID from Sale where PercentOff = 10.00), 'SKU905', 4.6, 'Industry-Leading Noise Cancellation, Touch Sensor Controls', 'Sony Corporation', 9.9, 3.0, 7.3, 0.83),
       ('Cuisinart 14-Cup Food Processor', 229.95, CAST('https://crdms.images.consumerreports.org/f_auto,w_600/prod/products/cr/models/387449-foodprocessors-cuisinart-custom14dfp14bcny.png' AS varbinary(max)), (SELECT SaleID from Sale where PercentOff = 15.00), 'SKU906', 4.7, 'Stainless Steel Blades, 14-Cup Capacity, Dough Blade', 'Cuisinart', 17.0, 10.0, 7.5, 18.0),
       ('GoPro HERO9 Black Action Camera', 449.99, CAST('https://coolstuf.com.pg/wp-content/uploads/2020/11/GOPRO-HERO9-main-1-2048x1540.jpg' AS varbinary(max)), (SELECT SaleID from Sale where PercentOff = 10.00), 'SKU907', 4.5, '5K Video, 20MP Photos, HyperSmooth 3.0', 'GoPro, Inc.', 2.3, 1.8, 1.5, 5.6),
       ('Under Armour Mens Tech 2.0 Short Sleeve T-Shirt', 25.00, CAST('https://m.media-amazon.com/images/W/MEDIAX_792452-T1/images/I/518WCqhKbLL._AC_SX679_.jpg' AS varbinary(max)), NULL, 'SKU908', 4, 'Moisture-Wicking, Quick-Drying, Anti-Odor Technology', 'Under Armour Inc.', NULL, NULL, NULL, NULL),
       ('Hamilton Beach Coffee Maker', 39.99, CAST('https://crdms.images.consumerreports.org/f_auto,w_600/prod/products/cr/models/400768-drip-coffee-makers-with-carafe-hamilton-beach-programmable-alexa-smart-49350-10011102.png' AS varbinary(max)), (SELECT SaleID from Sale where PercentOff = 15.00), 'SKU909', 4.3, '12-Cup Programmable BrewStation, Dispensing Coffee Machine', 'Hamilton Beach Brands, Inc.', 13.9, 8.2, 14.4, 6.0),
       ('Fitbit Charge 4 Fitness and Activity Tracker', 149.95, CAST('https://helios-i.mashable.com/imagery/reviews/06l8zHGjxPvYrSnoDGq9W95/hero-image.fill.size_1248x702.v1641937211.png' AS varbinary(max)), (SELECT SaleID from Sale where PercentOff = 10.00), 'SKU910', 4.4, 'Built-In GPS, Heart Rate Monitoring, Sleep Tracking', 'Fitbit, Inc.', 1.4, 0.9, 0.5, 0.3);

-- Insert dummy data into ProductToCategory table
INSERT INTO ProductToCategory (ProductID, CategoryID)
VALUES
  ((SELECT ProductID FROM Product WHERE ProductName = 'Lenovo IdeaPad 3 Laptop'), (SELECT CategoryID FROM Category WHERE Name = 'Electronics')),
  ((SELECT ProductID FROM Product WHERE ProductName = 'iPhone 14'), (SELECT CategoryID FROM Category WHERE Name = 'Electronics')),
  ((SELECT ProductID FROM Product WHERE ProductName = 'PlayStation 5'), (SELECT CategoryID FROM Category WHERE Name = 'Electronics')),
  ((SELECT ProductID FROM Product WHERE ProductName = 'Samsung 4K Smart TV'), (SELECT CategoryID FROM Category WHERE Name = 'Electronics')),
  ((SELECT ProductID FROM Product WHERE ProductName = 'Canon EOS Rebel T7i DSLR Camera'), (SELECT CategoryID FROM Category WHERE Name = 'Electronics')),
  ((SELECT ProductID FROM Product WHERE ProductName = 'KitchenAid Stand Mixer'), (SELECT CategoryID FROM Category WHERE Name = 'Kitchenware')),
  ((SELECT ProductID FROM Product WHERE ProductName = 'Apple AirPods Pro'), (SELECT CategoryID FROM Category WHERE Name = 'Electronics')),
  ((SELECT ProductID FROM Product WHERE ProductName = 'Dyson V11 Torque Drive Cordless Vacuum'), (SELECT CategoryID FROM Category WHERE Name = 'Home Appliances')),
  ((SELECT ProductID FROM Product WHERE ProductName = 'Adidas Ultraboost Running Shoes'), (SELECT CategoryID FROM Category WHERE Name = 'Clothing')),
  ((SELECT ProductID FROM Product WHERE ProductName = 'LEGO Star Wars Millennium Falcon'), (SELECT CategoryID FROM Category WHERE Name = 'Toys')),
  ((SELECT ProductID FROM Product WHERE ProductName = 'Instant Pot Duo 7-in-1 Electric Pressure Cooker'), (SELECT CategoryID FROM Category WHERE Name = 'Kitchenware')),
  ((SELECT ProductID FROM Product WHERE ProductName = 'Nike Golf Mens Dri-FIT Polo Shirt'), (SELECT CategoryID FROM Category WHERE Name = 'Clothing')),
  ((SELECT ProductID FROM Product WHERE ProductName = 'Kindle Paperwhite E-reader'), (SELECT CategoryID FROM Category WHERE Name = 'Electronics')),
  ((SELECT ProductID FROM Product WHERE ProductName = 'Breville Smart Oven'), (SELECT CategoryID FROM Category WHERE Name = 'Electronics')),
  ((SELECT ProductID FROM Product WHERE ProductName = 'Nike Air Zoom Pegasus 38 Running Shoes'), (SELECT CategoryID FROM Category WHERE Name = 'Clothing')),
  ((SELECT ProductID FROM Product WHERE ProductName = 'LEGO Technic Bugatti Chiron'), (SELECT CategoryID FROM Category WHERE Name = 'Toys')),
  ((SELECT ProductID FROM Product WHERE ProductName = 'Sony WH-1000XM4 Wireless Noise-Canceling Headphones'), (SELECT CategoryID FROM Category WHERE Name = 'Electronics')),
  ((SELECT ProductID FROM Product WHERE ProductName = 'Cuisinart 14-Cup Food Processor'), (SELECT CategoryID FROM Category WHERE Name = 'Kitchenware')),
  ((SELECT ProductID FROM Product WHERE ProductName = 'GoPro HERO9 Black Action Camera'), (SELECT CategoryID FROM Category WHERE Name = 'Electronics')),
  ((SELECT ProductID FROM Product WHERE ProductName = 'Under Armour Mens Tech 2.0 Short Sleeve T-Shirt'), (SELECT CategoryID FROM Category WHERE Name = 'Clothing')),
  ((SELECT ProductID FROM Product WHERE ProductName = 'Hamilton Beach Coffee Maker'), (SELECT CategoryID FROM Category WHERE Name = 'Electronics')),
  ((SELECT ProductID FROM Product WHERE ProductName = 'Fitbit Charge 4 Fitness and Activity Tracker'), (SELECT CategoryID FROM Category WHERE Name = 'Electronics'));
