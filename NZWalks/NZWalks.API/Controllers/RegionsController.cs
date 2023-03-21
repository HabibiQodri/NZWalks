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
       public async Task<IActionResult> GetAllRegions()
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
    }
}
