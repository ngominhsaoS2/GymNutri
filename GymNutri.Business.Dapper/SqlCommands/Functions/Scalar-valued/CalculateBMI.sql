-- =============================================
-- Author:			SaoNM
-- Create date:		22/02/2019
-- Description:		Tính chỉ số BMI
-- =============================================
ALTER FUNCTION CalculateBMI 
(
	@HeightCm float = 1, -- Chiều cao, đơn vị tính cm
	@WeightKg float = 0 -- Cân nặng, đơn vị tính kg
)
RETURNS float
AS
BEGIN
	DECLARE @BMI float = 0;

	IF(@HeightCm <= 0)
		SET @BMI = 0;
	ELSE
		SET @BMI = @WeightKg / (@HeightCm * @HeightCm / 10000);

	RETURN ROUND(@BMI, 2);
END
GO

SELECT [dbo].[CalculateBMI] (172, 65)
GO