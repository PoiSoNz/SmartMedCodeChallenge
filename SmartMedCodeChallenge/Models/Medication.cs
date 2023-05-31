using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartMedCodeChallenge.Models
{
    /// <summary>
    /// Each medication must have a name, a quantity and a creation date.
    /// </summary>
    [Table("Medication", Schema = "dbo")]
    public class Medication
    {
        [Key]
        public string Name { get; set; }

        public int Quantity { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime? CreationDate { get; set; }
    }
}