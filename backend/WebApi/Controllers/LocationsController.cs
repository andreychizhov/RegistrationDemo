using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PM.Infrastructure;
using PM.Model.Entities;
using System.Collections.Generic;
using System.Linq;
using WebApi.DTO;

namespace WebApi.Controllers
{
    //[Authorize]
    [Route("[controller]")]
    [ApiController]
    public class LocationsController : ControllerBase
    {
        private readonly IRepositoryWithTypedId<Country, int> _countryRepo;
        private readonly IRepositoryWithTypedId<Province, int> _provinceRepo;
        private readonly IMapper _mapper;

        public LocationsController(IMapper mapper, IRepositoryWithTypedId<Country, int> countryRepo, 
            IRepositoryWithTypedId<Province, int> provinceRepo)
        {
            _countryRepo = countryRepo;
            _provinceRepo = provinceRepo;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetCountries()
        {
            var countries = _countryRepo.GetAll();

            return Ok(_mapper.Map<IList<LocationResponse>>(countries));
        }

        [HttpGet("{id}")]
        public IActionResult GetProvinces(int id)
        {
            var countries = _provinceRepo.GetAll()
                .Where(x => x.ParentId == id).ToList();

            return Ok(_mapper.Map<IList<LocationResponse>>(countries));
        }
    }
}