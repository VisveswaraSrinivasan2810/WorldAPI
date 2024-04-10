using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using World.API.Data;
using World.API.DTO;
using World.API.Models;
using World.API.Repository.IRepository;

namespace World.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly ICountryRepository _countryRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CountryController> _logger;
        public CountryController(ICountryRepository countryRepository, IMapper mapper, ILogger<CountryController> logger)
        {
            _countryRepository = countryRepository;
            _mapper = mapper;
            _logger = logger;
        }
        [HttpGet]
        public async  Task<ActionResult<IEnumerable<Country>>> GetAll()
        {
            var countries = await _countryRepository.GetAll();
            var countriesDTO = _mapper.Map<List<Country>>(countries);
            if(countries==null)
            {
                return NoContent();
            }
            return Ok(countriesDTO);
        }
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<Country>> GetById(int id) 
        {
            var country = await _countryRepository.Get(id);
            var countryDTO = _mapper.Map<CountryDTO>(country);
            if(country==null)
            {
                _logger.LogError($"Error While trying to get a id :{id}");
                return NoContent();
            }
            return Ok(countryDTO);
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult<CountryDTO>> Create([FromBody]CountryDTO countrydto) 
        {
            var result = _countryRepository.IsRecordExists(x=>x.Name == countrydto.Name);
            if (result)
            {
                return Conflict("Country already exists");
            }

            //Country country = new Country();
            //country.Name = countrydto.Name;
            //country.ShortName = countrydto.ShortName;
            //country.CountryCode = countrydto.CountryCode;

            var country = _mapper.Map<Country>(countrydto);

            //_dbContext.Countries.Add(country);
            //_dbContext.SaveChanges();

           await  _countryRepository.Create(country);
            return CreatedAtAction("GetById",new {id = country.Id},country);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Country>> Update(int id,[FromBody]CountryUpdateDTO countryDto) 
        {
            if(countryDto==null || id!=countryDto.Id)
            {
                return BadRequest();
            }
            //var countryFromDb = _dbContext.Countries.Find(id);
            //if(countryFromDb==null)
            //{
            //    return NotFound();
            //}
            //countryFromDb.Name = country.Name;
            //countryFromDb.ShortName = country.ShortName;
            //countryFromDb.CountryCode = country.CountryCode;

            //_dbContext.Countries.Update(countryFromDb);
            //_dbContext.SaveChanges();

            var country = _mapper.Map<Country>(countryDto);
            await _countryRepository.Update(country);
            return Ok();
        }
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult<Country>>DeleteById(int id)
        {
            var country = await _countryRepository.Get(id);
            if(country==null)
            {
                return NotFound();
            }
            await _countryRepository.Delete(country);
            return NoContent();
        }
    }
}
