/*
CREATE DATABASE db_veterinaria;
GO
USE db_veterinaria;
GO

-- 1. PERSONAS
CREATE TABLE [personas]
(
    [id] INT PRIMARY KEY IDENTITY(1, 1) NOT NULL,
    [nombre] NVARCHAR(200) NOT NULL,
    [telefono] NVARCHAR(50) NOT NULL,
    [email] NVARCHAR(200) NOT NULL,
    [direccion] NVARCHAR(200) NOT NULL,
    [fechaNacimiento] SMALLDATETIME NOT NULL
);

-- 2. CLIENTES
CREATE TABLE [clientes]
(
    [id] INT PRIMARY KEY IDENTITY(1, 1) NOT NULL,
    [persona] INT UNIQUE NOT NULL REFERENCES [personas](id),
    [tipoCliente] NVARCHAR(100) NOT NULL,
    [fechaRegistro] SMALLDATETIME NOT NULL,
    [estado] NVARCHAR(50) NOT NULL,
    [observaciones] NVARCHAR(500) NULL,
    [preferenciaContacto] NVARCHAR(100) NOT NULL
);

-- 3. VETERINARIOS
CREATE TABLE [veterinarios]
(
    [id] INT PRIMARY KEY IDENTITY(1, 1) NOT NULL,
    [persona] INT UNIQUE NOT NULL REFERENCES [personas](id),
    [especialidad] NVARCHAR(150) NOT NULL,
    [registroProfesional] NVARCHAR(100) NOT NULL,
    [fechaIngreso] SMALLDATETIME NOT NULL,
    [estado] NVARCHAR(50) NOT NULL,
    [telefonoLaboral] NVARCHAR(50) NOT NULL
);

-- 4. EMPLEADOS
CREATE TABLE [empleados]
(
    [id] INT PRIMARY KEY IDENTITY(1, 1) NOT NULL,
    [persona] INT UNIQUE NOT NULL REFERENCES [personas](id),
    [cargo] NVARCHAR(100) NOT NULL,
    [fechaIngreso] SMALLDATETIME NOT NULL,
    [salario] DECIMAL(12, 2) NOT NULL,
    [estado] NVARCHAR(50) NOT NULL,
    [turno] NVARCHAR(50) NOT NULL
);

-- 5. ESPECIES
CREATE TABLE [especies]
(
    [id] INT PRIMARY KEY IDENTITY(1, 1) NOT NULL,
    [nombre] NVARCHAR(100) NOT NULL,
    [descripcion] NVARCHAR(500) NOT NULL
);

-- 6. RAZAS
CREATE TABLE [razas]
(
    [id] INT PRIMARY KEY IDENTITY(1, 1) NOT NULL,
    [especie] INT NOT NULL REFERENCES [especies](id),
    [nombre] NVARCHAR(100) NOT NULL,
    [tamanoPromedio] DECIMAL(10, 2) NOT NULL,
    [pesoPromedio] DECIMAL(10, 2) NOT NULL,
    [esperanzaVidaAnios] INT NOT NULL,
    [descripcion] NVARCHAR(500) NOT NULL
);

-- 7. FINCAS
CREATE TABLE [fincas]
(
    [id] INT PRIMARY KEY IDENTITY(1, 1) NOT NULL,
    [cliente] INT NOT NULL REFERENCES [clientes](id),
    [nombre] NVARCHAR(200) NOT NULL,
    [direccion] NVARCHAR(300) NOT NULL,
    [areaHectareas] DECIMAL(10, 2) NOT NULL,
    [tipoProduccion] NVARCHAR(100) NOT NULL,
    [estado] NVARCHAR(50) NOT NULL
);

-- 8. ANIMALES
CREATE TABLE [animales]
(
    [id] INT PRIMARY KEY IDENTITY(1, 1) NOT NULL,
    [raza] INT NOT NULL REFERENCES [razas](id),
    [cliente] INT NOT NULL REFERENCES [clientes](id),
    [finca] INT NOT NULL REFERENCES [fincas](id),
    [nombre] NVARCHAR(100) NOT NULL,
    [sexo] NVARCHAR(20) NOT NULL,
    [fechaNacimiento] SMALLDATETIME NOT NULL,
    [color] NVARCHAR(50) NOT NULL,
    [peso] DECIMAL(10, 2) NOT NULL,
    [estado] NVARCHAR(50) NOT NULL
);

-- 9. CITAS
CREATE TABLE [citas]
(
    [id] INT PRIMARY KEY IDENTITY(1, 1) NOT NULL,
    [animal] INT NOT NULL REFERENCES [animales](id),
    [veterinario] INT NOT NULL REFERENCES [veterinarios](id),
    [fecha] SMALLDATETIME NOT NULL,
    [hora] TIME NOT NULL,
    [motivo] NVARCHAR(300) NOT NULL,
    [estado] NVARCHAR(50) NOT NULL,
    [observaciones] NVARCHAR(500) NULL
);

-- 10. CONSULTAS
CREATE TABLE [consultas]
(
    [id] INT PRIMARY KEY IDENTITY(1, 1) NOT NULL,
    [cita] INT UNIQUE NOT NULL REFERENCES [citas](id),
    [animal] INT NOT NULL REFERENCES [animales](id),
    [veterinario] INT NOT NULL REFERENCES [veterinarios](id),
    [fecha] SMALLDATETIME NOT NULL,
    [diagnostico] NVARCHAR(500) NOT NULL,
    [observaciones] NVARCHAR(500) NULL,
    [pesoActual] DECIMAL(10, 2) NOT NULL,
    [temperatura] DECIMAL(5, 2) NOT NULL
);

-- 11. HISTORIAS CLINICAS
CREATE TABLE [historiasClinicas]
(
    [id] INT PRIMARY KEY IDENTITY(1, 1) NOT NULL,
    [animal] INT UNIQUE NOT NULL REFERENCES [animales](id),
    [antecedentes] NVARCHAR(MAX) NULL,
    [alergias] NVARCHAR(MAX) NULL,
    [observaciones] NVARCHAR(MAX) NULL
);

-- 12. PROVEEDORES
CREATE TABLE [proveedores]
(
    [id] INT PRIMARY KEY IDENTITY(1, 1) NOT NULL,
    [nombre] NVARCHAR(200) NOT NULL,
    [telefono] NVARCHAR(50) NOT NULL,
    [email] NVARCHAR(200) NOT NULL,
    [direccion] NVARCHAR(300) NOT NULL,
    [nit] NVARCHAR(50) NOT NULL
);

-- 13. PRODUCTOS
CREATE TABLE [productos]
(
    [id] INT PRIMARY KEY IDENTITY(1, 1) NOT NULL,
    [nombre] NVARCHAR(200) NOT NULL,
    [tipo] NVARCHAR(100) NOT NULL,
    [precio] DECIMAL(12, 2) NOT NULL,
    [stock] INT NOT NULL,
    [descripcion] NVARCHAR(500) NOT NULL
);

-- 14. VACUNACIONES
CREATE TABLE [vacunaciones]
(
    [id] INT PRIMARY KEY IDENTITY(1, 1) NOT NULL,
    [animal] INT NOT NULL REFERENCES [animales](id),
    [producto] INT NOT NULL REFERENCES [productos](id),
    [fecha] SMALLDATETIME NOT NULL,
    [observacion] NVARCHAR(500) NULL,
    [proximaFecha] SMALLDATETIME NOT NULL,
    [lote] NVARCHAR(100) NOT NULL,
    [dosis] DECIMAL(10, 2) NOT NULL
);

-- 15. TRATAMIENTOS
CREATE TABLE [tratamientos]
(
    [id] INT PRIMARY KEY IDENTITY(1, 1) NOT NULL,
    [animal] INT NOT NULL REFERENCES [animales](id),
    [consulta] INT NOT NULL REFERENCES [consultas](id),
    [producto] INT NOT NULL REFERENCES [productos](id),
    [dosis] DECIMAL(10, 2) NOT NULL,
    [frecuencia] NVARCHAR(100) NOT NULL,
    [duracion] INT NOT NULL,
    [indicaciones] NVARCHAR(500) NOT NULL,
    [fechaInicio] SMALLDATETIME NOT NULL
);

-- 16. COMPRAS
CREATE TABLE [compras]
(
    [id] INT PRIMARY KEY IDENTITY(1, 1) NOT NULL,
    [proveedor] INT NOT NULL REFERENCES [proveedores](id),
    [fecha] SMALLDATETIME NOT NULL,
    [total] DECIMAL(12, 2) NOT NULL,
    [numeroFactura] NVARCHAR(100) NOT NULL,
    [estado] NVARCHAR(50) NOT NULL,
    [observaciones] NVARCHAR(500) NULL
);

-- 17. DETALLE DE COMPRAS
CREATE TABLE [detalleCompras]
(
    [id] INT PRIMARY KEY IDENTITY(1, 1) NOT NULL,
    [compra] INT NOT NULL REFERENCES [compras](id),
    [producto] INT NOT NULL REFERENCES [productos](id),
    [cantidad] INT NOT NULL,
    [precio] DECIMAL(12, 2) NOT NULL
);

-- 18. SERVICIOS
CREATE TABLE [servicios]
(
    [id] INT PRIMARY KEY IDENTITY(1, 1) NOT NULL,
    [nombre] NVARCHAR(200) NOT NULL,
    [descripcion] NVARCHAR(500) NOT NULL,
    [precio] DECIMAL(12, 2) NOT NULL,
    [duracion] INT NOT NULL,
    [categoria] NVARCHAR(100) NOT NULL
);

-- 19. FACTURAS
CREATE TABLE [facturas]
(
    [id] INT PRIMARY KEY IDENTITY(1, 1) NOT NULL,
    [cliente] INT NOT NULL REFERENCES [clientes](id),
    [fecha] SMALLDATETIME NOT NULL,
    [total] DECIMAL(12, 2) NOT NULL,
    [metodoPago] NVARCHAR(100) NOT NULL,
    [estado] NVARCHAR(50) NOT NULL,
    [observaciones] NVARCHAR(500) NULL
);

-- 20. DETALLE DE FACTURAS
CREATE TABLE [detalleFacturas]
(
    [id] INT PRIMARY KEY IDENTITY(1, 1) NOT NULL,
    [factura] INT NOT NULL REFERENCES [facturas](id),
    [producto] INT NULL REFERENCES [productos](id),
    [servicio] INT NULL REFERENCES [servicios](id),
    [cantidad] INT NOT NULL,
    [precio] DECIMAL(12, 2) NOT NULL
);
*/