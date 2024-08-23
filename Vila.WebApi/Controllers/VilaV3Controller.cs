using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Vila.WebApi.Paging;
using Vila.WebApi.Services.Vila;

namespace Vila.WebApi.Controllers
{
    [Route("api/v{version:ApiVersion}/[controller]")]
    [ApiVersion("3.0")]
    [ApiController]
    public class VilaV3Controller : ControllerBase
    {
        private readonly IVilaService _vila;

        public VilaV3Controller(IVilaService vila)
        {
            _vila = vila;
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        [ProducesResponseType(200, Type = typeof(VilaAdminPaging))]
        [ProducesResponseType(400)]

        public IActionResult search(int pageId = 1, string? filter = "", int take = 2)
        {
            if (pageId < 1 || take < 1) return BadRequest();
            var model = _vila.SearchVilaAdmin(pageId, filter, take);
            return Ok(model);
        }
    }
}
