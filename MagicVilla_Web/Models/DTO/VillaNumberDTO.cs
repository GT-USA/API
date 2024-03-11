using System.ComponentModel.DataAnnotations;

namespace MagicVilla_Web.Models.DTO
{
    public class VillaNumberDTO
    {
        [Required]
        public int VillaNo { get; set; }
        [Required]
        public int VillaID { get; set; }
        public string SpecialDetails { get; set; }

        //Navigation Property
        //VillaDTO needs to populated when return VillaNumberDTO
        public VillaDTO Villa { get; set; }
    }
}
