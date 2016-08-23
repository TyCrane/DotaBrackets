/***********************************************************************************************************
Description: Updates gamer information for a gamer into the gamer Table (update)
	 
Author: 
	Tyrell Powers-Crane 
Date: 
	8.18.2016
Change History:
	
************************************************************************************************************/
CREATE PROCEDURE [dbo].[upd_Gamer]
	@userName VARCHAR(MAX),
	@access VARCHAR(MAX),
	@gamerID INT 

AS
	BEGIN
		BEGIN TRY
					UPDATE Gamer 

					SET userName = @userName,
						@access = ISNULL(@access, '')
							
					WHERE Gamer.gamerID = @gamerID
			
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
