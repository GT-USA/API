using MagicVilla_Web.Models.DTO;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MagicVilla_Web.Models.ViewModel
{
    public class VillaNumberCreateViewModel
    {
        public VillaNumberCreateViewModel()
        {
            VillaNumber = new VillaNumberDTOCreate();
        }

        public VillaNumberDTOCreate VillaNumber { get; set; }

        //Dropdown
        [ValidateNever]
        public IEnumerable<SelectListItem> VillaList { get; set; }
    }
}
