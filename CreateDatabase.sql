/*
	Soprt adatb�zis l�trehoz�sa Database First technol�gi�val 
	
	- L�pj be a Microsoft SQL Server Management studi�ba
		sa / 1A2w3e4F

	- �rd a scriptet egy �j Query ablakba

   	- Futtasd le a scriptet F5 billenty�vel, vagy az Execute parancsgombbal.
*/


-- Adatb�zis l�trehoz�sa
CREATE DATABASE Vizsgazo2; -- Adatb�zis l�trehoz�sa
GO -- Nagyon fontos ezzel hajtja v�gre val�ban a parancsot

USE Vizsgazo2; -- Adatb�zis haszn�latba v�tele
GO

-- Orsz�g t�bla
CREATE TABLE Orszag (
    OrszagId INT IDENTITY(1,1) NOT NULL, -- Egyedi azonos�t�
    OrszagKod NVARCHAR(10) NOT NULL UNIQUE, -- Nem ism�tl�dhet
    CONSTRAINT PK_Orszag PRIMARY KEY CLUSTERED (OrszagId) -- Clustere index a kulcsra
);

-- Helysz�n t�bla
CREATE TABLE Helyszin (
    HelyszinId INT IDENTITY(1,1) NOT NULL,
    HelyszinNev NVARCHAR(100) NOT NULL UNIQUE,
    CONSTRAINT PK_Helyszin PRIMARY KEY CLUSTERED (HelyszinId)
);

-- Sportol� t�bla
CREATE TABLE Sportolo (
    SportoloId INT IDENTITY(1,1) NOT NULL,
    SportoloNev NVARCHAR(100) NOT NULL,
    OrszagId INT NOT NULL,
    CONSTRAINT PK_Sportolo PRIMARY KEY CLUSTERED (SportoloId),  -- Kapcsolatok 
    CONSTRAINT FK_Sportolo_Orszag FOREIGN KEY (OrszagId) REFERENCES Orszag(OrszagId)
);

-- Verseny t�bla
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
