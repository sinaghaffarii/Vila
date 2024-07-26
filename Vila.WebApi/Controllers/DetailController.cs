using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Vila.WebApi.Dtos;
using Vila.WebApi.Services.Detail;
using Vila.WebApi.Services.Vila;

namespace Vila.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DetailController : ControllerBase
    {
        private readonly IDetailService _detail;
        private readonly IVilaService _vila;
        private readonly IMapper _mapper;

        public DetailController(IDetailService detail, IVilaService vila, IMapper mapper)
        {
            _detail = detail;
            _vila = vila;
            _mapper = mapper;
        }

        [HttpGet("{vilaId:int}")]
        public IActionResult GetAllVilaDetails(int vilaId)
        {
            var vila = _vila.GetById(vilaId);
            if (vila == null) return NotFound();

            var details = _detail.GetAllVilaDetails(vilaId);
            List<DetailDto> model = new();

            details.ForEach(x =>
            {
                model.Add(_mapper.Map<DetailDto>(x));
            });
            return Ok(model);
        }
    }
}
