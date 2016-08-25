/***********************************************************************************************************
Description: Stored Procedure to pull friend information from friendsList Table
	 
Author: 
	Tyrell Powers-Crane 
Date: 
	8.25.16
Change History:
	
************************************************************************************************************/
CREATE PROCEDURE [dbo].[get_FriendsList]
	@gamerID INT

AS
	BEGIN
		BEGIN TRY

			SELECT ISNULL(steamID, 0) AS dota2ID,
					ISNULL(userName, '') AS userName

			FROM FriendsList

			WHERE FriendsList.gamerID = @gamerID

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


