-- Tablas del Sistema de Control Clínico Veterinario v2.
-- Se ejecuta automáticamente al iniciar el programa (IF NOT EXISTS evita errores si ya existen).

CREATE TABLE IF NOT EXISTS Propietarios (
    Id          INTEGER PRIMARY KEY AUTOINCREMENT,
    Nombre      TEXT NOT NULL,
    Apellido    TEXT NOT NULL,
    Telefono    TEXT NOT NULL,
    Correo      TEXT NOT NULL
);

CREATE TABLE IF NOT EXISTS Veterinarios (
    Id                INTEGER PRIMARY KEY AUTOINCREMENT,
    Nombre            TEXT NOT NULL,
    Apellido          TEXT NOT NULL,
    Especialidad      TEXT NOT NULL,
    NumeroColegiado   TEXT NOT NULL UNIQUE
);

CREATE TABLE IF NOT EXISTS Mascotas (
    Id                INTEGER PRIMARY KEY AUTOINCREMENT,
    PropietarioId     INTEGER NOT NULL,
    Nombre            TEXT NOT NULL,
    Especie           TEXT NOT NULL,
    Raza              TEXT NOT NULL,
    FechaNacimiento   TEXT NOT NULL,
    Peso              REAL NOT NULL CHECK (Peso > 0),
    FOREIGN KEY (PropietarioId) REFERENCES Propietarios(Id) ON DELETE CASCADE
);

CREATE TABLE IF NOT EXISTS Citas (
    Id              INTEGER PRIMARY KEY AUTOINCREMENT,
    MascotaId       INTEGER NOT NULL,
    VeterinarioId   INTEGER NOT NULL,
    FechaHora       TEXT NOT NULL,
    Motivo          TEXT NOT NULL,
    Estado          TEXT NOT NULL CHECK (Estado IN ('Programada', 'Atendida', 'Cancelada')),
    FOREIGN KEY (MascotaId) REFERENCES Mascotas(Id) ON DELETE CASCADE,
    FOREIGN KEY (VeterinarioId) REFERENCES Veterinarios(Id)
);

CREATE TABLE IF NOT EXISTS Consultas (
    Id              INTEGER PRIMARY KEY AUTOINCREMENT,
    MascotaId       INTEGER NOT NULL,
    VeterinarioId   INTEGER NOT NULL,
    Fecha           TEXT NOT NULL,
    Sintomas        TEXT NOT NULL,
    Diagnostico     TEXT NOT NULL,
    Tratamiento     TEXT NOT NULL,
    PesoRegistrado  REAL NOT NULL CHECK (PesoRegistrado > 0),
    FOREIGN KEY (MascotaId) REFERENCES Mascotas(Id) ON DELETE CASCADE,
    FOREIGN KEY (VeterinarioId) REFERENCES Veterinarios(Id)
);

CREATE INDEX IF NOT EXISTS Ix_Mascotas_Propietario ON Mascotas(PropietarioId);
CREATE INDEX IF NOT EXISTS Ix_Citas_Mascota ON Citas(MascotaId);
CREATE INDEX IF NOT EXISTS Ix_Consultas_Mascota ON Consultas(MascotaId);
