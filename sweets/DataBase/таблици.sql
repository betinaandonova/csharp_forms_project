CREATE TABLE groups (
group_id CHAR(3) PRIMARY KEY,
group_name NVARCHAR(50) NOT NULL
);

CREATE TABLE assortment (
assortment_id CHAR(5) PRIMARY KEY,
assortment_name CHAR(5) NOT NULL,
group_id CHAR(3) NOT NULL,
recipe VARCHAR(MAX),
weight INT,
unit_price DECIMAL(10,2)
 CONSTRAINT FK_Assortment_Groups FOREIGN KEY (group_id)
        REFERENCES Groups(group_id)

);
IF OBJECT_ID('orders', 'U') IS NOT NULL
   DROP TABLE dbo.orders;

CREATE TABLE orders (
    order_id INT PRIMARY KEY,
    asortment_id CHAR(5) NOT NULL,
    availability_to_make BIT NOT NULL, -- 0 = Не, 1 = Да
    delivery_date DATE NOT NULL,
    unit_price FLOAT,
    quantity INT,
    CONSTRAINT FK_Orders_Assortment FOREIGN KEY (asortment_id)
        REFERENCES Assortment(assortment_id)
);