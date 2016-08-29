/***********************************************************************************************************
Description: Inserts gamer information for a gamer into the gamer Table for the first time (not an update)
	 
Author: 
	Tyrell Powers-Crane 
Date: 
	8.16.2016
Change History:
	
************************************************************************************************************/
CREATE PROCEDURE [dbo].[ins_Gamer]
	@traitsID INT,
	@preferencesID INT,
	@userName VARCHAR(MAX),
	@access VARCHAR(MAX),
	@steamID BIGINT,
	@dota2ID INT,
	@avatar VARCHAR(MAX),
	@gamerID INT OUTPUT

AS
	BEGIN
		BEGIN TRY

			IF EXISTS (SELECT * FROM Gamer WHERE 
							userName = @userName
						AND access = @access
						)

				BEGIN
					SET @gamerID = (
							SELECT gamerID FROM Gamer
			
							WHERE	userName = @userName
								AND access = @access
								)

					RETURN @gamerID
				END
			ELSE
				BEGIN
					INSERT Gamer (traitsID, 
									preferencesID, 
									userName,
									access,
									steamID,
									dota2ID,
									isSearching,
									avatar
									)

					VALUES (ISNULL(@traitsID, 1),
							ISNULL(@preferencesID, 1),
							ISNULL(@userName, ''),
							ISNULL(@access, ''),
							ISNULL(@steamID, 1),
							ISNULL(@dota2ID, 1),
							0,
							ISNULL(@avatar, '')
							)

					SET @gamerID = (
							SELECT gamerID FROM Gamer
			
							WHERE	userName = @userName
								AND access = @access
								)
					RETURN @gamerID
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
