/***********************************************************************************************************
Description: Initializes the Database
	 
Author: 
	Tyrell Powers-Crane 
Date: 
	08.15.2016
Change History:
	
************************************************************************************************************/
CREATE TABLE Traits (
	traitsID INT IDENTITY (1,1) PRIMARY KEY,
	mmr INT,
	hasMic INT,
	lang INT,
	serv INT
	)

CREATE TABLE Preferences (
	preferencesID INT IDENTITY (1,1) PRIMARY KEY,
	mmr INT,
	hasMic INT,
	lang INT
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

