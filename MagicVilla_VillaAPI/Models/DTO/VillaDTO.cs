using System.ComponentModel.DataAnnotations;

namespace MagicVilla_VillaAPI.Models.DTO
{
    //DATA TRANSFER OBJECT
    //THIS CLASS CREATED FOR WHAT IS API EXPOSED FROM REAL Villa.cs 
    //SO, Choose what will shown in API from Villa.cs
    //Check Villa.cs to see differences
    public class VillaDTO
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(30)]
        public string Name { get; set; }
        public int Occupants { get; set; }
        public int Sqft { get; set; }
        public string Details { get; set; }
        [Required]
        public double Rate { get; set; }
        public string ImageUrl { get; set; }
        public string Amenity { get; set; }
    }
}
