using AutoMapper;
using MagicVilla_Web.Models;
using MagicVilla_Web.Models.DTO;
using MagicVilla_Web.Models.ViewModel;
using MagicVilla_Web.Services;
using MagicVilla_Web.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Reflection;

namespace MagicVilla_Web.Controllers
{
    public class VillaNumberController : Controller
    {
        private readonly IVillaNumberService _villaNumberService;
        //Inject for Dropdownbox in View to retrieve all villas
        private readonly IVillaService _villaService;
        private readonly IMapper _mapper;

        public VillaNumberController(IVillaNumberService villaNumberService, IMapper mapper, IVillaService villaService)
        {
            _mapper = mapper;
            _villaNumberService = villaNumberService;
            _villaService = villaService;
        }
        public async Task<IActionResult> IndexVillaNumber()
        {
            List<VillaNumberDTO> list = new();
            var response = await _villaNumberService.GetAllAsync<APIResponse>();
            if (response != null && response.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<VillaNumberDTO>>(Convert.ToString(response.Result));
            }

            return View(list);
        }

        
        public async Task<IActionResult> CreateVillaNumber()
        {
            //to populate dropdown, call villa services API
            VillaNumberCreateViewModel villaNumberVM = new();
            var response = await _villaService.GetAllAsync<APIResponse>();
            if (response != null && response.IsSuccess)
            {
                villaNumberVM.VillaList = JsonConvert.DeserializeObject<List<VillaDTO>>
                    (Convert.ToString(response.Result)).Select(i => new SelectListItem
                    {
                        Text = i.Name,
                        Value = i.Id.ToString()
                    });
            }
            return View(villaNumberVM);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateVillaNumber(VillaNumberCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var response = await _villaNumberService.CreateAsync<APIResponse>(model.VillaNumber);
                if (response != null && response.IsSuccess)
                {
                    return RedirectToAction(nameof(IndexVillaNumber));
                }
                else
                {
                    if (response.ErrorMessages.Count > 0)
                    {
                        ModelState.AddModelError("ErrorMessages", response.ErrorMessages.FirstOrDefault());
                    }
                }
            }

            //NEED TO POPULATE MODULE AGAIN IF GET ERROR MESSAGE!!!
            var resp = await _villaService.GetAllAsync<APIResponse>();
            if (resp != null && resp.IsSuccess)
            {
                model.VillaList = JsonConvert.DeserializeObject<List<VillaDTO>>
                    (Convert.ToString(resp.Result)).Select(i => new SelectListItem
                    {
                        Text = i.Name,
                        Value = i.Id.ToString()
                    });
            }
            return View(model);
        }

        
        public async Task<IActionResult> UpdateVillaNumber(int villaNo)
        {
            //to populate dropdown, call villa services API
            VillaNumberUpdateViewModel villaNumberVM = new();
            var response = await _villaNumberService.GetAsync<APIResponse>(villaNo);
            if (response != null && response.IsSuccess)
            {
                //retrive villaDTO with deserialization
                VillaNumberDTO updateModel = JsonConvert.DeserializeObject<VillaNumberDTO>(Convert.ToString(response.Result));
                //return updated values with villa dto update through mapper
                villaNumberVM.VillaNumber = _mapper.Map<VillaNumberDTOUpdate>(updateModel);
            }

            response = await _villaService.GetAllAsync<APIResponse>();
            if (response != null && response.IsSuccess)
            {
                villaNumberVM.VillaList = JsonConvert.DeserializeObject<List<VillaDTO>>
                    (Convert.ToString(response.Result)).Select(i => new SelectListItem
                    {
                        Text = i.Name,
                        Value = i.Id.ToString()
                    });
                return View(villaNumberVM);
            }

            return NotFound();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateVillaNumber(VillaNumberUpdateViewModel vmUpdate)
        {
            if (ModelState.IsValid) 
            {
                var response = await _villaNumberService.UpdateAsync<APIResponse>(vmUpdate.VillaNumber);
                if (response != null && response.IsSuccess)
                {
                    return RedirectToAction(nameof(IndexVillaNumber));
                }
                else 
                {
                    if (response.ErrorMessages.Count > 0)
                    {
                        ModelState.AddModelError("ErrorMessages", response.ErrorMessages.FirstOrDefault());
                    }
                }
            }

            //NEED TO POPULATE MODULE AGAIN IF GET ERROR MESSAGE!!!
            var resp = await _villaService.GetAllAsync<APIResponse>();
            if (resp != null && resp.IsSuccess)
            {
                vmUpdate.VillaList = JsonConvert.DeserializeObject<List<VillaDTO>>
                    (Convert.ToString(resp.Result)).Select(i => new SelectListItem
                    {
                        Text = i.Name,
                        Value = i.Id.ToString()
                    });
            }
            return View(vmUpdate);
        }
        //AZURE DEPLOYMENT TEST===========================

        
        public async Task<IActionResult> DeleteVillaNumber (int villaNo)
        {
            //to populate dropdown, call villa services API
            VillaNumberDeleteViewModel villaNumberVM = new();
            var response = await _villaNumberService.GetAsync<APIResponse>(villaNo);
            if (response != null && response.IsSuccess)
            {
                //retrive villaDTO with deserialization
                VillaNumberDTO updateModel = JsonConvert.DeserializeObject<VillaNumberDTO>(Convert.ToString(response.Result));
                //return updated values with villa dto update through mapper
                villaNumberVM.VillaNumber = updateModel;
            }

            response = await _villaService.GetAllAsync<APIResponse>();
            if (response != null && response.IsSuccess)
            {
                villaNumberVM.VillaList = JsonConvert.DeserializeObject<List<VillaDTO>>
                    (Convert.ToString(response.Result)).Select(i => new SelectListItem
                    {
                        Text = i.Name,
                        Value = i.Id.ToString()
                    });
                return View(villaNumberVM);
            }

            return NotFound();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteVillaNumber(VillaNumberDeleteViewModel vm)
        {
            var response = await _villaNumberService.DeleteAsync<APIResponse>(vm.VillaNumber.VillaNo); 
            if (response != null && response.IsSuccess) 
            {
                return RedirectToAction(nameof(IndexVillaNumber));
            }
            return View(vm);    
        }
    }
}
