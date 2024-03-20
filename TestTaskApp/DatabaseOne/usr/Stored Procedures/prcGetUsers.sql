CREATE PROCEDURE usr.prcGetUsers
AS
BEGIN
	BEGIN TRY
		SELECT
			a.FirstName,
			a.SecondName,
			a.Surname,
			a.Phone,
			a.Birthday
		FROM
			usr.tUserTable AS a;
	END TRY
	BEGIN CATCH
		IF @@TRANCOUNT>0 ROLLBACK TRANSACTION;
		DECLARE
			@ErrNumber   INT = (SELECT ERROR_NUMBER()),
			@ErrState    INT = (SELECT ERROR_STATE()),
			@ErrProc     VARCHAR(128) = (SELECT ERROR_PROCEDURE()),
			@ErrLine    INT = (SELECT ERROR_LINE()),
			@ErrMessage VARCHAR(MAX) = (SELECT ERROR_MESSAGE());
		EXEC usr.prcLogger @ErrNumber,@ErrState, @ErrProc, @ErrLine, @ErrMessage; 
	END CATCH
END

