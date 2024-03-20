CREATE PROCEDURE usr.prcLogger
(
	@ErrNumber INT, 
	@ErrState INT,  
	@ErrProc VARCHAR(128),   
	@ErrLine INT,   
	@ErrMessage VARCHAR(MAX)
)
AS
BEGIN
	INSERT INTO usr.tErrorLog
	(
		ErrNumber,
		ErrState,
		ErrProc,
		ErrLine,
		ErrMessage
	)
	VALUES
	(@ErrNumber, @ErrState, @ErrProc, @ErrLine, @ErrMessage);  
END


