DROP TABLE Ships;

WAITFOR DELAY '00:00:01' --One seconds

CREATE TABLE Ships (
    ID uniqueIdentifier NOT NULL PRIMARY KEY,

    Nation varchar(255),
    ClassName varchar(255),

    ClassType varchar(255),
    NumberInClass int,
    SpeedKnots float,
    LengthFt int,
    BeamFt int,
    StandardWeight int,
    FullWeight int,
    Guns int,
    GunDiameter float,
    Armor float,
    Rudders int,
    RudderType varchar(255),
    RudderStyle varchar(255),
    Shafts int,
    Launched int,
    LastYearBuilt int,

    ShipClass int,
    Units float,
    SpeedIrcwcc int,

    ShiplistKey int,
    Comment varchar(2000),
    WikiLink varchar(255),
    Notes varchar(255)
);
