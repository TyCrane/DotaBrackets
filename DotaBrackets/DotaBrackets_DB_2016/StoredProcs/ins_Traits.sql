/***********************************************************************************************************
Description: Inserts trait information for a gamer into the traits Table for the first time (not an update)
	 
Author: 
	Tyrell Powers-Crane 
Date: 
	8.16.2016
Change History:
	
************************************************************************************************************/
CREATE PROCEDURE [dbo].[ins_Traits]
	@tlang INT,
	@thasMic INT,
	@tserv INT,
	@tmmr INT,
	@traitsID INT OUTPUT

AS
	BEGIN
		BEGIN TRY

			IF EXISTS (SELECT * FROM Traits WHERE 
							mmr = ISNULL(@tmmr, 1)
						AND hasMic = ISNULL(@thasMic, 1)
						AND lang = ISNULL(@tlang, 1)
						AND serv = ISNULL(@tserv, 1)
						)

				BEGIN
					SET @traitsID = (
							SELECT traitsID FROM Traits
			
							WHERE	mmr = ISNULL(@tmmr, 1)
								AND hasMic = ISNULL(@thasMic, 1)
								AND lang = ISNULL(@tlang, 1)
								AND serv = ISNULL(@tserv, 1)
									)
					RETURN @traitsID
				END
			ELSE
				BEGIN
					INSERT Traits (mmr, 
									hasMic, 
									lang, 
									serv
									)

					VALUES (ISNULL(@tmmr, 1),
							ISNULL(@thasMic, 1),
							ISNULL(@tlang, 1),
							ISNULL(@tserv, 1)
							)

					SET @traitsID = (
							SELECT traitsID FROM Traits
			
							WHERE	mmr = ISNULL(@tmmr, 1)
								AND hasMic = ISNULL(@thasMic, 1)
								AND lang = ISNULL(@tlang, 1)
								AND serv = ISNULL(@tserv, 1)
									)
					RETURN @traitsID 
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
