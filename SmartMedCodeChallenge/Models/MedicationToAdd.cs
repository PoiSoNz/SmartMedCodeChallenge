using System.ComponentModel.DataAnnotations;

namespace SmartMedCodeChallenge.Models
{
    /// <summary>
    /// A medication to be added just needs name and quantity. No creation date is needed.
    /// </summary>
    public class MedicationToAdd
    {
        [Key]
        public string Name { get; set; }

        public int Quantity { get; set; }
    }
}