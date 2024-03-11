using MagicVilla_Web.Models.DTO;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MagicVilla_Web.Models.ViewModel
{
    public class VillaNumberUpdateViewModel
    {
        public VillaNumberUpdateViewModel()
        {
            VillaNumber = new VillaNumberDTOUpdate();
        }

        public VillaNumberDTOUpdate VillaNumber { get; set; }

        //DropDown
        [ValidateNever]
        public IEnumerable<SelectListItem> VillaList { get; set; }
    }
}
