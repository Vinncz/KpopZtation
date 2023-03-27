INSERT INTO Customer (CustomerID, CustomerName, CustomerEmail, CustomerPassword, CustomerGender, CustomerAddress, CustomerRole)
VALUES
    (1, 'John Doe', 'johndoe@email.com', 'password123', 'Male', '123 Main Street', 'User'),
    (2, 'Jane Smith', 'janesmith@email.com', 'password456', 'Female', '456 Elm Street', 'User'),
    (3, 'Bob Johnson', 'bobjohnson@email.com', 'password789', 'Male', '789 Oak Street', 'Admin'),
    (4, 'Alice Brown', 'alicebrown@email.com', 'password321', 'Female', '321 Pine Street', 'User'),
    (5, 'Charlie Davis', 'charliedavis@email.com', 'password654', 'Male', '654 Maple Street', 'User');

INSERT INTO Artist (ArtistID, ArtistName, ArtistImage)
VALUES
    (1, 'The Beatles', 'the-beatles.jpg'),
    (2, 'Elvis Presley', 'elvis-presley.jpg'),
    (3, 'Michael Jackson', 'michael-jackson.jpg'),
    (4, 'Madonna', 'madonna.jpg'),
    (5, 'Led Zeppelin', 'led-zeppelin.jpg');

INSERT INTO Album (AlbumID, ArtistID, AlbumName, AlbumImage, AlbumPrice, AlbumStock, AlbumDescription)
VALUES
    (1, 1, 'Abbey Road', '/images/abbey-road.jpg', 20, 1000, 'The Beatles eleventh studio album'),
    (2, 2, 'Elvis Presley', '/images/elvis-presley-album.jpg', 15, 500, 'Elvis Presleys debut studio album'),
    (3, 3, 'Thriller', '/images/thriller.jpg', 25, 2000, 'Michael Jacksons sixth studio album'),
    (4, 4, 'Like a Virgin', '/images/like-a-virgin.jpg', 18, 1500, 'Madonnas second studio album'),
    (5, 5, 'Led Zeppelin IV', '/images/led-zeppelin-iv.jpg', 22, 1200, 'Led Zeppelins fourth studio album');

INSERT INTO Cart (CustomerID , AlbumID , Quantity )
VALUES
    (1, 1, 2),
    (1, 3, 1),
    (2, 2, 3),
    (3, 5, 4),
    (4, 4, 2);

INSERT INTO TransactionHeader (TransactionID , TransactionDate , CustomerID )
VALUES
    (1, '2023-03-27', 1),
    (2, '2023-03-28', 2),
    (3, '2023-03-29', 3),
    (4, '2023-03-30', 4),
    (5, '2023-03-31', 5);

INSERT INTO TransactionDetail (TransactionID , AlbumID , Quantity )
VALUES
    (1, 1, 2),
    (1, 3, 1),
    (2, 2, 3),
    (3, 5, 4),
    (4, 4, 2),
    (5, 1, 1);