/***********************************************************************************************************
Description: Stored Procedure to pull type information from ServType Table
	 
Author: 
	Tyrell Powers-Crane 
Date: 
	8.17.16
Change History:
	
************************************************************************************************************/
CREATE PROCEDURE [dbo].[get_AllServType]

AS
	BEGIN
		BEGIN TRY

			SELECT ISNULL(servType, '') AS servType,
					ISNULL(servTypeID, 1) AS servTypeID
			FROM ServType

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


