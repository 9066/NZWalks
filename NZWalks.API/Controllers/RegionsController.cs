using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private NZWalksDbContext _dbContext;
        private IRegionRepository _regionRepository;
        private readonly IMapper _mapper;

        public RegionsController(NZWalksDbContext dbContext,IRegionRepository regionRepository, IMapper mapper)
        {
            this._dbContext = dbContext;
            this._regionRepository = regionRepository;
            this._mapper = mapper;
        }

        // Get All regions
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            // Get data from the database : Domain Models
            var regions = await _regionRepository.GetAllAsync();

            // Use automapper to map the domain model to dto
            var regionDtos =  _mapper.Map<List<RegionDTO>>(regions);
            // Return DTO.
            return Ok(regionDtos);
        }

        // Get Regions by Id

        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id) {
            // Get data from the database : Domain Models
           // var region = _dbContext.Regions.Find(id);
           // or
           var region = await _regionRepository.GetByIdAsync(id);
            if (region == null) { 
                return NotFound();
            }
            // Map Domain with DTO
            var regionDto = _mapper.Map<RegionDTO>(region);
            return Ok(regionDto);
        }

        // Post  to create new region
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddRegionRequestDTO  addRegionRequestDTO)
        {
            // Map DTO to Domain Model
            var regionDomainModel = _mapper.Map<Region>(addRegionRequestDTO);
            //Use domain model to create the region
            regionDomainModel = await _regionRepository.CreateAsync(regionDomainModel);

            // Map Domain model to DTO back to send it client
            var regionDto = _mapper.Map<RegionDTO>(regionDomainModel);
            return CreatedAtAction(nameof(GetById), new {id = regionDomainModel.Id}, regionDto);
        }

        // Update region
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update([FromRoute]Guid id, [FromBody] UpdateRegionRequestDTO updateRegionRequestDTO)
        {

            // Map DTO to domain model
            var regionDomain = _mapper.Map<Region>(updateRegionRequestDTO);
            var region =  await _regionRepository.UpdateAsync(id, regionDomain);
            if (region == null)
            {
                return NotFound();
            }
            // Convert Domain Model to DTO
            var regionDto = _mapper.Map<RegionDTO>(region);
            return Ok(regionDto);

        }

        [HttpDelete]
        [Route("{Id:guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid Id)
        {
            // Check first whether region exist or not 

            var region = await _regionRepository.DeleteAsync(Id);
            if (region == null)
            {
                return NotFound();
            }            // We can return the deleted Region Back to UI 
            return Ok();
        }

    }
}
