using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;
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

            if (_customer.Register(model))
            {
                return StatusCode(201);
            }
            else
            {
                ModelState.AddModelError("", "خطای شبکه!");
                return StatusCode(500, ModelState);
            }
        }
        [HttpPost("Login")]
        public IActionResult Login([FromBody] RegisterModel login)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_customer.PasswordIsCorrect(login.Mobile, login.Pass))
            {
                ModelState.AddModelError("model.mobile", "کاربری یافت نشد :Error.");
                return BadRequest(ModelState);
            }

            var user = _customer.Login(login.Mobile, login.Pass);
            if (user == null) return NotFound();

            return Ok(user);
        }
    }
}
