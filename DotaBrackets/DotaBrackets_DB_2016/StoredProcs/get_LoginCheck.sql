/***********************************************************************************************************
Description: Check to see if u user exits
	 
Author: 
	Tyrell Powers-Crane 
Date: 
	8.16.2016
Change History:
	
************************************************************************************************************/
CREATE PROCEDURE [dbo].[get_LoginCheck]
	@userName varchar(max),
	@access varchar(max),
	@status int OUTPUT

AS
	BEGIN
		BEGIN TRY

		DECLARE @thisGamerID INT = 0

		SELECT @thisGamerID = (
								SELECT ISNULL(gamerID, 0) FROM Gamer gamer

								WHERE gamer.userName = @userName And gamer.access = @access
							)

		IF (@thisGamerID = 0)
			BEGIN
				SET @status = 1
			END
		ELSE
			BEGIN
				SET @status = 0
			END

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

