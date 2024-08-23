using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Vila.WebApi.Dtos;
using Vila.WebApi.Services.Vila;
using AutoMapper;
using System.Threading.Tasks.Dataflow;
using Microsoft.AspNetCore.Authorization;

namespace Vila.WebApi.Controllers
{
    //[Route("api/[controller]")]
    [Route("api/v{version:ApiVersion}/Vila")]
    //[ApiVersion("1.0")]
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
        /// <summary>
        /// دریافت لیست تمام ویلا ها 
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = "admin")]
        [HttpGet("[action]")]
        [Authorize]
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
        /// <summary>
        /// دریافت یک ویلا و آی دی ویلا
        /// </summary>
        /// <param name="vilaId"></param>
        /// <returns></returns>
        [HttpGet("[action]/{VilaId:int}", Name = "GetDetails")]
        [Authorize]
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
        //    var vila = _mapper.Map<Models.Vila>(model);
        //    _vila.Create(vila);
        //    return Ok(new { status = true, message = "عملیات با موفقیت انجام شد" });
        //}
        /// <summary>
        /// ایجاد یک ویلای جدید
        /// </summary>
        /// <param name="model">اطلاعات ویلا (VilaDto)</param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = "admin")]
        public IActionResult Create([FromBody] VilaDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var vila = _mapper.Map<Models.Vila>(model);
            if (_vila.Create(vila))
            {
                return CreatedAtRoute("GetDetails", new { vilaId = vila.VilaId }, _mapper.Map<VilaDto>(vila));
            }
            ModelState.AddModelError("", "مشکل از سمت سرور میباشد، لطفا مجددا تلاش فرمایید.");
            return StatusCode(500, ModelState);
        }
        /// <summary>
        /// ویرایش ویلا
        /// </summary>
        /// <param name="vilaId">آی دی ویلا</param>
        /// <param name="model">اطاعات ویلا(VilaDto)</param>
        /// <returns></returns>
        [HttpPatch("{vilaId:int}")]
        [Authorize(Roles = "admin")]
        public IActionResult Update(int vilaId, [FromBody] VilaDto model)
        {
            if(vilaId != model.VilaId)
                return NotFound();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var vila = _mapper.Map<Models.Vila>(model);
            if (_vila.Update(vila))
            {
                return StatusCode(204);

            }
            ModelState.AddModelError("", "مشکل از سمت سرور میباشد، لطفا مجددا تلاش فرمایید.");
            return StatusCode(500, ModelState);
        }
        /// <summary>
        /// حذف ویلا
        /// </summary>
        /// <param name="vilaId">کلید ویلا</param>
        /// <returns></returns>
        [HttpDelete("{vilaId:int}")]
        [Authorize(Roles = "admin")]
        public IActionResult Remove(int vilaId)
        {
            var vila = _vila.GetById(vilaId);
            if(vila == null)
            {
                return NotFound();
            }
       
            if (_vila.delete(vila))
            {
                return StatusCode(204);

            }
            ModelState.AddModelError("", "مشکل از سمت سرور میباشد، لطفا مجددا تلاش فرمایید.");
            return StatusCode(500, ModelState);
        }
    }
}
