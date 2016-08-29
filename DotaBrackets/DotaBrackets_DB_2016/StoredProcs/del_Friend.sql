/***********************************************************************************************************
Description: Stored Procedure to delete a friends information from friendsList Table
	 
Author: 
	Tyrell Powers-Crane 
Date: 
	8.26.16
Change History:
	
************************************************************************************************************/
CREATE PROCEDURE [dbo].[del_Friend]
	@dota2ID BIGINT,
	@gamerID INT
AS
	BEGIN
		BEGIN TRY

			DELETE FROM FriendsList
			WHERE FriendsList.steamID = @dota2ID AND FriendsList.gamerID = @gamerID

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


