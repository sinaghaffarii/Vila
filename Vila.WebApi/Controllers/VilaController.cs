using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Vila.WebApi.Dtos;
using Vila.WebApi.Services.Vila;

namespace Vila.WebApi.Controllers
{
    //[Route("api/[controller]")]
    [Route("api/Vila")]
    [ApiController]
    public class VilaController : ControllerBase
    {
        private readonly IVilaService _vila;
        public VilaController(IVilaService vila)
        {
            _vila = vila;
        }
        public IActionResult GetAll()
        {

            var list = _vila.GetAll();
            List<VilaDto> model = new();
            list.ForEach(x =>
            {
                model.Add(new()
                {
                    VilaId = x.VilaId,
                    Address = x.Address,
                    BuildDate = x.BuildDate,
                    City = x.City,
                    Mobile = x.Mobile,
                    Name = x.Name,
                    State = x.State
                });
            });
            return Ok(model);
        }
    }
}
