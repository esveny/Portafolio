-- Crear base de datos
CREATE DATABASE Proyecto404;
GO

USE Proyecto404;
GO

-- Tabla de TipoEntidad
CREATE TABLE TipoEntidad (
    IdTipoEntidad INT PRIMARY KEY IDENTITY(1,1),
    NombreTipoEntidad VARCHAR(30) NOT NULL UNIQUE,
    Descripcion VARCHAR(150) NOT NULL,
    FechaDeRegistro DATETIME NOT NULL DEFAULT GETDATE(),
    FechaDeModificacion DATETIME NULL,
    Estado BIT NOT NULL DEFAULT 1
);
GO

-- Tabla de Entidad
CREATE TABLE Entidad (
    IdEntidad INT PRIMARY KEY IDENTITY(1,1),
    CodigoEntidad VARCHAR(30) NOT NULL UNIQUE,
    NombreEntidad VARCHAR(100) NOT NULL,
    TelefonoEntidad VARCHAR(50) NOT NULL,
    CorreoElectronico VARCHAR(100) NOT NULL,
    Direccion VARCHAR(400) NOT NULL CHECK (LEN(Direccion) >= 20),
    FechaDeRegistro DATETIME NOT NULL DEFAULT GETDATE(),
    FechaDeModificacion DATETIME NOT NULL DEFAULT GETDATE(),
    IdTipoEntidad INT NOT NULL,
    Estado BIT NOT NULL DEFAULT 1,
    FOREIGN KEY (IdTipoEntidad) REFERENCES TipoEntidad(IdTipoEntidad)
);
GO

-- Tabla de Contadores
CREATE TABLE Contador (
    IdContador INT PRIMARY KEY IDENTITY(1,1),
    IdContadorIdentity UNIQUEIDENTIFIER DEFAULT NEWID(),
    IdentificacionContador VARCHAR(10) NOT NULL UNIQUE,
    NombreContador VARCHAR(30) NOT NULL,
    PrimerApellidoContador VARCHAR(30) NOT NULL,
    SegundoApellidoContador VARCHAR(30) NOT NULL,
    NumeroDeColegio VARCHAR(20) NOT NULL,
    CorreoElectronico VARCHAR(50) NOT NULL,
    TelefonoCelular VARCHAR(10) NOT NULL,
    TelefonoSecundario VARCHAR(10) NOT NULL,
    MetodoDeContacto INT NOT NULL CHECK (MetodoDeContacto BETWEEN 1 AND 4),
    FechaDeRegistro DATETIME NOT NULL DEFAULT GETDATE(),
    FechaDeModificacion DATETIME NULL,
    Estado BIT NOT NULL DEFAULT 1
);
GO

-- Tabla de Bitácora de Eventos
CREATE TABLE BitacoraEventos (
    IdEvento INT PRIMARY KEY IDENTITY(1,1),
    TablaDeEvento VARCHAR(20) NOT NULL,
    TipoDeEvento VARCHAR(20) NOT NULL,
    FechaDeEvento DATETIME NOT NULL DEFAULT GETDATE(),
    DescripcionDeEvento VARCHAR(MAX) NOT NULL,
    StackTrace VARCHAR(MAX) NULL,
    DatosAnteriores VARCHAR(MAX) NULL,
    DatosPosteriores VARCHAR(MAX) NULL
);
GO


-- INSERT en TipoEntidad
INSERT INTO TipoEntidad (NombreTipoEntidad, Descripcion, Estado)
VALUES 
    ('Banco', 'Entidad financiera con funciones bancarias', 1);


-- INSERT en Entidad
INSERT INTO Entidad (CodigoEntidad, NombreEntidad, TelefonoEntidad, CorreoElectronico, Direccion, IdTipoEntidad, Estado)
VALUES 
    ('ENT001', 'Banco Nacional', '2222-3333', 'contacto@bancional.com', 'San José, Costa Rica', 1, 1);

-- INSERT en Contador
INSERT INTO Contador (IdentificacionContador, NombreContador, PrimerApellidoContador, SegundoApellidoContador, NumeroDeColegio, CorreoElectronico, TelefonoCelular, TelefonoSecundario, MetodoDeContacto, Estado)
VALUES 
    ('C001', 'Felo', 'Salas', 'Sánchez', 'COL-12345', 'felo.salas@contadores.com', '8963-1233', '1111-2222', 1, 1);

-- INSERT en BitácoraEventos (ejemplo de eventos)
INSERT INTO BitacoraEventos (TablaDeEvento, TipoDeEvento, DescripcionDeEvento, StackTrace, DatosAnteriores, DatosPosteriores)
VALUES 
    ('Entidad', 'Registrar', 'Entidad Banco Nacional registrada', NULL, NULL, '{ "CodigoEntidad": "ENT001", "NombreEntidad": "Banco Nacional" }');



INSERT INTO Contador (
    IdentificacionContador,
    NombreContador,
    PrimerApellidoContador,
    SegundoApellidoContador,
    NumeroDeColegio,
    CorreoElectronico,
    TelefonoCelular,
    TelefonoSecundario,
    MetodoDeContacto,
    FechaDeRegistro,
    FechaDeModificacion,
    Estado
)
VALUES (
    '1234567890',                   -- Identificación única (10 caracteres)
    'Carlos',
    'Ramírez',
    'Jiménez',
    'COL-56789',
    'carlos.ramirez@email.com',
    '88887777',
    '22223333',
    1,                              -- Método de contacto válido (1–4)
    GETDATE(),
    NULL,
    1                               -- Activo
);


INSERT INTO ReservaLiquidez (
    IdEntidad,
    MontoDeReserva,
    CantidadDeBeneficiarios,
    MontoDeSeguroBancario,
    Periodo,
    IdContador,
    FechaDeRegistro,
    FechaDeModificacion,
    Estado
)
VALUES (
    1,                    -- Asegúrate de que exista un Entidad con Id = 1
    500000.00,
    120,
    250000.00,
    '2024-12-31',
    1,                    -- Asegúrate de que exista un Contador con Id = 1
    GETDATE(),
    GETDATE(),
    1                     -- Estado: Registrado
);


INSERT INTO Entidad (
    CodigoEntidad,
    NombreEntidad,
    TelefonoEntidad,
    CorreoElectronico,
    Direccion,
    FechaDeRegistro,
    FechaDeModificacion,
    IdTipoEntidad,
    Estado
)
VALUES (
    'ENT-001',
    'Entidad Financiera ABC',
    '22778899',
    'contacto@entidadabc.com',
    'Calle 123, Barrio Centro, Ciudad, Provincia',
    GETDATE(),
    GETDATE(),
    1, -- Asegúrate que este IdTipoEntidad exista
    1
);
