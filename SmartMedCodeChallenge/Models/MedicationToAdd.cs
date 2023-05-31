using System.ComponentModel.DataAnnotations;

namespace SmartMedCodeChallenge.Models
{
    /// <summary>
    /// A medication that to be added just needs a name and a quantity. No creation date is needed.
    /// </summary>
    public class MedicationToAdd
    {
        [Key]
        public string Name { get; set; }

        public int Quantity { get; set; }
    }
}