/***********************************************************************************************************
Description: Inserts gamer friends information for a gamer into the friends Table 
	 
Author: 
	Tyrell Powers-Crane 
Date: 
	8.25.2016
Change History:
	
************************************************************************************************************/
CREATE PROCEDURE [dbo].[upd_FriendsList]
	@friendID INT,
	@dota2ID BIGINT,
	@userName VARCHAR(MAX),
	@success INT OUTPUT
	

AS
	BEGIN
		BEGIN TRY

			IF EXISTS (SELECT gamerID FROM FriendsList WHERE 
							userName = @userName
						AND steamID = @dota2ID
						AND gamerID = @friendID
						)

				BEGIN
					SET @success = 0

					RETURN @success
				END
			ELSE
				BEGIN
					INSERT FriendsList (userName, steamID, gamerID)
					
					VALUES (@userName, @dota2ID, @friendID)

					SET @success = 1

					RETURN @success
				END

		END TRY
		BEGIN CATCH

			DECLARE @timeStamp DATETIME,
				@errorMessage VARCHAR(255),
				@errorProcedure VARCHAR(100)	

			SELECT @timeStamp = GETDATE(),
					@errorMessage = ERROR_MESSAGE(),
					@errorProcedure = ERROR_PROCEDURE()
			
			RETURN 0
			EXECUTE dbo.log_ErrorTimeStamp @timeStamp, @errorMessage, @errorProcedure

		END CATCH
	END

