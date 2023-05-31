using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartMedCodeChallenge.Controllers;
using SmartMedCodeChallenge.Data;
using SmartMedCodeChallenge.Models;

namespace MedicationTests
{
    public class MedicationControllerTest
    {
        private readonly MedicationDbContext _dbContext;
        private readonly MedicationController _controller;

        public MedicationControllerTest()
        {
            // Create dbContext
            DbContextOptions options = new DbContextOptionsBuilder<MedicationDbContext>()
            .UseSqlServer("Server=localhost;Database=MedicationDB;Trusted_Connection=true;TrustServerCertificate=True")
            .Options;
            this._dbContext = new MedicationDbContext(options);

            // Create controller
            this._controller = new MedicationController(this._dbContext);
        }

        /// <summary>
        /// Creates new medication and validates if response contains new medication and if dbContext returns it form database.
        /// </summary>
        [Fact]
        public async void AddMedication_ShouldCreateNewMedication()
        {
            // Create medication with random name to be added
            MedicationToAdd medicationToAdd = new MedicationToAdd()
            {
                Name = $"Test Medication {Guid.NewGuid()}",
                Quantity = new Random().Next(1, 1000)
            };
            
            // Add medication through controller
            var response = await this._controller.AddMedication(medicationToAdd);

            // Make sure response is successful
            Assert.IsType<OkObjectResult>(response);

            Assert.NotNull(((OkObjectResult)response).Value);

            // Make sure returned medication has correct data
            Medication responseMedication = (Medication) ((OkObjectResult)response).Value;
            Assert.Equal(medicationToAdd.Name, responseMedication.Name);
            Assert.Equal(medicationToAdd.Quantity, responseMedication.Quantity);
            Assert.NotNull(responseMedication.CreationDate);
            Assert.Contains(DateTime.UtcNow.ToString("dd/MM/yyyy"), responseMedication.CreationDate?.ToString("dd/MM/yyyy"));

            // Check if dbContext lists new medication
            try
            {
                Medication DbContextAddedMedication = await this._dbContext.Medications.SingleAsync(medication => medication.Name == medicationToAdd.Name);
                
                Assert.Equal(medicationToAdd.Name, DbContextAddedMedication.Name);
                Assert.Equal(medicationToAdd.Quantity, DbContextAddedMedication.Quantity);
                Assert.Equal(responseMedication.CreationDate.ToString(), DbContextAddedMedication.CreationDate.ToString());
            }
            catch (InvalidOperationException)
            {
                Assert.Fail($"DbContext couldn't find '{medicationToAdd.Name}' medication.");
            }
        }
    }
}