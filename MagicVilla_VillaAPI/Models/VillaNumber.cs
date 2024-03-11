using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MagicVilla_VillaAPI.Models
{
    public class VillaNumber
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        // defines that number will provided by user and will use as key
        public int VillaNo { get; set; }

        //FK for Villa table
        [ForeignKey("Villa")]
        public int VillaID { get; set; }

        //
        //Navigation property to Villa to get Villa Id
        //Populate VillaNumber with Villa's that they have foreign key relationship
        public Villa Villa { get; set; }
        public string SpecialDetails { get; set; }

        public DateTime CurrentDate { get; set; }

        public DateTime UpdatedDate { get; set; }
    }
}
