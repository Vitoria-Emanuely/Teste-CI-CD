using Microsoft.AspNetCore.Mvc;
using UserApi.Api.Models;
using UserApi.Api.Services;

namespace UserApi.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _service;

        public UsersController(IUserService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult<List<User>> Get() => _service.Get();

        [HttpPost]
        public ActionResult<User> Create(User user)
        {
            _service.Create(user);
            return CreatedAtAction(nameof(Create), new { id = user.Id }, user);
        }
    }
}
