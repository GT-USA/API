    using AutoMapper;
using MagicVilla_Web.Models;
using MagicVilla_Web.Models.DTO;
using MagicVilla_Web.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace MagicVilla_Web.Controllers
{
    public class VillaController : Controller
    {
        private readonly IVillaService _villaService;
        private readonly IMapper _mapper;

        public VillaController(IVillaService villaService, IMapper mapper)
        {
            _mapper = mapper;
            _villaService = villaService;
        }
        public async Task<IActionResult> IndexVilla()
        {
            //Return list of VillaDTO
            List<VillaDTO> list = new();
            var response = await _villaService.GetAllAsync<APIResponse>();
            if (response != null && response.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<VillaDTO>>(Convert.ToString(response.Result));
            }

            return View(list);
        }

        
        public async Task<IActionResult> CreateVilla()
        {
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateVilla(VillaDTOCreate model)
        {
            //Model is valid call API
            if (ModelState.IsValid)
            {
                var response = await _villaService.CreateAsync<APIResponse>(model);
                if (response != null && response.IsSuccess)
                {
                    TempData["success"] = "Villa Created Successfully";
                    //After create new villa, return back to IndexVilla
                    return RedirectToAction(nameof(IndexVilla));
                }
            }

            TempData["error"] = "Error!";
            //if Model is not valid
            return View(model);
        }

        
        public async Task<IActionResult> UpdateVilla(int villaId)
        {
            var response = await _villaService.GetAsync<APIResponse>(villaId);
            if (response != null && response.IsSuccess)
            {
                //retrive villaDTO with deserialization
                VillaDTO updateModel = JsonConvert.DeserializeObject<VillaDTO>(Convert.ToString(response.Result));
                //return updated values with villa dto update through mapper
                return View(_mapper.Map<VillaDTOUpdate>(updateModel));
            }
            TempData["error"] = "Error!";
            return NotFound();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateVilla(VillaDTOUpdate updateModel)
        {
            if (ModelState.IsValid)
            {
                var response = await _villaService.UpdateAsync<APIResponse>(updateModel);
                if (response != null && response.IsSuccess)
                {
                    TempData["success"] = "Villa Updated Successfully";
                    return RedirectToAction(nameof(IndexVilla));
                }
            }
            TempData["error"] = "Error!";
            return View(updateModel);
        }

        
        public async Task<IActionResult> DeleteVilla(int villaId)
        {
            var response = await _villaService.GetAsync<APIResponse>(villaId);
            if (response != null && response.IsSuccess)
            {
                VillaDTO deleteModel = JsonConvert.DeserializeObject<VillaDTO>(Convert.ToString(response.Result));
                return View(deleteModel);
            }
            return NotFound();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteVilla(VillaDTO deleteModel)
        {

            var response = await _villaService.DeleteAsync<APIResponse>(deleteModel.Id);
            if (response != null && response.IsSuccess)
            {
                TempData["success"] = "Villa Deleted Successfully";
                return RedirectToAction(nameof(IndexVilla));
            }

            TempData["error"] = "Error!";
            return View(deleteModel);
        }

    }
}
