CREATE TABLE Customer (
    CustomerID INT PRIMARY KEY NOT NULL,
    CustomerName VARCHAR(50) NOT NULL,
    CustomerEmail VARCHAR(50) NOT NULL,
    CustomerPassword VARCHAR(50) NOT NULL,
    CustomerGender VARCHAR(6) NOT NULL,
    CustomerAddress VARCHAR(100) NOT NULL,
    CustomerRole VARCHAR(5) NOT NULL
);

CREATE TABLE Artist (
    ArtistID INT PRIMARY KEY NOT NULL,
    ArtistName VARCHAR(50) NOT NULL,
    ArtistImage VARCHAR(50)
);

CREATE TABLE Album (
    AlbumID INT PRIMARY KEY NOT NULL,
    ArtistID INT FOREIGN KEY REFERENCES Artist(ArtistID) NOT NULL,
    AlbumName VARCHAR(50) NOT NULL,
    AlbumImage VARCHAR(50),
    AlbumPrice INT NOT NULL,
    AlbumStock INT NOT NULL,
    AlbumDescription VARCHAR(255)
);

CREATE TABLE Cart (
    CustomerID INT FOREIGN KEY REFERENCES Customer(CustomerID) NOT NULL,
    AlbumID INT FOREIGN KEY REFERENCES Album(AlbumID) NOT NULL,
    Quantity INT NOT NULL
);

CREATE TABLE TransactionHeader (
    TransactionID INT PRIMARY KEY NOT NULL,
    TransactionDate DATE NOT NULL,
    CustomerID INT FOREIGN KEY REFERENCES Customer(CustomerID) NOT NULL
);

CREATE TABLE TransactionDetail (
    TransactionID INT FOREIGN KEY REFERENCES TransactionHeader(TransactionID) NOT NULL,
    AlbumID INT FOREIGN KEY REFERENCES Album(AlbumID) NOT NULL,
    Quantity INT NOT NULL
);