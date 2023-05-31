CREATE TRIGGER [dbo].[tgMedicationInsertOrUpdateCreationDate]
	ON Medication
	FOR INSERT, UPDATE
	AS
	BEGIN
		SET NOCOUNT ON;

		DECLARE @current_medication_id INT;
		DECLARE @current_medication_creation_date DATETIME2(7);

		-- If an update occurred, keep CreationDate intact
		IF EXISTS(SELECT * FROM deleted)
		BEGIN
			DECLARE old_medications_cursor CURSOR FOR SELECT MedicationId, CreationDate FROM deleted; 
			OPEN old_medications_cursor;

			FETCH NEXT FROM old_medications_cursor INTO @current_medication_id, @current_medication_creation_date;
			WHILE @@FETCH_STATUS = 0
			BEGIN
				-- Keep creation date value prior to update, to prevent it form being modified
				UPDATE Medication SET CreationDate = @current_medication_creation_date WHERE MedicationId = @current_medication_id;

				FETCH NEXT FROM old_medications_cursor INTO @current_medication_id, @current_medication_creation_date;
			END; 

			CLOSE old_medications_cursor;
			DEALLOCATE old_medications_cursor;
		END;
		-- If a new Medication was created, set CreationDate to current date
		ELSE
		BEGIN
			DECLARE new_medications_cursor CURSOR FOR SELECT MedicationId FROM inserted; 
			OPEN new_medications_cursor;

			FETCH NEXT FROM new_medications_cursor INTO @current_medication_id;
			WHILE @@FETCH_STATUS = 0
			BEGIN
				-- Set creation date
				UPDATE Medication SET CreationDate = GETUTCDATE() WHERE MedicationId = @current_medication_id;

				FETCH NEXT FROM new_medications_cursor INTO @current_medication_id;
			END; 

			CLOSE new_medications_cursor;
			DEALLOCATE new_medications_cursor;
		END;
	END;
