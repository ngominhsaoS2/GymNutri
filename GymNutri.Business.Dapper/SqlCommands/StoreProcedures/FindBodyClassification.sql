-- =============================================
-- Author:			SaoNM
-- Create date:		26/02/2019
-- Description:		Tìm BodyClassification trong nhóm BMI khi biết chỉ số BMI
-- =============================================
CREATE PROC [dbo].[FindBodyClassification]
	@userBodyIndexId INT,
	@bodyClassificationId INT,
	@correct BIT OUTPUT
AS
BEGIN
	DECLARE @sqlCommand NVARCHAR(4000), @criterion NVARCHAR(4000), @count int;

	-- Lấy ra Criterion theo BodyClassificationId
	SELECT @criterion = Criterion
	FROM BodyClassifications
	WHERE Id = @bodyClassificationId;

	SET @sqlCommand = 'SELECT @count = COUNT(Id) FROM UserBodyIndexes WHERE Id = ' + CAST(@userBodyIndexId AS NVARCHAR(8)) + ' ';
	SET @sqlCommand += 'AND ' + @criterion;
	PRINT(@sqlCommand);

	EXEC sp_executesql @sqlCommand, N'@count int out', @count out;

	IF (@count) > 0
		SET @correct = 1;
	ELSE
		SET @correct = 0;

	--PRINT(@correct);
END
GO

EXEC [dbo].[FindBodyClassification] @userBodyIndexId = 1, @bodyClassificationId = 9, @correct = 0
GO