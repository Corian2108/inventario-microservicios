-- Base de datos del servicio transacciones
-- Autor: Agustin Ruiz
-- Fecha: 28/01/2026
-- Descripción: en esta base de datos se almacenan las transacciones, ventas o compras

CREATE DATABASE TRANSACTIONS_DB

-- parámetros, los tipos de transacción que pueden ser compra, venta, baja del inventario
USE TRANSACTIONS_DB
CREATE TABLE [type] (
    [type_id] INT IDENTITY(1,1) NOT NULL,
    [name] NVARCHAR(100) NOT NULL,
    is_active BIT NOT NULL DEFAULT 1,
    CONSTRAINT PK_type PRIMARY KEY ([type_id])
)

-- tabla de las transacciones
USE TRANSACTIONS_DB
CREATE TABLE transactions (
    transaction_id INT IDENTITY(1,1) NOT NULL,
    product_id INT NOT NULL,
    type_fk INT NOT NULL,
    quantity INT NOT NULL,
    [date] DATETIME NOT NULL,
    notes NVARCHAR(50) NULL,
    created_at DATETIME NOT NULL DEFAULT GETDATE(),
    CONSTRAINT PK_transacrions PRIMARY KEY (transaction_id),
    CONSTRAINT FK_transaction_type 
        FOREIGN KEY (type_fk) 
        REFERENCES [type]([type_id])
)

-- Stored prodcedure para inicializar la tabla types
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE sp_seed_types
AS
BEGIN
    SET NOCOUNT ON;

	IF (SELECT COUNT([name]) FROM [type]) = 0
	BEGIN
		
		INSERT INTO [type] ([name])
		VALUES ('Compra'),
		('Venta'),
		('Baja de inventario')

	END
END
GO

--Ejecución del SP
EXEC sp_seed_types

--inserción de datos de prueba
INSERT INTO transactions (product_id, type_fk, quantity, [date], notes)
VALUES (1, 1, 200, '2026-01-27', 'ingreso de inventrio'),
(4, 2, 1, '2026-01-27', 'venta al detal'),
(6, 3, 2, '2026-01-27', 'daño por humedad')
