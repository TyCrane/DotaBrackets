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
	hasMic BIT,
	lang INT,
	serv INT
	)

CREATE TABLE Preferences (
	preferencesID INT IDENTITY (1,1) PRIMARY KEY,
	mmr INT,
	hasMic BIT,
	lang VARCHAR(25),
	serv INT
	)

CREATE TABLE Gamer (
	gamerID INT IDENTITY (1,1) PRIMARY KEY,
	traitsID INT FOREIGN KEY REFERENCES Traits(traitsID),
	preferencesID INT FOREIGN KEY REFERENCES Preferences(preferencesID),
	userName VARCHAR(25),
	access VARCHAR(25),
	steamID INT,
	dota2ID INT,
	isSearching BIT,
	avatar VARCHAR(MAX)
	)

CREATE TABLE FriendsList (
	friendsListID INT IDENTITY (1,1) PRIMARY KEY,
	gamerID INT FOREIGN KEY REFERENCES Gamer(gamerID),
	friendsGamerID INT
	)