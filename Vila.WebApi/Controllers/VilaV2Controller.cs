using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Vila.WebApi.Services.Vila;

namespace Vila.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VilaV2Controller : ControllerBase
    {
        private readonly IVilaService _vila;

        public VilaV2Controller(IVilaService vila)
        {
            _vila = vila;
        }

        [HttpGet]
        public IActionResult search(int pageId = 1, string? filter = "", int take = 2)
        {
            if (pageId < 1 || take < 1) return BadRequest();
            var model = _vila.SearchVila(pageId, filter, take);
            return Ok(model);
        }
    }
}
