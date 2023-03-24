using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [ApiController]
    // [Route("Regions")] == [Route("[controller]")]
    [Route("[controller]")]
    public class RegionsController : Controller
    {
        private readonly IRegionRepository regionRepository;
        private readonly IMapper mapper;

        public RegionsController(IRegionRepository regionRepository , IMapper mapper)
        {
            this.regionRepository = regionRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRegionsAsync()
        {
            var regions = await regionRepository.GetAllAsync();

            // return DTO Regions
                //var regionsDTO = new List<RegionDto>();
                //regions.ToList().ForEach(region => {
                //    var regionDto = new RegionDto()
                //    {
                //        Id = region.Id,
                //        Code = region.Code,
                //        Name = region.Name,
                //        Area = region.Area,
                //        Lat = region.Lat,
                //        Long = region.Long,
                //        Population  = region.Population,        
                //    };  
                //    regionsDTO.Add(regionDto);  
                //});
            
            var regionsDTO = mapper.Map<List<RegionDto>>(regions);
            
            return Ok(regionsDTO);
        }

        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetRegionAsync")]
        public async Task<IActionResult> GetRegionAsync(Guid id)
        {
            var region = await regionRepository.GetRegionAsync(id);

            if( region == null)
            {
                return NotFound();
            }

            var regionDto = mapper.Map<RegionDto>(region);

            return Ok(regionDto);
        }

        [HttpPost]    
        public async Task<IActionResult> AddRegionAsync(AddRegionRequest addRegionRequest)
        {
            // Request(DTO) to Domain Model
            var region = new Region()
            {
                Code = addRegionRequest.Code,
                Area =  addRegionRequest.Area,  
                Lat =   addRegionRequest.Lat,
                Long = addRegionRequest.Long,
                Name =  addRegionRequest.Name,
                Population = addRegionRequest.Population,
            };
            // Pass details to Repository
            var response =  await regionRepository.AddAsync(region);

            // Convert back to DTO
            var regionDto = new RegionDto
            {
                Id = response.Id,
                Code = response.Code,
                Area = response.Area,
                Lat = response.Lat,
                Long = response.Long,
                Name = response.Name,
                Population = response.Population,
            };

            return CreatedAtAction(nameof(GetRegionAsync), new { id = regionDto.Id }, regionDto);
        
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteRegionAsync(Guid id) 
        {
            // Get region from database

            var region = await regionRepository.DeleteAsync(id);

            // If null NotFound

            if (region == null)
            {
                return NotFound();
            }

            // Convert response back to DTO
            var regionDto = mapper.Map<RegionDto>(region);

            // return Ok response

            return Ok(regionDto);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateRegionAsync([FromRoute]Guid id,[FromBody] UpdateRegionRequest updateRegionRequest)
        {
            // Convert DTO to Domain Model
            var region = new Region
            {
                Code = updateRegionRequest.Code,
                Name = updateRegionRequest.Name,
                Area = updateRegionRequest.Area,
                Population = updateRegionRequest.Population,
                Lat = updateRegionRequest.Lat,
                Long= updateRegionRequest.Long,
            };

            // Update Region using repository

            var response = await regionRepository.UpdateAsync(id, region);

            // If null then not found

            if(response == null)
            {
                return NotFound();
            }

            // Convert domain to DTO

            var regionDto = mapper.Map<RegionDto>(region);

            // Return Ok response

            return Ok(regionDto);
        }

    }
}
