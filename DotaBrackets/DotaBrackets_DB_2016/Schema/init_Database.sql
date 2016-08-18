/***********************************************************************************************************
Description: Initializes the Database
	 
Author: 
	Tyrell Powers-Crane 
Date: 
	08.15.2016
Change History:
	
************************************************************************************************************/
CREATE TABLE MmrType (
	mmrTypeID INT IDENTITY (1,1) PRIMARY KEY,
	mmrType VARCHAR(25)
	)

CREATE TABLE HasMicType (
	hasMicTypeID INT IDENTITY (1,1) PRIMARY KEY,
	hasMicType VARCHAR(25)
	)

CREATE TABLE LangType (
	langTypeID INT IDENTITY (1,1) PRIMARY KEY,
	langType VARCHAR(25)
	)

CREATE TABLE ServType (
	servTypeID INT IDENTITY (1,1) PRIMARY KEY,
	servType VARCHAR(25)
	)

CREATE TABLE Traits (
	traitsID INT IDENTITY (1,1) PRIMARY KEY,
	mmr INT FOREIGN KEY REFERENCES MmrType(mmrTypeID),
	hasMic INT FOREIGN KEY REFERENCES HasMicType(hasMicTypeID),
	lang INT FOREIGN KEY REFERENCES LangType(langTypeID),
	serv INT FOREIGN KEY REFERENCES ServType(servTypeID)
	)

CREATE TABLE Preferences (
	preferencesID INT IDENTITY (1,1) PRIMARY KEY,
	mmr INT FOREIGN KEY REFERENCES MmrType(mmrTypeID),
	hasMic INT FOREIGN KEY REFERENCES HasMicType(hasMicTypeID),
	lang INT FOREIGN KEY REFERENCES LangType(langTypeID)
	)

CREATE TABLE Gamer (
	gamerID INT IDENTITY (1,1) PRIMARY KEY,
	traitsID INT FOREIGN KEY REFERENCES Traits(traitsID),
	preferencesID INT FOREIGN KEY REFERENCES Preferences(preferencesID),
	userName VARCHAR(MAX),
	access VARCHAR(MAX),
	steamID BIGINT,
	dota2ID INT,
	isSearching BIT,
	avatar VARCHAR(MAX)
	)

CREATE TABLE FriendsList (
	friendsListID INT IDENTITY (1,1) PRIMARY KEY,
	gamerID INT FOREIGN KEY REFERENCES Gamer(gamerID),
	friendsGamerID INT,
	userName VARCHAR(MAX),
	steamID BIGINT
	)

CREATE TABLE ErrorLog (
	errorLog INT IDENTITY (1,1) PRIMARY KEY,
	errorTime datetime,
	errorMessage NVARCHAR(MAX),
	errorProcedure NVARCHAR(MAX)
	)





