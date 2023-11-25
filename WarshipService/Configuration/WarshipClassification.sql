DROP TABLE WarshipClassification;

WAITFOR DELAY '00:00:01' --One seconds


CREATE TABLE WarshipClassification (

    ID varchar(255) NOT NULL PRIMARY KEY,
    Family varchar(255),
    Abbreviation varchar(255),
    DisplayName varchar(255),
    ClassRank int,
    Aliases varchar(255),
);

WAITFOR DELAY '00:00:01' --One seconds

INSERT INTO WarshipClassification ( ID ,  Family ,    Abbreviation ,    DisplayName ,  ClassRank,  Aliases )
VALUES 
('HeavyCruiser', 'Cruiser', 'CA', 'Heavy Cruiser',1, '["ca"]'),
('LightCruiser', 'Cruiser', 'CL', 'Light Cruiser',1, '["cl"]'),
('ArmoredCruiser', 'Cruiser', 'CAE', 'Armored Cruiser',2, '["cae"]'),
('ProtectedCruiser', 'Cruiser', 'CP', 'Protected Cruiser',2, '["cp"]'),

('Destroyer', 'Destroyer', 'DD', 'Destroyer', 1, '["dd"]'),
('Submarine', 'Submarine', 'SS', 'Submarine', 1, '["ss"]'),

('CoastalDefense', 'CoastalDefense', 'CDS', 'Coastal Defense', 1, '["cds"]'),
('Monitor', 'Monitor', 'MN', 'Monitor', 1, '["mn"]'),

('Predreadnought', 'Battleship', 'BB', 'Predreadnought Battleship', 1, '["semidreadnought","pdn"]'),
('SuperDreadnought', 'Battleship', 'BB', 'Super Dreadnought Battleship', 1, ''),
('Dreadnought', 'Battleship', 'BB', 'Dreadnought Battleship', 2, '["dn"]'),
('Battleship', 'Battleship', 'BB', 'Battleship', 3, '["bb"]'),

('Battlecruiser', 'Battlecruiser', 'BC', 'Battlecruiser', 1, '["bc"]'),

('Convoy', 'Convoy', 'CON', 'Convoy', 2, '["con"]'),
('Ironclad', 'Ironclad', '', 'Ironclad', 2, ''),

('EscortCarrier', 'Carrier', 'CVE', 'Escort Carrier', 1, '["cve"]'),
('LightCarrier', 'Carrier', 'CVL', 'Light Carrier', 1, '["cvl"]'),
('Carrier', 'Carrier', 'CV', 'Carrier', 2, '["cv"]')


	
