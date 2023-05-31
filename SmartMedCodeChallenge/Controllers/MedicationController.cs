using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SmartMedCodeChallenge.Data;
using SmartMedCodeChallenge.Models;

namespace SmartMedCodeChallenge.Controllers
{
    [ApiController]
    public class MedicationController : ControllerBase
    {
        private readonly MedicationDbContext _dbContext;

        public MedicationController(MedicationDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        /// <summary>
        /// Fetches all medications in the database.
        /// If no medication exists, 'There are no medications' message is returned.
        /// </summary>
        [HttpGet]
        [Route("medications")]
        public async Task<IActionResult> GetAllMedications()
        {
            List<Medication>? medications = await this._dbContext.Medications.ToListAsync();
            if (medications.IsNullOrEmpty())
            {
                return Ok("There are no medications.");
            } 
            else
            {
                return Ok(medications);
            }
        }

        /// <summary>
        /// Adds a new medication to the database and returns it.
        /// </summary>
        [HttpPost]
        [Route("medication")]
        public async Task<IActionResult> AddMedication(MedicationToAdd medicationToAdd)
        {
            Medication newMedication = new Medication()
            {
                Name = medicationToAdd.Name,
                Quantity = medicationToAdd.Quantity
            };

            await this._dbContext.Medications.AddAsync(newMedication);
            await this._dbContext.SaveChangesAsync();

            // Return the medication that has just been created
            return Ok(newMedication);
        }

        /// <summary>
        /// Deletes medication with given name from the database.
        /// If there's no medication with given name, throw an error.
        /// </summary>
        [HttpDelete]
        [Route("medication/{name}")]
        public async Task<IActionResult> DeleteMedication([FromRoute] string name)
        {
            // Throw error if no medication exists with given name
            try 
            {
                Medication medication = await this._dbContext.Medications.SingleAsync(medication => medication.Name == name);

                this._dbContext.Remove(medication);
                await this._dbContext.SaveChangesAsync();

                return Ok($"'{name}' medication has been sucessfully deleted");
            }
            catch (InvalidOperationException) 
            {
                return Ok($"No medication exists with name '{name}'.");
            }
        }
    }
}