/***********************************************************************************************************
Description: Stored Procedure that logs error information into Errorlog Table
	 
Author: 
	Tyrell Powers-Crane 
Date: 
	7.17.16
Change History:
	
************************************************************************************************************/
CREATE PROCEDURE [dbo].[log_ErrorTimeStamp]
	@errorTime datetime,
	@errorMessage varchar(255),
	@errorProcedure varchar (100)
	

AS
	BEGIN

			INSERT ErrorLog (errorTime, errorMessage, errorProcedure)
			VALUES (@errorTime, @errorMessage, @errorProcedure)

	END