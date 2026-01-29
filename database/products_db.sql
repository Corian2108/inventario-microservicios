-- Base de datos del servicio productos
-- Autor: Agustin Ruiz
-- Fecha: 28/01/2026
-- Descripción: Esta base de datos se encarga de almacenar todos los datos relacionados a los productos

CREATE DATABASE PRODUCTS_DB

-- tabla de parámetros de las categorías de productos
USE PRODUCTS_DB
CREATE TABLE category (
    category_id INT IDENTITY(1,1) NOT NULL,
    [name] NVARCHAR(100) NOT NULL,
    [description] NVARCHAR(255) NULL,
    is_active BIT NOT NULL DEFAULT 1,
    CONSTRAINT PK_category PRIMARY KEY (category_id)
)

-- tabla de los productos
CREATE TABLE products (
    product_id INT IDENTITY(1,1) NOT NULL,
    [name] NVARCHAR(150) NOT NULL,
    category_fk INT NOT NULL,
    [description] NVARCHAR(255) NULL,
    price DECIMAL(18,2) NOT NULL,
    stock INT NOT NULL,
    batch NVARCHAR(50) NULL,
    entry_date DATETIME NOT NULL,
    image_url NVARCHAR(255) NULL,
    created_at DATETIME NOT NULL DEFAULT GETDATE(),
    is_active BIT NOT NULL DEFAULT 1,
    CONSTRAINT PK_products PRIMARY KEY (product_id),
    CONSTRAINT FK_products_category 
        FOREIGN KEY (category_fk) 
        REFERENCES category(category_id)
)

-- Stored prodcedure encargado de inicializar las categorías de productos
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE sp_seed_categories
AS
BEGIN
    SET NOCOUNT ON;

	IF (SELECT COUNT([name]) FROM category) = 0
	BEGIN
		
		INSERT INTO category ([name], [description])
        VALUES ('Alimentos y Bebidas', 'Productos comestibles y bebidas para consumo humano'),
		('Productos de Limpieza', 'Artículos para limpieza del hogar e industrial'),
		('Productos Plásticos', 'Envases y productos elaborados a base de plástico'),
		('Cuidado Personal', 'Productos de higiene y cuidado personal'),
		('Bebidas', 'Bebidas alcohólicas y no alcohólicas'),
		('Útiles de Oficina', 'Artículos utilizados en entornos de oficina')

	END

END
GO

-- Ejecución del SP
EXEC sp_seed_categories;

SELECT * FROM category

-- Inserción de datos de prueba
INSERT INTO products ([name], category_fk, [description], price, stock, batch, entry_date, image_url)
VALUES ('Arroz Blanco envejecido 1kg', 1, 'Arroz blanco de la marca osito', 1.45, 120, 'L2026-ARZ-01', '2026-01-26', 'assets/img/1.jpg'),
('Detergente Líquido Deja 2L', 2, 'Deterjente líquido sueve con las manos marca deja', 4.80, 45, 'L2026-DET-02', '2026-01-26', 'assets/img/2.jpg'),
('Botella Plástica 1L', 3, 'Botella para contener bebidas de consumo, no apto para limpiadores',  0.35, 300, 'L2026-BOT-05', '2026-01-26', 'assets/img/3.jpg'),
('Shampoo Anticaspa Savital 400ml', 4, 'Shampoo para evitar la caspa en estados tempranos',  3.95, 60, 'L2026-SHAM-03', '2026-01-26', 'assets/img/4.jpg'),
('Gaseosa CocaCola 2L', 5, 'Formula tradicional', 2.10, 80, 'L2026-GAS-07', '2026-01-26', 'assets/img/5.jpg'),
('Resma de Papel A4', 6, 'Marca high tech apta para impresora laser', 6.50, 25, 'L2026-PAP-04', '2026-01-26', 'assets/img/6.jpg')
