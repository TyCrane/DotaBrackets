/***********************************************************************************************************
Description: Inserts Preference information for a gamer into the Preference Table for the first time (not an update)
	 
Author: 
	Tyrell Powers-Crane 
Date: 
	8.16.2016
Change History:
	
************************************************************************************************************/
CREATE PROCEDURE [dbo].[ins_Preferences]
	@pmmr INT,
	@phasMic INT,
	@plang INT,
	@preferencesID INT OUTPUT

AS
	BEGIN
		BEGIN TRY

				BEGIN
					INSERT Preferences (mmr, 
									hasMic, 
									lang
									)

					VALUES (ISNULL(@pmmr, 1),
							ISNULL(@phasMic, 1),
							ISNULL(@plang, 1)
							)

					SET @preferencesID = (
							SELECT TOP (1) preferencesID FROM Preferences
			
							WHERE	mmr = ISNULL(@pmmr, 1)
								AND hasMic = ISNULL(@phasMic, 1)
								AND lang = ISNULL(@plang, 1)
									)
					RETURN @preferencesID
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
			RETURN 0
		END CATCH
	END
