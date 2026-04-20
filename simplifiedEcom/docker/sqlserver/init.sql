CREATE DATABASE EcommerceDb;
GO

USE EcommerceDb;
GO

CREATE TABLE Categories
(
    Id   INT IDENTITY PRIMARY KEY,
    Name NVARCHAR(50) NOT NULL
);

CREATE TABLE Subcategories
(
    Id         INT IDENTITY PRIMARY KEY,
    CategoryId INT          NOT NULL,
    Name       NVARCHAR(50) NOT NULL,
    FOREIGN KEY (CategoryId) REFERENCES Categories (Id)
);

CREATE TABLE Brands
(
    Id   INT IDENTITY PRIMARY KEY,
    Name NVARCHAR(50) NOT NULL
);

CREATE TABLE Products
(
    Id            INT IDENTITY (1,1) PRIMARY KEY,
    Name          NVARCHAR(100)  NOT NULL,
    Description   NVARCHAR(300),
    Price         DECIMAL(10, 2) NOT NULL,
    CategoryId    INT            NOT NULL,
    SubcategoryId INT            NOT NULL,
    BrandId       INT            NOT NULL,
    Availability  BIT            NOT NULL,
    Condition     NVARCHAR(25)   NOT NULL,
    ImageUrl      NVARCHAR(300),
    FOREIGN KEY (CategoryId) REFERENCES Categories (Id),
    FOREIGN KEY (SubcategoryId) REFERENCES Subcategories (Id),
    FOREIGN KEY (BrandId) REFERENCES Brands (Id)
);

INSERT INTO Categories (Name)
VALUES ('Women'),
       ('Men'),
       ('Kids');

INSERT INTO Subcategories (CategoryId, Name)
VALUES (1, 'Dress'),
       (1, 'Tops'),
       (1, 'Saree'),
       (2, 'T-shirts'),
       (2, 'Jeans'),
       (3, 'Dress'),
       (3, 'Tops & Shirts');

INSERT INTO Brands (Name)
VALUES ('POLO'),
       ('H&M'),
       ('MADAME'),
       ('MAST & HARBOUR'),
       ('BABYHUG'),
       ('ALLEN SOLLY JUNIOR'),
       ('KOOKIE KIDS'),
       ('BIBA');

INSERT INTO Products (Name,
                      Description,
                      Price,
                      CategoryId,
                      SubcategoryId,
                      BrandId,
                      Availability,
                      Condition,
                      ImageUrl)
VALUES ('Blue Top',
        'Stylish blue top for women',
        500,
        1,
        2,
        1,
        1,
        'New',
        'blue-top.jpg'),
       ('Men T-shirt',
        'Casual T-shirt for men',
        600,
        2,
        4,
        2,
        1,
        'New',
        'men_t-shirt.jpg'),
       ('Kids Dress',
        'Comfortable dress for kids',
        420,
        3,
        6,
        1,
        1,
        'New',
        'kids-dress.jpg'),
       ('Women Saree',
        'Elegant saree',
        1200,
        1,
        3,
        2,
        1,
        'Used',
        'women-saree.jpg'),
       ('Men Jeans',
        'Grunt blue slim fit jeans',
        1400,
        2,
        5,
        1,
        1,
        'New',
        'men-jeans-polo.jpg');
    
CREATE TABLE Users
(
    Id INT IDENTITY PRIMARY KEY,
    Email NVARCHAR(100) NOT NULL UNIQUE,
    PasswordHash NVARCHAR(255) NOT NULL,
    Name NVARCHAR(70)
);

CREATE TABLE Carts(
    Id INT IDENTITY PRIMARY KEY,
    UserId INT NOT NULL UNIQUE,
    FOREIGN KEY (UserId) REFERENCES Users(Id)
);

CREATE TABLE CartItems(
    Id INT IDENTITY PRIMARY KEY,
    CartId INT,
    ProductId INT,
    Quantity INT,
    FOREIGN KEY (CartId) REFERENCES Carts(Id),
    FOREIGN KEY (ProductId) REFERENCES Products(Id),
    UNIQUE (CartId, ProductId)
);

CREATE TABLE Orders(
    Id INT IDENTITY PRIMARY KEY,
    UserId INT,
    TotalAmount DECIMAL(10,2),
    
    FOREIGN KEY (UserId) REFERENCES Users(Id)
);

CREATE TABLE OrderItems(
    Id INT IDENTITY PRIMARY KEY,
    OrderId INT,
    ProductId INT,
    
    FOREIGN KEY (OrderId) REFERENCES Orders(Id),
    FOREIGN KEY (ProductId) REFERENCES Products(Id)
);