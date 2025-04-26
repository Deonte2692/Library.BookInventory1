CREATE DATABASE BookInventoryDB;
USE BookInventoryDB;
CREATE TABLE Books (
       Id INT AUTO_INCREMENT PRIMARY KEY,
    Title VARCHAR(255) NOT NULL,
    Author VARCHAR(255) NOT NULL,
    Category VARCHAR(100),
    IsAvailable BOOLEAN NOT NULL DEFAULT TRUE
);
INSERT INTO Books (Title, Author, Category, IsAvailable) VALUES
('The Great Gatsby', 'F. Scott Fitzgerald', 'Fiction', TRUE),
('To Kill a Mockingbird', 'Harper Lee', 'Fiction', TRUE),
('The Lean Startup', 'Eric Ries', 'Business', TRUE);