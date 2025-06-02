-- SQL Script c# alkalmazásból --
USE master
GO
CREATE DATABASE Vizsgazo4
GO
USE Vizsgazo4
GO

CREATE TABLE Helyszin (
Id INT IDENTITY(1,1) NOT NULL,
Nev NVARCHAR(50) NOT NULL
CONSTRAINT PK_Helyszin PRIMARY KEY CLUSTERED (Id)
)

CREATE TABLE Kapcsolat (
Id INT IDENTITY(1,1) NOT NULL,
Nev NVARCHAR(50) NOT NULL,
Telefon NVARCHAR(15) NOT NULL,
Email NVARCHAR(50) NOT NULL,
Cegnev NVARCHAR(50) NOT NULL,
CONSTRAINT PK_Kapcsolat PRIMARY KEY CLUSTERED (Id)
)

CREATE TABLE Tipus (
Id INT IDENTITY(1,1) NOT NULL,
Nev NVARCHAR(50) NOT NULL,
CONSTRAINT PK_Tipus PRIMARY KEY CLUSTERED (Id),
)

CREATE TABLE Rendezveny (
Id INT IDENTITY(1,1) NOT NULL,
KapcsolatId INT NOT NULL,
Idopont DATE NOT NULL,
NapokSzama INT NOT NULL,
HelyszinId INT NOT NULL,
Letszam INT NOT NULL,
TipusId INT NOT NULL
CONSTRAINT PK_Rendezveny PRIMARY KEY CLUSTERED (Id), 
CONSTRAINT FK_Rendezveny_Kapcsolat FOREIGN KEY (KapcsolatId) REFERENCES Kapcsolat(Id), 
CONSTRAINT FK_Rendezveny_Helyszin  FOREIGN KEY (HelyszinId)  REFERENCES Helyszin(Id),
CONSTRAINT FK_Rendezveny_Tipus     FOREIGN KEY (TipusId)     REFERENCES Tipus(Id)
)
GO

SET IDENTITY_INSERT Helyszin ON
INSERT INTO Helyszin (Id, Nev) VALUES(6, N'Balaton')
INSERT INTO Helyszin (Id, Nev) VALUES(2, N'Budapest')
INSERT INTO Helyszin (Id, Nev) VALUES(4, N'Debrecen')
INSERT INTO Helyszin (Id, Nev) VALUES(8, N'Egyéb')
INSERT INTO Helyszin (Id, Nev) VALUES(1, N'Megrendelő telephelye')
INSERT INTO Helyszin (Id, Nev) VALUES(5, N'Pécs')
INSERT INTO Helyszin (Id, Nev) VALUES(3, N'Szeged')
INSERT INTO Helyszin (Id, Nev) VALUES(7, N'Velencei tó')
SET IDENTITY_INSERT Helyszin OFF

SET IDENTITY_INSERT Kapcsolat ON
INSERT INTO Kapcsolat (Id, Nev, Telefon, Email, Cegnev) VALUES(1, N'Kiss Piroska', N'3620123456', N'kiss.piroska@paprika.hu', N'Paprika Paradicsom')
INSERT INTO Kapcsolat (Id, Nev, Telefon, Email, Cegnev) VALUES(2, N'Nagy Béla', N'3670523456', N'nagy.bela@bugfix.hu', N'BugFix IT')
INSERT INTO Kapcsolat (Id, Nev, Telefon, Email, Cegnev) VALUES(3, N'Vass Alajos', N'36309998877', N'vass.alajos@vaskalapos.hu', N'Vaskalapos Hulladékhasznosító')
INSERT INTO Kapcsolat (Id, Nev, Telefon, Email, Cegnev) VALUES(4, N'Nagy Lilla', N'36308768768', N'nagy.lilla@szoke-ciklon.hu', N'Szőke Ciklon Illatszergyár')
INSERT INTO Kapcsolat (Id, Nev, Telefon, Email, Cegnev) VALUES(5, N'Major Anna', N'36201347761', N'major.anna@organic.hu', N'Organic Gyógyszergyár')
INSERT INTO Kapcsolat (Id, Nev, Telefon, Email, Cegnev) VALUES(6, N'Balogh Béla', N'36304673753', N'balogh.bela@nadpalca.hu', N'Nádpálca Oktatástechnikai Kereskelem')
INSERT INTO Kapcsolat (Id, Nev, Telefon, Email, Cegnev) VALUES(7, N'Szabó Krisztina', N'36205049928', N'szabo.krisztina@kaqkk.hu', N'Kaqkk Kft.')
INSERT INTO Kapcsolat (Id, Nev, Telefon, Email, Cegnev) VALUES(8, N'Hanta Balázs', N'36705463728', N'hanta.balazs@hanta.hu', N'Hanta Pályázatíró Kft.')
INSERT INTO Kapcsolat (Id, Nev, Telefon, Email, Cegnev) VALUES(9, N'Mekk Elek', N'36305161721', N'mekk.elek@talan-holnap.hu', N'Talán Holnap Karbantartás')
SET IDENTITY_INSERT Kapcsolat OFF

SET IDENTITY_INSERT Tipus ON
INSERT INTO Tipus (Id, Nev) VALUES(2, N'beltéri')
INSERT INTO Tipus (Id, Nev) VALUES(5, N'céges rendezvény')
INSERT INTO Tipus (Id, Nev) VALUES(3, N'online')
INSERT INTO Tipus (Id, Nev) VALUES(1, N'szabadtéri')
INSERT INTO Tipus (Id, Nev) VALUES(4, N'tréning')
SET IDENTITY_INSERT Tipus OFF

SET IDENTITY_INSERT Rendezveny ON
INSERT INTO Rendezveny (Id, KapcsolatId, Idopont, NapokSzama, HelyszinId, Letszam, TipusId) VALUES(1, 5, '2024-12-01', 3, 3, 100, 4)
INSERT INTO Rendezveny (Id, KapcsolatId, Idopont, NapokSzama, HelyszinId, Letszam, TipusId) VALUES(2, 3, '2024-12-01', 1, 1, 120, 5)
INSERT INTO Rendezveny (Id, KapcsolatId, Idopont, NapokSzama, HelyszinId, Letszam, TipusId) VALUES(3, 1, '2024-12-06', 1, 1, 40, 5)
INSERT INTO Rendezveny (Id, KapcsolatId, Idopont, NapokSzama, HelyszinId, Letszam, TipusId) VALUES(4, 7, '2024-12-07', 3, 3, 20, 4)
INSERT INTO Rendezveny (Id, KapcsolatId, Idopont, NapokSzama, HelyszinId, Letszam, TipusId) VALUES(5, 4, '2024-12-08', 1, 2, 70, 2)
INSERT INTO Rendezveny (Id, KapcsolatId, Idopont, NapokSzama, HelyszinId, Letszam, TipusId) VALUES(6, 6, '2024-12-09', 1, 1, 30, 3)
INSERT INTO Rendezveny (Id, KapcsolatId, Idopont, NapokSzama, HelyszinId, Letszam, TipusId) VALUES(7, 8, '2024-12-11', 3, 4, 10, 4)
INSERT INTO Rendezveny (Id, KapcsolatId, Idopont, NapokSzama, HelyszinId, Letszam, TipusId) VALUES(8, 9, '2024-12-12', 1, 1, 30, 2)
INSERT INTO Rendezveny (Id, KapcsolatId, Idopont, NapokSzama, HelyszinId, Letszam, TipusId) VALUES(9, 2, '2024-12-13', 5, 5, 30, 4)
INSERT INTO Rendezveny (Id, KapcsolatId, Idopont, NapokSzama, HelyszinId, Letszam, TipusId) VALUES(10, 5, '2024-12-14', 1, 2, 230, 5)
INSERT INTO Rendezveny (Id, KapcsolatId, Idopont, NapokSzama, HelyszinId, Letszam, TipusId) VALUES(11, 7, '2024-12-15', 1, 1, 65, 5)
INSERT INTO Rendezveny (Id, KapcsolatId, Idopont, NapokSzama, HelyszinId, Letszam, TipusId) VALUES(12, 8, '2024-12-15', 1, 2, 40, 5)
SET IDENTITY_INSERT Rendezveny OFF
