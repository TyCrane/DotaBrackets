/***********************************************************************************************************
Description: Retrieves all information for a gamer after they login
	 
Author: 
	Tyrell Powers-Crane 
Date: 
	8.16.2016
Change History:
	
************************************************************************************************************/
CREATE PROCEDURE [dbo].[get_Login]
	@userName varchar(max),
	@access varchar(max)

AS
	BEGIN
		BEGIN TRY

		DECLARE @thisGamerID INT

		SELECT	ISNULL(gamer.gamerID, 0) AS gamerID,
				ISNULL(gamer.traitsID, 0) AS traitsID,
				ISNULL(gamer.preferencesID, 0) AS preferencesID,
				ISNULL(gamer.userName, '') AS userName,
				ISNULL(gamer.steamID, 0) AS steamID,
				ISNULL(gamer.dota2ID, 0) AS dota2ID,
				ISNULL(gamer.isSearching, 0) AS isSearching,
				ISNULL(gamer.avatar, '') AS avatar,
				ISNULL(traits.mmr, 0) AS tmmr,
				ISNULL(traits.hasMic, 0) AS thasMic,
				ISNULL(traits.lang, 0) AS tlang,
				ISNULL(traits.serv, 0) AS tserv,
				ISNULL(pref.mmr, 0) AS pmmr,
				ISNULL(pref.hasMic, 0) AS phasMic,
				ISNULL(pref.lang, 0) AS plang
			

			FROM Gamer gamer 
				LEFT JOIN Traits traits ON
					gamer.traitsID = traits.traitsID
				LEFT JOIN Preferences pref ON
					gamer.preferencesID = pref.preferencesID
				
			WHERE gamer.userName = @userName And gamer.access = @access


		SET @thisGamerID = (
								SELECT gamerID FROM Gamer gamer

								WHERE gamer.userName = @userName And gamer.access = @access
							)

			SELECT	ISNULL(frend.friendsGamerID, 0) AS friendID,
					ISNULL(frend.userName, '') AS friendUserName,
					ISNULL(frend.steamID, 0) AS friendSteamID

			FROM FriendsList frend

			WHERE frend.gamerID = @thisGamerID


		END TRY
		BEGIN CATCH

			DECLARE @timeStamp DATETIME,
				@errorMessage VARCHAR(255),
				@errorProcedure VARCHAR(100)	

			SELECT @timeStamp = GETDATE(),
					@errorMessage = ERROR_MESSAGE(),
					@errorProcedure = ERROR_PROCEDURE()
			
			EXECUTE dbo.log_ErrorTimeStamp @timeStamp, @errorMessage, @errorProcedure

		END CATCH
	END

