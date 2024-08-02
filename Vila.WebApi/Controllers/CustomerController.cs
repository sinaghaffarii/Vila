using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Vila.WebApi.CustomerModels;
using Vila.WebApi.Services.Customer;

namespace Vila.WebApi.Controllers
{
    [Route("api/v{version:ApiVersion}/Vila")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customer;
        public CustomerController(ICustomerService customer)
        {
            _customer = customer;
        }
        [HttpPost("Register")]
        public IActionResult Register([FromBody] RegisterModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (_customer.ExistMobile(model.Mobile))
            {
                ModelState.AddModelError("model.mobile", "شماره موبایل تکراری است");
                return BadRequest(ModelState);
            }

            if(_customer.Register(model))
            {
                return StatusCode(201);
            }else
            {
                ModelState.AddModelError("", "خطای شبکه!");
                return StatusCode(500, ModelState);
            }
        }
    }
}
