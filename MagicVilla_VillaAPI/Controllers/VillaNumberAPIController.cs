using AutoMapper;
using MagicVilla_VillaAPI.Logging;
using MagicVilla_VillaAPI.Models;
using MagicVilla_VillaAPI.Models.DTO;
using MagicVilla_VillaAPI.Repository.IRepository;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace MagicVilla_VillaAPI.Controllers
{

    [Route("api/VillaNumberAPI")]
    [ApiController]

    public class VillaNumberAPIController : ControllerBase
    {
        protected APIResponse _response;

        private readonly IVillaNumberRepository _dbVillaNumber;

        //After FK implementation
        private readonly IVillaRepository _dbVilla;
        private readonly ILogging _logger;

        private readonly IMapper _mapper;
        public VillaNumberAPIController(ILogging logger, IVillaNumberRepository dbVilla, IMapper mapper, IVillaRepository dbVillas)
        {
            _logger = logger;
            _dbVillaNumber = dbVilla;
            _mapper = mapper;
            this._response = new APIResponse();
            _dbVilla = dbVillas;

        }

        //ENDPOINTS

        //HttpGet ALL VILLA NUMBER OBJECTS
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]

        public async Task<ActionResult<APIResponse>> GetVillaNumbers()
        {
            try
            {
                //Add includeProperties to call associated Villa properties with VillaNumber
                IEnumerable<VillaNumber> villaNumberList = await _dbVillaNumber.GetAllAsync(includeProperties: "Villa");
                _response.Result = _mapper.Map<List<VillaNumberDTO>>(villaNumberList);
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
                _logger.Log("Getting all Villa Number's", "");
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>()
                {
                    ex.ToString()
                };
            }
            return _response;
        }

        //HttpGet SINGLE VILLA NUMBER OBJECT
        [HttpGet("{id:int}", Name = "GetVillaNumber")]
        [ProducesResponseType(StatusCodes.Status200OK)] //EndPont Status Documentation
        [ProducesResponseType(StatusCodes.Status400BadRequest)] //EndPont Status Documentation
        [ProducesResponseType(StatusCodes.Status404NotFound)] //EndPont Status Documentation

        public async Task<ActionResult<APIResponse>> GetVillaNumber(int id)
        {
            try
            {
                //400
                if (id == 0)
                {
                    _logger.Log("Get Villa Number Error with Id " + id, "error");
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }

                var villaNumber = await _dbVillaNumber.GetAsync(u => u.VillaNo == id);

                //404
                if (villaNumber == null)
                {
                    return NotFound();
                }

                _response.Result = _mapper.Map<VillaNumberDTO>(villaNumber);
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);

            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>()
                {
                    ex.ToString()
                };
            }
            return _response;
        }

        //Create new VillaNumber 
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)] //EndPont Status Documentation
        [ProducesResponseType(StatusCodes.Status400BadRequest)] //EndPont Status Documentation
        [ProducesResponseType(StatusCodes.Status404NotFound)] //EndPont Status Documentation

        public async Task<ActionResult<APIResponse>> CreateVilla([FromBody] VillaNumberDTOCreate createNumberDTO)
        {
            try
            {
                if (await _dbVilla.GetAsync(u => u.Id == createNumberDTO.VillaID) == null)
                {
                    ModelState.AddModelError("ErrorMessages", "Villa ID is Invalid!");
                    return BadRequest(ModelState);
                }
                //Check if VillaNumber is not already exist
                if (await _dbVillaNumber.GetAsync(u => u.VillaNo == createNumberDTO.VillaNo) != null)
                {
                    ModelState.AddModelError("ErrorMessages", "Villa is already exist!");
                    return BadRequest(ModelState);

                }

                if (createNumberDTO.VillaNo == 0)
                {
                    ModelState.AddModelError("ErrorMessages", "VillaNumber can NOT be 0");
                    return BadRequest(ModelState);
                }

                if (createNumberDTO == null) { return BadRequest(createNumberDTO); }

                //Mapping Conversion

                VillaNumber modelDTO = _mapper.Map<VillaNumber>(createNumberDTO);

                //BUG FIXED IN SQL SERVER
                modelDTO.CurrentDate = DateTime.Now;
                modelDTO.UpdatedDate = DateTime.Now;


                await _dbVillaNumber.CreateAsync(modelDTO);

                //Return 201
                _response.Result = _mapper.Map<VillaNumber>(modelDTO);
                _response.StatusCode = HttpStatusCode.Created;

                return CreatedAtRoute("GetVillaNumber", new { id = modelDTO.VillaNo }, _response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>()
                { ex.ToString() };
            }
            return _response;
        }

        //Delete VillaNumber
        [HttpDelete("{id:int}", Name = "DeleteVillaNumber")]
        [ProducesResponseType(StatusCodes.Status200OK)] //EndPont Status Documentation
        [ProducesResponseType(StatusCodes.Status400BadRequest)] //EndPont Status Documentation
        [ProducesResponseType(StatusCodes.Status404NotFound)] //EndPont Status Documentation

        public async Task<ActionResult<APIResponse>> DeleteVillaNumber(int id)
        {
            try
            {
                if (id < 0) { return BadRequest(); }

                var villaNumber = await _dbVillaNumber.GetAsync(u => u.VillaNo == id);

                if (villaNumber == null) { return NotFound(); }

                await _dbVillaNumber.RemoveAsync(villaNumber);

                _response.StatusCode = HttpStatusCode.NotFound;
                _response.IsSuccess = true;
                return Ok(_response);

            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>()
                { ex.ToString() };
            }

            return _response;
        }

        //Update VillaNumber All properties
        [HttpPut("{id:int}", Name = "UpdateVillaNumber")]
        [ProducesResponseType(StatusCodes.Status200OK)] //EndPont Status Documentation
        [ProducesResponseType(StatusCodes.Status400BadRequest)] //EndPont Status Documentation
        public async Task<ActionResult<APIResponse>> UpdateVillaNumber(int id, [FromBody] VillaNumberDTOUpdate updateDTO)
        {
            try
            {
                if (updateDTO == null || id != updateDTO.VillaNo)
                {
                    return BadRequest();
                }

                if (await _dbVilla.GetAsync(u => u.Id == updateDTO.VillaID) == null)
                {
                    ModelState.AddModelError("ErrorMessages", "Villa ID is Invalid!");
                    return BadRequest(ModelState);
                }

                VillaNumber modelDTO = _mapper.Map<VillaNumber>(updateDTO);

                await _dbVillaNumber.UpdateAsync(modelDTO);

                _response.StatusCode = HttpStatusCode.NoContent;
                _response.IsSuccess = true;
                return Ok(_response);

            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.ToString() };
            }
            return _response;
        }

        [HttpPatch("{id:int}", Name = "UpdatePartialVillaNumber")]
        [ProducesResponseType(StatusCodes.Status204NoContent)] //EndPont Status Documentation
        [ProducesResponseType(StatusCodes.Status400BadRequest)] //EndPont Status Documentation

        public async Task<IActionResult> UpdatePartialVillaNumber(int id, JsonPatchDocument<VillaNumberDTOUpdate> patchDTO)
        {
            // TO RUN THIS, CHANGE JSON REQUEST BODY WITH:
            //"op": "replace", "path": "/specialDetails", "value": "partailWworks!"
            //jsonpatch.com for more
            if (patchDTO == null || id == 0)
            {
                return BadRequest();
            }

            var villaNumber = await _dbVillaNumber.GetAsync(u => u.VillaNo == id, tracker: false);
            VillaNumberDTOUpdate villaNumbers = _mapper.Map<VillaNumberDTOUpdate>(villaNumber);
            if (villaNumber == null)
            {
                return BadRequest();
            }
            patchDTO.ApplyTo(villaNumbers, ModelState);

            VillaNumber modelDTO = _mapper.Map<VillaNumber>(villaNumbers);

            modelDTO.CurrentDate = DateTime.Now;
            modelDTO.UpdatedDate = DateTime.Now;

            await _dbVillaNumber.UpdateAsync(modelDTO);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return NoContent();
        }
    }

}

