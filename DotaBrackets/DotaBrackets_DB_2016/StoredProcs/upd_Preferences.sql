/***********************************************************************************************************
Description: Updates Preference information for a gamer into the Preference Table (update)
	 
Author: 
	Tyrell Powers-Crane 
Date: 
	8.18.2016
Change History:
	
************************************************************************************************************/
CREATE PROCEDURE [dbo].[upd_Preferences]
	@pmmr INT,
	@phasMic INT,
	@plang INT,
	@preferencesID INT

AS
	BEGIN
		BEGIN TRY
					UPDATE Preferences 

					SET mmr = @pmmr,
							hasMic = ISNULL(@phasMic, 1),
							lang = ISNULL(@plang, 1)
							
							
					WHERE Preferences.preferencesID = @preferencesID
			
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
