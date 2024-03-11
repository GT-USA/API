using MagicVilla_Web.Models.DTO;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MagicVilla_Web.Models.ViewModel
{
    public class VillaNumberDeleteViewModel
    {
        public VillaNumberDeleteViewModel()
        {
            VillaNumber = new VillaNumberDTO();
        }

        public VillaNumberDTO VillaNumber { get; set; }

        //Dropdown
        [ValidateNever]
        public IEnumerable<SelectListItem> VillaList { get; set; }
    }
}
