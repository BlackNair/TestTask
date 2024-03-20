CREATE PROCEDURE usr.prcInsertUsers
(
	@FirstName VARCHAR(128),
	@SecondName VARCHAR(256),
	@Surname VARCHAR(128),
	@Phone VARCHAR(32),
	@Birthday DATETIME2(0)
)
AS
BEGIN
	BEGIN TRY
		INSERT INTO usr.tUserTable
		(
			FirstName,
			SecondName,
			Surname,
			Birthday,
			Phone
		)
		VALUES (@FirstName, @SecondName, @Surname, @Birthday, @Phone);
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
