/***********************************************************************************************************
Description: Stored Procedure to pull stat information from pref and traits tables
	 
Author: 
	Tyrell Powers-Crane 
Date: 
	8.26.16
Change History:
	
************************************************************************************************************/
CREATE PROCEDURE [dbo].[get_Stats]

AS
	BEGIN
		BEGIN TRY

		--mmr traits
			SELECT mmr
			FROM Traits

		--mmr preferences
			SELECT mmr
			FROM Preferences

		--hasMic traits
			SELECT hasMic
			FROM Traits

		--hasMic preferences
			SELECT hasMic
			FROM Preferences

		--language traits
			SELECT lang
			FROM Traits

		--language preferences
			SELECT lang
			FROM Preferences

		--server traits
			SELECT serv
			FROM Traits
			
		--Amount of users
			SELECT gamerID
			FROM Gamer

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


