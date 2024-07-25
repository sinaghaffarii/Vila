using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Vila.WebApi.Dtos;
using Vila.WebApi.Services.Vila;
using AutoMapper;

namespace Vila.WebApi.Controllers
{
    //[Route("api/[controller]")]
    [Route("api/Vila")]
    [ApiController]
    public class VilaController : ControllerBase
    {
        private readonly IVilaService _vila;
        private readonly IMapper _mapper;
        public VilaController(IVilaService vila, IMapper mapper)
        {
            _vila = vila;
            _mapper = mapper;
        }
        public IActionResult GetAll()
        {

            var list = _vila.GetAll();
            List<VilaDto> model = new();
            //list.ForEach(x =>
            //{
            //    model.Add(new()
            //    {
            //        VilaId = x.VilaId,
            //        Address = x.Address,
            //        BuildDate = x.BuildDate,
            //        City = x.City,
            //        Mobile = x.Mobile,
            //        Name = x.Name,
            //        State = x.State
            //    });
            //});

            list.ForEach(x =>
            {
                if (x != null)
                {
                    model.Add(_mapper.Map<VilaDto>(x));
                }
            });

            return Ok(model);
        }

        [HttpGet("[action]/{VilaId:int}")]
        public IActionResult GetDetails([FromRoute] int vilaId)
        {

            var vila = _vila.GetById(vilaId);
            if (vila == null) return NotFound();
            var model = _mapper.Map<VilaDto>(vila);
            return Ok(model);
        }

        [HttpGet("[action]")]
        public IActionResult GetVilaAddress([FromQuery] int vilaId)
        {

            var vila = _vila.GetById(vilaId);
            if (vila == null) return NotFound();
            return Ok(new { id = vila.VilaId, state = vila.State, city = vila.City, address = vila.Address });
        }

        [HttpGet("[action]")]
        public IActionResult GetVilaMobile([FromHeader] int VilaId)
        {
            var vila = _vila.GetById(VilaId);
            if (vila == null) return NotFound();
            return Ok(new { id = vila.VilaId, mobile = vila.Mobile });
        }
        //[HttpPost]
        //public IActionResult Create([FromForm] VilaDto model)
        //{
        //    var vila = _mapper.Map<Model.Vila>(model);
        //    _vila.Create(vila);
        //    return Ok(new { status = true, message = "عملیات با موفقیت انجام شد" });
        //}

        [HttpPost]
        public IActionResult Create([FromBody] VilaDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var vila = _mapper.Map<Model.Vila>(model);
            if (_vila.Create(vila))
            {
                ModelState.AddModelError("", "عملیات با موفقیت انجام شد.");
                return StatusCode(201, ModelState);
            }
            ModelState.AddModelError("", "مشکل از سمت سرور میباشد، لطفا مجددا تلاش فرمایید.");
            return StatusCode(500, ModelState);
        }
    }
}
