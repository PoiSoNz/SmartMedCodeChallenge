using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartMedCodeChallenge.Models
{
    /// <summary>
    /// Each medication must have a name, a quantity and a creation date.
    /// </summary>
    [Table("Medication", Schema = "dbo")]
    public class MedicationToAdd
    {
        //public int? Id { get; set; }

        [Key]
        public string Name { get; set; }

        public int Quantity { get; set; }
    }
}