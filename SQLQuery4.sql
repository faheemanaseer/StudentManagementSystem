CREATE PROCEDURE SearchStudentsByName
    @Name NVARCHAR(100) = NULL
AS
BEGIN
    IF @Name IS NULL OR LTRIM(RTRIM(@Name)) = ''
    BEGIN
        SELECT UId, Name, Email, Phone, Age, UserId
        FROM Students
    END
    ELSE
    BEGIN
        SELECT UId, Name, Email, Phone, Age, UserId
        FROM Students
        WHERE Name LIKE '%' + @Name + '%'
    END
END
