--INSERT INTO [dbo].[Category] ([Name]) VALUES ('Active Wear - Men');
--INSERT INTO [dbo].[Category] ([Name]) VALUES ('Active Wear - Women');
--INSERT INTO [dbo].[Category] ([Name]) VALUES ('Mineral Water');
--INSERT INTO [dbo].[Category] ([Name]) VALUES ('Publications');
--INSERT INTO [dbo].[Category] ([Name]) VALUES ('Supplements');


TRUNCATE TABLE [dbo].[Product];

INSERT INTO [dbo].[Product] ([CategoryId], [Name], [Sku], [Price], [IsAvailable])
VALUES (1, 'Grunge Skater Jeans', 'AWMGSJ', 68, 1);

INSERT INTO [dbo].[Product] ([CategoryId], [Name], [Sku], [Price], [IsAvailable])
VALUES (1, 'Polo Shirt', 'AWMPS', 35, 1);

INSERT INTO [dbo].[Product] ([CategoryId], [Name], [Sku], [Price], [IsAvailable])
VALUES (1, 'Skater Graphic T-Shirt', 'AWMSGT', 33, 1);

INSERT INTO [dbo].[Product] ([CategoryId], [Name], [Sku], [Price], [IsAvailable])
VALUES (1, 'Slicker Jacket', 'AWMSJ', 125, 1);

INSERT INTO [dbo].[Product] ([CategoryId], [Name], [Sku], [Price], [IsAvailable])
VALUES (1, 'Thermal Fleece Jacket', 'AWMTFJ', 60, 1);

INSERT INTO [dbo].[Product] ([CategoryId], [Name], [Sku], [Price], [IsAvailable])
VALUES (1, 'Unisex Thermal Vest', 'AWMUTV', 95, 1);

INSERT INTO [dbo].[Product] ([CategoryId], [Name], [Sku], [Price], [IsAvailable])
VALUES (1, 'V-Neck Pullover', 'AWMVNP', 65, 1);

INSERT INTO [dbo].[Product] ([CategoryId], [Name], [Sku], [Price], [IsAvailable])
VALUES (1, 'V-Neck Sweater', 'AWMVNS', 65, 1);

INSERT INTO [dbo].[Product] ([CategoryId], [Name], [Sku], [Price], [IsAvailable])
VALUES (1, 'V-Neck T-Shirt', 'AWMVNT', 17, 1);

INSERT INTO [dbo].[Product] ([CategoryId], [Name], [Sku], [Price], [IsAvailable])
VALUES (12, 'Bamboo Thermal Ski Coat', 'AWWBTSC', 99, 1);

INSERT INTO [dbo].[Product] ([CategoryId], [Name], [Sku], [Price], [IsAvailable])
VALUES (12, 'Cross-Back Training Tank', 'AWWCTT', 0, 0);

INSERT INTO [dbo].[Product] ([CategoryId], [Name], [Sku], [Price], [IsAvailable])
VALUES (12, 'Grunge Skater Jeans', 'AWWGSJ', 68, 1);

INSERT INTO [dbo].[Product] ([CategoryId], [Name], [Sku], [Price], [IsAvailable])
VALUES (12, 'Slicker Jacket', 'AWWSJ', 125, 1);

INSERT INTO [dbo].[Product] ([CategoryId], [Name], [Sku], [Price], [IsAvailable])
VALUES (12, 'Stretchy Dance Pants', 'AWWSDP', 55, 1);

INSERT INTO [dbo].[Product] ([CategoryId], [Name], [Sku], [Price], [IsAvailable])
VALUES (12, 'Ultra-Soft Tank Top', 'AWWUTT', 22, 1);

INSERT INTO [dbo].[Product] ([CategoryId], [Name], [Sku], [Price], [IsAvailable])
VALUES (12, 'Unisex Thermal Vest', 'AWWUTV', 95, 1);

INSERT INTO [dbo].[Product] ([CategoryId], [Name], [Sku], [Price], [IsAvailable])
VALUES (12, 'V-Next T-Shirt', 'AWWVNT', 17, 1);

INSERT INTO [dbo].[Product] ([CategoryId], [Name], [Sku], [Price], [IsAvailable])
VALUES (3, 'Blueberry Mineral Water', 'MWB', 2.8, 1);

INSERT INTO [dbo].[Product] ([CategoryId], [Name], [Sku], [Price], [IsAvailable])
VALUES (3, 'Lemon-Lime Mineral Water', 'MWLL', 2.8, 1);

INSERT INTO [dbo].[Product] ([CategoryId], [Name], [Sku], [Price], [IsAvailable])
VALUES (3, 'Orange Mineral Water', 'MWO', 2.8, 1);

INSERT INTO [dbo].[Product] ([CategoryId], [Name], [Sku], [Price], [IsAvailable])
VALUES (3, 'Peach Mineral Water', 'MWP', 2.8, 1);

INSERT INTO [dbo].[Product] ([CategoryId], [Name], [Sku], [Price], [IsAvailable])
VALUES (3, 'Raspberry Mineral Water', 'MWR', 2.8, 1);

INSERT INTO [dbo].[Product] ([CategoryId], [Name], [Sku], [Price], [IsAvailable])
VALUES (3, 'Strawberry Mineral Water', 'MWS', 2.8, 1);

INSERT INTO [dbo].[Product] ([CategoryId], [Name], [Sku], [Price], [IsAvailable])
VALUES (4, 'In the Kitchen with H+ Sport', 'PITK', 24.99, 1);

INSERT INTO [dbo].[Product] ([CategoryId], [Name], [Sku], [Price], [IsAvailable])
VALUES (5, 'Calcium 400 IU (150 tablets)', 'SC400', 9.99, 1);

INSERT INTO [dbo].[Product] ([CategoryId], [Name], [Sku], [Price], [IsAvailable])
VALUES (5, 'Flaxseed Oil 100 mg (90 capsules)', 'SFO100', 12.49, 1);

INSERT INTO [dbo].[Product] ([CategoryId], [Name], [Sku], [Price], [IsAvailable])
VALUES (5, 'Iron 65 mg (150 caplets)', 'SI65', 13.99, 1);

INSERT INTO [dbo].[Product] ([CategoryId], [Name], [Sku], [Price], [IsAvailable])
VALUES (5, 'Magnesium 250 mg (100 tablets)', 'SM250', 12.49, 1);

INSERT INTO [dbo].[Product] ([CategoryId], [Name], [Sku], [Price], [IsAvailable])
VALUES (5, 'Multi-Vitamin (90 capsules)', 'SMV', 9.99, 1);

INSERT INTO [dbo].[Product] ([CategoryId], [Name], [Sku], [Price], [IsAvailable])
VALUES (5, 'Vitamin A 10,000 IU (125 caplets)', 'SVA', 11.99, 1);

INSERT INTO [dbo].[Product] ([CategoryId], [Name], [Sku], [Price], [IsAvailable])
VALUES (5, 'Vitamin B-Complex (100 caplets)', 'SVB', 12.99, 1);

INSERT INTO [dbo].[Product] ([CategoryId], [Name], [Sku], [Price], [IsAvailable])
VALUES (5, 'Vitamin C 1000 mg (100 tablets)', 'SVC', 9.99, 1);

INSERT INTO [dbo].[Product] ([CategoryId], [Name], [Sku], [Price], [IsAvailable])
VALUES (5, 'Vitamin D3 1000 IU (100 tablets)', 'SVD3', 12.49, 1);
