-- =====================================================
-- Usuarios_Roles_Auditorias.sql
-- Para una base db_veterinaria que YA existe: agrega las
-- tablas roles, usuarios y auditorias. Se puede ejecutar
-- mas de una vez sin duplicar nada.
-- =====================================================
USE db_veterinaria;
GO

IF OBJECT_ID('roles', 'U') IS NULL
CREATE TABLE [roles]
(
    [id] INT PRIMARY KEY IDENTITY(1, 1) NOT NULL,
    [nombre] NVARCHAR(100) NOT NULL UNIQUE,
    [descripcion] NVARCHAR(500) NOT NULL
);
GO

IF OBJECT_ID('usuarios', 'U') IS NULL
CREATE TABLE [usuarios]
(
    [id] INT PRIMARY KEY IDENTITY(1, 1) NOT NULL,
    [nombreUsuario] NVARCHAR(100) NOT NULL UNIQUE,
    [contrasena] NVARCHAR(200) NOT NULL,
    [rol] INT NOT NULL REFERENCES [roles](id),
    [estado] NVARCHAR(50) NOT NULL
);
GO

IF OBJECT_ID('auditorias', 'U') IS NULL
CREATE TABLE [auditorias]
(
    [id] INT PRIMARY KEY IDENTITY(1, 1) NOT NULL,
    [usuario] INT NULL REFERENCES [usuarios](id),
    [fecha] DATETIME NOT NULL,
    [accion] NVARCHAR(50) NOT NULL,
    [entidad] NVARCHAR(100) NOT NULL,
    [registro] INT NULL,
    [detalle] NVARCHAR(500) NULL
);
GO

IF NOT EXISTS (SELECT 1 FROM [roles] WHERE [nombre] = 'Administrador')
    INSERT INTO [roles] ([nombre], [descripcion]) VALUES ('Administrador', 'Acceso a todas las ventanas');
IF NOT EXISTS (SELECT 1 FROM [roles] WHERE [nombre] = 'Usuario')
    INSERT INTO [roles] ([nombre], [descripcion]) VALUES ('Usuario', 'Acceso a las ventanas del consultorio');
GO

-- Usuarios iniciales (las contrasenas estan cifradas con SHA-256)
IF NOT EXISTS (SELECT 1 FROM [usuarios] WHERE [nombreUsuario] = 'admin')
    INSERT INTO [usuarios] ([nombreUsuario], [contrasena], [rol], [estado])
    SELECT 'admin', '3B612C75A7B5048A435FB6EC81E52FF92D6D795A8B5A9C17070F6A63C97A53B2', [id], 'Activo' FROM [roles] WHERE [nombre] = 'Administrador';
IF NOT EXISTS (SELECT 1 FROM [usuarios] WHERE [nombreUsuario] = 'practicante')
    INSERT INTO [usuarios] ([nombreUsuario], [contrasena], [rol], [estado])
    SELECT 'practicante', '01E18C24453B626B59C02C2855647BA2829478AD03E08639598A91E4D46A531B', [id], 'Activo' FROM [roles] WHERE [nombre] = 'Usuario';
GO
