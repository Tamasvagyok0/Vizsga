/*
	Soprt adatbázis létrehozása Database First technológiával 
	
	- Lépj be a Microsoft SQL Server Management studióba
		sa / 1A2w3e4F

	- Írd a scriptet egy új Query ablakba

   	- Futtasd le a scriptet F5 billentyûvel, vagy az Execute parancsgombbal.
*/


-- Adatbázis létrehozása
CREATE DATABASE Vizsgazo2; -- Adatbázis létrehozása
GO -- Nagyon fontos ezzel hajtja végre valóban a parancsot

USE Vizsgazo2; -- Adatbázis használatba vétele
GO

-- Ország tábla
CREATE TABLE Orszag (
    OrszagId INT IDENTITY(1,1) NOT NULL, -- Egyedi azonosító
    OrszagKod NVARCHAR(10) NOT NULL UNIQUE, -- Nem ismétlõdhet
    CONSTRAINT PK_Orszag PRIMARY KEY CLUSTERED (OrszagId) -- Clustere index a kulcsra
);

-- Helyszín tábla
CREATE TABLE Helyszin (
    HelyszinId INT IDENTITY(1,1) NOT NULL,
    HelyszinNev NVARCHAR(100) NOT NULL UNIQUE,
    CONSTRAINT PK_Helyszin PRIMARY KEY CLUSTERED (HelyszinId)
);

-- Sportoló tábla
CREATE TABLE Sportolo (
    SportoloId INT IDENTITY(1,1) NOT NULL,
    SportoloNev NVARCHAR(100) NOT NULL,
    OrszagId INT NOT NULL,
    CONSTRAINT PK_Sportolo PRIMARY KEY CLUSTERED (SportoloId),  -- Kapcsolatok 
    CONSTRAINT FK_Sportolo_Orszag FOREIGN KEY (OrszagId) REFERENCES Orszag(OrszagId)
);

-- Verseny tábla
CREATE TABLE Verseny (
    VersenyId INT IDENTITY(1,1) NOT NULL,
    Datum DATE NOT NULL,
    HelyszinId INT NOT NULL,
    SportoloId INT NOT NULL,
    Eredmeny DECIMAL(6,2) NOT NULL,
    Helyezes INT NOT NULL,
    CONSTRAINT PK_Verseny PRIMARY KEY CLUSTERED (VersenyId),
    CONSTRAINT FK_Verseny_Helyszin FOREIGN KEY (HelyszinId) REFERENCES Helyszin(HelyszinId),
    CONSTRAINT FK_Verseny_Sportolo FOREIGN KEY (SportoloId) REFERENCES Sportolo(SportoloId)
);
