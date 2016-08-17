/***********************************************************************************************************
Description: Inserts gamer information for a gamer into the FriendsList Table for the first time (not an update)
	 
Author: 
	Tyrell Powers-Crane 
Date: 
	8.17.2016
Change History:
	
************************************************************************************************************/
CREATE PROCEDURE [dbo].[ins_FriendsList]
	@gamerID INT

AS
	BEGIN
		BEGIN TRY

			IF EXISTS (SELECT * FROM Gamer WHERE 
							gamerID = ISNULL(@gamerID, 1)
						)
				BEGIN
					RETURN 
				END
			ELSE
				BEGIN
					INSERT FriendsList (gamerID
									)

					VALUES (ISNULL(@gamerID, 1)
							)

					RETURN 
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
