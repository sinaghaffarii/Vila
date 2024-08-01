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

        [HttpGet("[action]/{vilaId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<DetailDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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
        [HttpGet("[action]/{detailId:int}", Name = "GetById")]
        public IActionResult GetById(int detailId)
        {
            var detail = _detail.GetById(detailId);
            if (detail == null) return NotFound();
            var model = _mapper.Map<DetailDto>(detail);
            return StatusCode(200, model);
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(DetailDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Create([FromBody] DetailDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var detail = _mapper.Map<Models.Detail>(model);
            if (_detail.Create(detail))
            {
                var dtoDetail = _mapper.Map<DetailDto>(detail);
                return CreatedAtRoute("GetById", new { detailId = dtoDetail.DetailId }, dtoDetail);
            }
            ModelState.AddModelError("", "مشکل از سمت سرور میباشد، لطفا مجددا تلاش فرمایید.");
            return StatusCode(500, ModelState);
        }

        [HttpPatch("{detailId:int}")]
        public IActionResult Update(int detailId, [FromBody] DetailDto model)
        {
            if (detailId != model.DetailId)
                return NotFound();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var detail = _mapper.Map<Models.Detail>(model);
            if (_detail.Update(detail))
            {
                return StatusCode(204);

            }
            ModelState.AddModelError("", "مشکل از سمت سرور میباشد، لطفا مجددا تلاش فرمایید.");
            return StatusCode(500, ModelState);
        }

        [HttpDelete("{detailId:int}")]
        public IActionResult Remove(int detailId)
        {
            var detail = _detail.GetById(detailId);
            if (detail == null)
            {
                return NotFound();
            }

            if (_detail.Delete(detail))
            {
                return StatusCode(204);

            }
            ModelState.AddModelError("", "مشکل از سمت سرور میباشد، لطفا مجددا تلاش فرمایید.");
            return StatusCode(500, ModelState);
        }
    }
}
