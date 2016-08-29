/***********************************************************************************************************
Description: Updates trait information for a gamer into the traits Table (update)
	 
Author: 
	Tyrell Powers-Crane 
Date: 
	8.18.2016
Change History:
	
************************************************************************************************************/
CREATE PROCEDURE [dbo].[upd_Traits]
	@tlang INT,
	@thasMic INT,
	@tserv INT,
	@tmmr INT,
	@traitsID INT 

AS
	BEGIN
		BEGIN TRY
					UPDATE Traits 

					SET mmr = @tmmr,
							hasMic = ISNULL(@thasMic, 1),
							lang = ISNULL(@tlang, 1),
							serv = ISNULL(@tserv, 1)
							
					WHERE Traits.traitsID = @traitsID

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
