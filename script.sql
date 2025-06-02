-- SQL Script c# alkalmazásból --
CREATE DATABASE Vizsgazo3
GO
USE Vizsgazo3
GO

CREATE TABLE Orszag (
OrszagId INT IDENTITY(1,1) NOT NULL,
OrszagKod NVARCHAR(10) NOT NULL
CONSTRAINT PK_Orszag PRIMARY KEY CLUSTERED (OrszagId)
)

CREATE TABLE Helyszin (
HelyszinId INT IDENTITY(1,1) NOT NULL,
HelyszinNev NVARCHAR(15) NOT NULL,
CONSTRAINT PK_Helyszin PRIMARY KEY CLUSTERED (HelyszinId)
)

CREATE TABLE Sportolo (
SportoloId INT IDENTITY(1,1) NOT NULL,
SportoloNev NVARCHAR(50) NOT NULL,
OrszagId INT NOT NULL,
CONSTRAINT PK_Sportolo PRIMARY KEY CLUSTERED (SportoloId),
CONSTRAINT FK_Sportolo_Orszag FOREIGN KEY (OrszagId) REFERENCES Orszag(OrszagId)
)

CREATE TABLE Verseny (
VersenyId INT IDENTITY(1,1) NOT NULL,
Datum DATE NOT NULL,
HelyszinId INT NOT NULL,
SportoloId INT NOT NULL,
Eredmeny DECIMAL(6,2) NOT NULL,
Helyezes INT NOT NULL
CONSTRAINT PK_Verseny PRIMARY KEY CLUSTERED (VersenyId), 
CONSTRAINT FK_Verseny_Helyszin FOREIGN KEY (HelyszinId) REFERENCES Helyszin(HelyszinId), 
CONSTRAINT FK_Verseny_Sportolo FOREIGN KEY (SportoloId) REFERENCES Sportolo(SportoloId)
)
GO

INSERT INTO Orszag (OrszagKod) VALUES (N'URS');
INSERT INTO Orszag (OrszagKod) VALUES (N'BLR');
INSERT INTO Orszag (OrszagKod) VALUES (N'JPN');
INSERT INTO Orszag (OrszagKod) VALUES (N'HUN');
INSERT INTO Orszag (OrszagKod) VALUES (N'POL');
INSERT INTO Orszag (OrszagKod) VALUES (N'RUS');
INSERT INTO Orszag (OrszagKod) VALUES (N'RPG');
INSERT INTO Orszag (OrszagKod) VALUES (N'FIN');
INSERT INTO Orszag (OrszagKod) VALUES (N'DEU');
INSERT INTO Orszag (OrszagKod) VALUES (N'USA');
INSERT INTO Orszag (OrszagKod) VALUES (N'NDK');
INSERT INTO Orszag (OrszagKod) VALUES (N'UKR');
INSERT INTO Orszag (OrszagKod) VALUES (N'SVN');

INSERT INTO Helyszin (HelyszinNev) VALUES (N'Stuttgart');
INSERT INTO Helyszin (HelyszinNev) VALUES (N'Drezda');
INSERT INTO Helyszin (HelyszinNev) VALUES (N'Minszk');
INSERT INTO Helyszin (HelyszinNev) VALUES (N'Prága');
INSERT INTO Helyszin (HelyszinNev) VALUES (N'Sevilla');
INSERT INTO Helyszin (HelyszinNev) VALUES (N'Grodno');
INSERT INTO Helyszin (HelyszinNev) VALUES (N'Lausanne');
INSERT INTO Helyszin (HelyszinNev) VALUES (N'Besztercebánya');
INSERT INTO Helyszin (HelyszinNev) VALUES (N'Szombathely');
INSERT INTO Helyszin (HelyszinNev) VALUES (N'Szczecin');
INSERT INTO Helyszin (HelyszinNev) VALUES (N'Zalaegerszeg');
INSERT INTO Helyszin (HelyszinNev) VALUES (N'Szocsi');
INSERT INTO Helyszin (HelyszinNev) VALUES (N'Adler');
INSERT INTO Helyszin (HelyszinNev) VALUES (N'Athén');
INSERT INTO Helyszin (HelyszinNev) VALUES (N'Edmonton');
INSERT INTO Helyszin (HelyszinNev) VALUES (N'Lahti');
INSERT INTO Helyszin (HelyszinNev) VALUES (N'Frankfurt');
INSERT INTO Helyszin (HelyszinNev) VALUES (N'Saint-Denis');
INSERT INTO Helyszin (HelyszinNev) VALUES (N'Dortmund');
INSERT INTO Helyszin (HelyszinNev) VALUES (N'Eugene');
INSERT INTO Helyszin (HelyszinNev) VALUES (N'Zűrich');
INSERT INTO Helyszin (HelyszinNev) VALUES (N'Kijev');
INSERT INTO Helyszin (HelyszinNev) VALUES (N'Celje');


                                        INSERT INTO Sportolo (SportoloNev, OrszagId)
                                        SELECT N'Yuriy Sedykh', OrszagId FROM Orszag WHERE OrszagKod = N'URS';

                                        INSERT INTO Sportolo (SportoloNev, OrszagId)
                                        SELECT N'Szergej Litvinov', OrszagId FROM Orszag WHERE OrszagKod = N'URS';

                                        INSERT INTO Sportolo (SportoloNev, OrszagId)
                                        SELECT N'Vadim Devyatovskiy', OrszagId FROM Orszag WHERE OrszagKod = N'BLR';

                                        INSERT INTO Sportolo (SportoloNev, OrszagId)
                                        SELECT N'Koji Murofushi', OrszagId FROM Orszag WHERE OrszagKod = N'JPN';

                                        INSERT INTO Sportolo (SportoloNev, OrszagId)
                                        SELECT N'Igor Astapkovich', OrszagId FROM Orszag WHERE OrszagKod = N'BLR';

                                        INSERT INTO Sportolo (SportoloNev, OrszagId)
                                        SELECT N'Ivan Tsikhan', OrszagId FROM Orszag WHERE OrszagKod = N'BLR';

                                        INSERT INTO Sportolo (SportoloNev, OrszagId)
                                        SELECT N'Igor Nikulin', OrszagId FROM Orszag WHERE OrszagKod = N'URS';

                                        INSERT INTO Sportolo (SportoloNev, OrszagId)
                                        SELECT N'Jűri Tamm', OrszagId FROM Orszag WHERE OrszagKod = N'URS';

                                        INSERT INTO Sportolo (SportoloNev, OrszagId)
                                        SELECT N'Adrián Annus', OrszagId FROM Orszag WHERE OrszagKod = N'HUN';

                                        INSERT INTO Sportolo (SportoloNev, OrszagId)
                                        SELECT N'PaweĹ Fajdek', OrszagId FROM Orszag WHERE OrszagKod = N'POL';

                                        INSERT INTO Sportolo (SportoloNev, OrszagId)
                                        SELECT N'Tibor Gécsek', OrszagId FROM Orszag WHERE OrszagKod = N'HUN';

                                        INSERT INTO Sportolo (SportoloNev, OrszagId)
                                        SELECT N'Andrej Abduvalijev', OrszagId FROM Orszag WHERE OrszagKod = N'URS';

                                        INSERT INTO Sportolo (SportoloNev, OrszagId)
                                        SELECT N'Alekszej Zagornyi', OrszagId FROM Orszag WHERE OrszagKod = N'RUS';

                                        INSERT INTO Sportolo (SportoloNev, OrszagId)
                                        SELECT N'Ralf Haber', OrszagId FROM Orszag WHERE OrszagKod = N'RPG';

                                        INSERT INTO Sportolo (SportoloNev, OrszagId)
                                        SELECT N'Szymon Ziolkowski', OrszagId FROM Orszag WHERE OrszagKod = N'POL';

                                        INSERT INTO Sportolo (SportoloNev, OrszagId)
                                        SELECT N'Olli-Pekka Karjalainen', OrszagId FROM Orszag WHERE OrszagKod = N'FIN';

                                        INSERT INTO Sportolo (SportoloNev, OrszagId)
                                        SELECT N'Heinz Weis', OrszagId FROM Orszag WHERE OrszagKod = N'DEU';

                                        INSERT INTO Sportolo (SportoloNev, OrszagId)
                                        SELECT N'Balázs Kiss', OrszagId FROM Orszag WHERE OrszagKod = N'HUN';

                                        INSERT INTO Sportolo (SportoloNev, OrszagId)
                                        SELECT N'Karsten Kobs', OrszagId FROM Orszag WHERE OrszagKod = N'DEU';

                                        INSERT INTO Sportolo (SportoloNev, OrszagId)
                                        SELECT N'Rudy Winkler', OrszagId FROM Orszag WHERE OrszagKod = N'USA';

                                        INSERT INTO Sportolo (SportoloNev, OrszagId)
                                        SELECT N'Krisztián Pars', OrszagId FROM Orszag WHERE OrszagKod = N'HUN';

                                        INSERT INTO Sportolo (SportoloNev, OrszagId)
                                        SELECT N'Gűnther Rodehau', OrszagId FROM Orszag WHERE OrszagKod = N'NDK';

                                        INSERT INTO Sportolo (SportoloNev, OrszagId)
                                        SELECT N'Szergej Kirmaszov', OrszagId FROM Orszag WHERE OrszagKod = N'RUS';

                                        INSERT INTO Sportolo (SportoloNev, OrszagId)
                                        SELECT N'Andriy Skvaruk', OrszagId FROM Orszag WHERE OrszagKod = N'UKR';

                                        INSERT INTO Sportolo (SportoloNev, OrszagId)
                                        SELECT N'PrimoĹž Kozmus', OrszagId FROM Orszag WHERE OrszagKod = N'SVN';


                                         INSERT INTO Verseny (Datum, HelyszinId, SportoloId, Eredmeny, Helyezes)
                                         SELECT '1986-08-30', h.HelyszinId, sp.SportoloId, 86.74, 1
                                         FROM Helyszin h, Sportolo sp
                                         WHERE h.HelyszinNev = N'Stuttgart' AND sp.SportoloNev = N'Yuriy Sedykh';

                                         INSERT INTO Verseny (Datum, HelyszinId, SportoloId, Eredmeny, Helyezes)
                                         SELECT '1986-07-03', h.HelyszinId, sp.SportoloId, 86.04, 2
                                         FROM Helyszin h, Sportolo sp
                                         WHERE h.HelyszinNev = N'Drezda' AND sp.SportoloNev = N'Szergej Litvinov';

                                         INSERT INTO Verseny (Datum, HelyszinId, SportoloId, Eredmeny, Helyezes)
                                         SELECT '2005-07-21', h.HelyszinId, sp.SportoloId, 84.90, 3
                                         FROM Helyszin h, Sportolo sp
                                         WHERE h.HelyszinNev = N'Minszk' AND sp.SportoloNev = N'Vadim Devyatovskiy';

                                         INSERT INTO Verseny (Datum, HelyszinId, SportoloId, Eredmeny, Helyezes)
                                         SELECT '2003-06-29', h.HelyszinId, sp.SportoloId, 84.86, 4
                                         FROM Helyszin h, Sportolo sp
                                         WHERE h.HelyszinNev = N'Prága' AND sp.SportoloNev = N'Koji Murofushi';

                                         INSERT INTO Verseny (Datum, HelyszinId, SportoloId, Eredmeny, Helyezes)
                                         SELECT '1992-06-06', h.HelyszinId, sp.SportoloId, 84.62, 5
                                         FROM Helyszin h, Sportolo sp
                                         WHERE h.HelyszinNev = N'Sevilla' AND sp.SportoloNev = N'Igor Astapkovich';

                                         INSERT INTO Verseny (Datum, HelyszinId, SportoloId, Eredmeny, Helyezes)
                                         SELECT '2008-07-09', h.HelyszinId, sp.SportoloId, 84.51, 6
                                         FROM Helyszin h, Sportolo sp
                                         WHERE h.HelyszinNev = N'Grodno' AND sp.SportoloNev = N'Ivan Tsikhan';

                                         INSERT INTO Verseny (Datum, HelyszinId, SportoloId, Eredmeny, Helyezes)
                                         SELECT '1990-07-12', h.HelyszinId, sp.SportoloId, 84.48, 7
                                         FROM Helyszin h, Sportolo sp
                                         WHERE h.HelyszinNev = N'Lausanne' AND sp.SportoloNev = N'Igor Nikulin';

                                         INSERT INTO Verseny (Datum, HelyszinId, SportoloId, Eredmeny, Helyezes)
                                         SELECT '1984-09-09', h.HelyszinId, sp.SportoloId, 84.40, 8
                                         FROM Helyszin h, Sportolo sp
                                         WHERE h.HelyszinNev = N'Besztercebánya' AND sp.SportoloNev = N'Jűri Tamm';

                                         INSERT INTO Verseny (Datum, HelyszinId, SportoloId, Eredmeny, Helyezes)
                                         SELECT '2003-08-10', h.HelyszinId, sp.SportoloId, 84.19, 9
                                         FROM Helyszin h, Sportolo sp
                                         WHERE h.HelyszinNev = N'Szombathely' AND sp.SportoloNev = N'Adrián Annus';

                                         INSERT INTO Verseny (Datum, HelyszinId, SportoloId, Eredmeny, Helyezes)
                                         SELECT '2015-08-09', h.HelyszinId, sp.SportoloId, 83.93, 10
                                         FROM Helyszin h, Sportolo sp
                                         WHERE h.HelyszinNev = N'Szczecin' AND sp.SportoloNev = N'PaweĹ Fajdek';

                                         INSERT INTO Verseny (Datum, HelyszinId, SportoloId, Eredmeny, Helyezes)
                                         SELECT '1998-09-19', h.HelyszinId, sp.SportoloId, 83.68, 11
                                         FROM Helyszin h, Sportolo sp
                                         WHERE h.HelyszinNev = N'Zalaegerszeg' AND sp.SportoloNev = N'Tibor Gécsek';

                                         INSERT INTO Verseny (Datum, HelyszinId, SportoloId, Eredmeny, Helyezes)
                                         SELECT '1990-05-26', h.HelyszinId, sp.SportoloId, 83.46, 12
                                         FROM Helyszin h, Sportolo sp
                                         WHERE h.HelyszinNev = N'Szocsi' AND sp.SportoloNev = N'Andrej Abduvalijev';

                                         INSERT INTO Verseny (Datum, HelyszinId, SportoloId, Eredmeny, Helyezes)
                                         SELECT '2002-02-10', h.HelyszinId, sp.SportoloId, 83.43, 13
                                         FROM Helyszin h, Sportolo sp
                                         WHERE h.HelyszinNev = N'Adler' AND sp.SportoloNev = N'Alekszej Zagornyi';

                                         INSERT INTO Verseny (Datum, HelyszinId, SportoloId, Eredmeny, Helyezes)
                                         SELECT '1988-05-16', h.HelyszinId, sp.SportoloId, 83.40, 14
                                         FROM Helyszin h, Sportolo sp
                                         WHERE h.HelyszinNev = N'Athén' AND sp.SportoloNev = N'Ralf Haber';

                                         INSERT INTO Verseny (Datum, HelyszinId, SportoloId, Eredmeny, Helyezes)
                                         SELECT '2001-08-05', h.HelyszinId, sp.SportoloId, 83.38, 15
                                         FROM Helyszin h, Sportolo sp
                                         WHERE h.HelyszinNev = N'Edmonton' AND sp.SportoloNev = N'Szymon Ziolkowski';

                                         INSERT INTO Verseny (Datum, HelyszinId, SportoloId, Eredmeny, Helyezes)
                                         SELECT '2004-07-14', h.HelyszinId, sp.SportoloId, 83.30, 16
                                         FROM Helyszin h, Sportolo sp
                                         WHERE h.HelyszinNev = N'Lahti' AND sp.SportoloNev = N'Olli-Pekka Karjalainen';

                                         INSERT INTO Verseny (Datum, HelyszinId, SportoloId, Eredmeny, Helyezes)
                                         SELECT '1997-06-29', h.HelyszinId, sp.SportoloId, 83.04, 17
                                         FROM Helyszin h, Sportolo sp
                                         WHERE h.HelyszinNev = N'Frankfurt' AND sp.SportoloNev = N'Heinz Weis';

                                         INSERT INTO Verseny (Datum, HelyszinId, SportoloId, Eredmeny, Helyezes)
                                         SELECT '1998-06-04', h.HelyszinId, sp.SportoloId, 83.00, 18
                                         FROM Helyszin h, Sportolo sp
                                         WHERE h.HelyszinNev = N'Saint-Denis' AND sp.SportoloNev = N'Balázs Kiss';

                                         INSERT INTO Verseny (Datum, HelyszinId, SportoloId, Eredmeny, Helyezes)
                                         SELECT '1999-06-26', h.HelyszinId, sp.SportoloId, 82.78, 19
                                         FROM Helyszin h, Sportolo sp
                                         WHERE h.HelyszinNev = N'Dortmund' AND sp.SportoloNev = N'Karsten Kobs';

                                         INSERT INTO Verseny (Datum, HelyszinId, SportoloId, Eredmeny, Helyezes)
                                         SELECT '2021-06-20', h.HelyszinId, sp.SportoloId, 82.71, 20
                                         FROM Helyszin h, Sportolo sp
                                         WHERE h.HelyszinNev = N'Eugene' AND sp.SportoloNev = N'Rudy Winkler';

                                         INSERT INTO Verseny (Datum, HelyszinId, SportoloId, Eredmeny, Helyezes)
                                         SELECT '2014-08-16', h.HelyszinId, sp.SportoloId, 82.69, 21
                                         FROM Helyszin h, Sportolo sp
                                         WHERE h.HelyszinNev = N'Zűrich' AND sp.SportoloNev = N'Krisztián Pars';

                                         INSERT INTO Verseny (Datum, HelyszinId, SportoloId, Eredmeny, Helyezes)
                                         SELECT '1985-08-03', h.HelyszinId, sp.SportoloId, 82.64, 22
                                         FROM Helyszin h, Sportolo sp
                                         WHERE h.HelyszinNev = N'Drezda' AND sp.SportoloNev = N'Gűnther Rodehau';

                                         INSERT INTO Verseny (Datum, HelyszinId, SportoloId, Eredmeny, Helyezes)
                                         SELECT '1998-05-30', h.HelyszinId, sp.SportoloId, 82.62, 23
                                         FROM Helyszin h, Sportolo sp
                                         WHERE h.HelyszinNev = N'Zalaegerszeg' AND sp.SportoloNev = N'Szergej Kirmaszov';

                                         INSERT INTO Verseny (Datum, HelyszinId, SportoloId, Eredmeny, Helyezes)
                                         SELECT '2002-04-27', h.HelyszinId, sp.SportoloId, 82.62, 23
                                         FROM Helyszin h, Sportolo sp
                                         WHERE h.HelyszinNev = N'Kijev' AND sp.SportoloNev = N'Andriy Skvaruk';

                                         INSERT INTO Verseny (Datum, HelyszinId, SportoloId, Eredmeny, Helyezes)
                                         SELECT '2009-09-02', h.HelyszinId, sp.SportoloId, 82.58, 25
                                         FROM Helyszin h, Sportolo sp
                                         WHERE h.HelyszinNev = N'Celje' AND sp.SportoloNev = N'PrimoĹž Kozmus';
