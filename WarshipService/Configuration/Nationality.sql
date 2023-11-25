DROP TABLE Nationality;

WAITFOR DELAY '00:00:01' --One second


CREATE TABLE Nationality (

    ID varchar(255) NOT NULL PRIMARY KEY,
    DisplayName varchar(255) NOT NULL,
    Tier int NOT NULL,
    Aliases varchar(255),
);

WAITFOR DELAY '00:00:01' --One second

INSERT INTO Nationality ( ID , DisplayName, Tier, Aliases )
VALUES 
('UnitedStates', 'United States', 1, '["USN", "United States Navy"]'),
('Germany', 'Germany', 2, '["Kriegsmarine","Imperial German Navy"]'),
('Japan', 'Japan', 3, '["Imperial Japanese Navy"]'),
('France', 'France', 4, '["French Navy","French","Free French Naval Forces"]'),
('UnitedKingdom', 'United Kingdom', 5, '["Royal Navy"]'),
('Italy', 'Italy', 6, '["Regina Marina", "Italian"]'),
('Russia', 'Russia', 7, '["Soviet Navy","Imperial Russian Navy"]'),
('Chile', 'Chile', 8, '["Chilean Navy"]'),
('Spain', 'Spain', 9, '["Spanish Navy"]'),
('AustroHungarian', 'Austro-Hungarian', 10, '["Austro-Hungarian Navy","Austria-Hungary","Ottoman Navy","Ottoman"]'),
('Ottoman', 'Ottoman', 11, ''),
('Brazil', 'Brazil', 12, '["Brazilian Navy"]'),
('Argentina', 'Argentina', 13, '["Argentine Navy","Argentine"]'),
('Netherlands', 'Netherlands', 14, ''),
('Greece', 'Greece', 15, ''),
('Portugal', 'Portugal', 16, ''),
('Peru', 'Peru', 17, ''),
('Columbia', 'Columbia', 18, ''),
('Yugoslavia', 'Yugoslavia', 19, ''),
('China', 'China', 20, ''),
('Sweden', 'Sweden', 21, ''),
('Turkey', 'Turkey', 22, ''),
('Poland', 'Poland', 23, ''),
('Denmark', 'Denmark', 24, ''),
('Romania', 'Romania', 25, ''),
('Finland', 'Finland', 26, '')

