using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using World.API.DTO.States;
using World.API.Models;
using World.API.Repository.IRepository;

namespace World.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StateController : ControllerBase
    {
        private readonly IStateRepository _stateRepository;
        private readonly IMapper _mapper;
        public StateController(IStateRepository stateRepository,IMapper mapper)
        {
            _stateRepository = stateRepository; 
            _mapper = mapper;
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult<IEnumerable<StateDTO>>> GetAll()
        {
            var states = await _stateRepository.GetAll();
            var stateDTO = _mapper.Map<List<StateDTO>>(states); ;
            if (states == null)
            {
                return NoContent(); ;
            }
            return Ok(stateDTO);
        }
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult<StateDTO>> GetById(int id)
        {
            var state = await _stateRepository.Get(id);
            var stateDto = _mapper.Map<StateDTO>(state);
            if (state == null)
            {
                return NoContent();
            }
            return Ok(stateDto);
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<State>> Create([FromBody] CreateStateDTO stateDTO)
        {
            var result = _stateRepository.IsRecordExists(x=>x.Name==stateDTO.Name);
            if(result)
            {
                return Conflict("State already exists");
            }

            var state = _mapper.Map<State>(stateDTO);
            await _stateRepository.Create(state);
            return CreatedAtAction("GetById", new { id = state.Id }, state);
        }
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<State>> Update(int id, [FromBody]UpdateStateDTO updateStateDTO)
        {
            if(updateStateDTO == null || id!= updateStateDTO.Id)
            {
                return BadRequest();
            }
           var state= _mapper.Map<State>(updateStateDTO);
            await _stateRepository.Update(state);
            return Ok(state);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult<State>> Delete(int id)
        {
            if (id==0)
            {
                return NotFound();
            }
            var state = await _stateRepository.Get(id);
           

            await _stateRepository.Delete(state);
            return NoContent();
        }


    }
}
