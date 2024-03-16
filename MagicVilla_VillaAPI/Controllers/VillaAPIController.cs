using AutoMapper;
using MagicVilla_VillaAPI.Data;
using MagicVilla_VillaAPI.Logging;
using MagicVilla_VillaAPI.Models;
using MagicVilla_VillaAPI.Models.DTO;
using MagicVilla_VillaAPI.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace MagicVilla_VillaAPI.Controllers
{
    [Route("api/VillaAPI")] // change VillaAPI [controller] auto apply class name b4 Controller string
    [ApiController]
    public class VillaAPIController : ControllerBase
    {
        protected APIResponse _response;

        private readonly IVillaRepository _dbVilla;

        private readonly ILogging _logger;

        private readonly IMapper _mapper;

        public VillaAPIController(ILogging logger, IVillaRepository dbVilla, IMapper mapper)
        {
            _logger = logger;
            _dbVilla = dbVilla;
            _mapper = mapper;
            this._response = new APIResponse();
        }

        //ENDPOINTS

        [Authorize]
        //HttpGet 
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)] //EndPont Status Documentation
        [ProducesResponseType(StatusCodes.Status401Unauthorized)] //EndPont Status Documentation
        public async Task<ActionResult<APIResponse>> GetVillas()
        {
            try
            {
                IEnumerable<Villa> villaList = await _dbVilla.GetAllAsync();
                _response.Result = _mapper.Map<List<VillaDTO>>(villaList);
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
                _logger.Log("Getting all Villa's", "");
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        //HttpGet ENDPOINT
        [Authorize(Roles = "admin")]
        [HttpGet("{id:int}", Name = "GetVilla")]
        [ProducesResponseType(StatusCodes.Status200OK)] //EndPont Status Documentation
        [ProducesResponseType(StatusCodes.Status400BadRequest)] //EndPont Status Documentation
        [ProducesResponseType(StatusCodes.Status404NotFound)] //EndPont Status Documentation
        [ProducesResponseType(StatusCodes.Status403Forbidden)] //EndPont Status Documentation
        [ProducesResponseType(StatusCodes.Status401Unauthorized)] //EndPont Status Documentation
        //SINGLE VILLA OBJECT 
        public async Task<ActionResult<APIResponse>> GetVilla(int id)
        {

            //Add logical conditions to define return responses 400/404
            try
            {
                //400
                if (id == 0)
                {
                    _logger.Log("Get Villa Error with Id: " + id, "error");
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }

                var villa = await _dbVilla.GetAsync(u => u.Id == id);

                //404
                if (villa == null) { return NotFound(); }

                //200
                _response.Result = _mapper.Map<VillaDTO>(villa);
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;

        }

        //Creating Resource (new Villa)
        [Authorize(Roles ="admin")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)] //EndPont Status Documentation
        [ProducesResponseType(StatusCodes.Status400BadRequest)] //EndPont Status Documentation
        [ProducesResponseType(StatusCodes.Status404NotFound)] //EndPont Status Documentation
        [ProducesResponseType(StatusCodes.Status403Forbidden)] //EndPont Status Documentation
        [ProducesResponseType(StatusCodes.Status401Unauthorized)] //EndPont Status Documentation
        public async Task<ActionResult<APIResponse>> CreateVilla([FromBody] VillaDTOCreate createDTO)
        {
            try
            {
                //Check that created Villa is not already exist 
                if (await _dbVilla.GetAsync(u => u.Name.ToLower() == createDTO.Name.ToLower()) != null)
                {
                    ModelState.AddModelError("ErrorMessages", "Villa is already exist!");
                    return BadRequest(ModelState);
                }

                if (createDTO == null) { return BadRequest(createDTO); }

                //Replace with Manual Converstion
                Villa modelDTO = _mapper.Map<Villa>(createDTO);

                await _dbVilla.CreateAsync(modelDTO);

                //To Get Route (location) for new created Villa in Response Body
                //return 201 when object created
                _response.Result = _mapper.Map<VillaDTO>(modelDTO);
                _response.StatusCode = HttpStatusCode.Created;

                return CreatedAtRoute("GetVilla", new { id = modelDTO.Id }, _response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        //Delete Resource (existing Villa)
        [Authorize(Roles = "admin")]
        [HttpDelete("{id:int}", Name = "DeleteVilla")]
        [ProducesResponseType(StatusCodes.Status204NoContent)] //EndPont Status Documentation
        [ProducesResponseType(StatusCodes.Status403Forbidden)] //EndPont Status Documentation
        [ProducesResponseType(StatusCodes.Status401Unauthorized)] //EndPont Status Documentation
        [ProducesResponseType(StatusCodes.Status400BadRequest)] //EndPont Status Documentation
        [ProducesResponseType(StatusCodes.Status404NotFound)] //EndPont Status Documentation
        // IActionResilt is not require return type!!!
        // Delete is not require return anything!!!
        public async Task<ActionResult<APIResponse>> DeleteVilla(int id)
        {
            try
            {
                if (id == 0) { return BadRequest(); }

                var villa = await _dbVilla.GetAsync(u => u.Id == id);

                if (villa == null) { return NotFound(); }

                await _dbVilla.RemoveAsync(villa);
                _response.StatusCode = HttpStatusCode.NoContent;
                _response.IsSuccess = true;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }


        //Update Resource (existing Villa) => all properties!
        [HttpPut("{id:int}", Name = "UpdateVilla")]
        [ProducesResponseType(StatusCodes.Status204NoContent)] //EndPont Status Documentation
        [ProducesResponseType(StatusCodes.Status400BadRequest)] //EndPont Status Documentation
        public async Task<ActionResult<APIResponse>> UpdateVilla(int id, [FromBody] VillaDTOUpdate updateDTO)
        {
            try
            {
                if (updateDTO == null || id != updateDTO.Id)
                {
                    return BadRequest();
                }

                Villa modelDTO = _mapper.Map<Villa>(updateDTO);

                await _dbVilla.UpdateAsync(modelDTO);

                _response.StatusCode = HttpStatusCode.NoContent;
                _response.IsSuccess = true;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [HttpPatch("{id:int}", Name = "UpdatePartialVilla")]
        [ProducesResponseType(StatusCodes.Status204NoContent)] //EndPont Status Documentation
        [ProducesResponseType(StatusCodes.Status400BadRequest)] //EndPont Status Documentation
        public async Task<IActionResult> UpdatePartialVilla(int id, JsonPatchDocument<VillaDTOUpdate> patchDTO)
        {

            //TO RUN THIS, CHANGE JSON REQUEST BODY WITH:
            //"op": "replace", "path": "/name", "value": "ANY NEW VALUE"
            //jsonpatch.com for more
            if (patchDTO == null || id == 0)
            {
                return BadRequest();
            }

            //Convert Villa to VillaDTO for local changes
            //AsNoTracking() avoids tracking Id's in villas and modelDTO at the same time. (replaced with tracker)
            //So, Entity Framework stop traking villas Id and apply changes through
            //Villa object instead VillaDTO.
            var villa = await _dbVilla.GetAsync(u => u.Id == id, tracker: false);

            VillaDTOUpdate villas = _mapper.Map<VillaDTOUpdate>(villa);

            if (villa == null)
            {
                return BadRequest();
            }

            patchDTO.ApplyTo(villas, ModelState);

            Villa modelDTO = _mapper.Map<Villa>(villas);

            await _dbVilla.UpdateAsync(modelDTO);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return NoContent();
        }
    }
}
