CREATE TABLE [dbo].[Medication]
(
	[MedicationId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Name] NVARCHAR(150) NOT NULL UNIQUE, 
    [Quantity] INT NOT NULL, 
    [CreationDate] DATETIME2 NOT NULL DEFAULT GETUTCDATE(), 
    CONSTRAINT [CkMedication_QuantityGreaterThanZero] CHECK (Quantity > 0)
)
