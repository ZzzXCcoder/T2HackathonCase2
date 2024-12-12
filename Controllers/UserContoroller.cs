using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using T2HackathonCase2.Dtos;

namespace T2HackathonCase2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserContoroller : ControllerBase
    {
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterDto userRegisterDto)
        {
            
        }
    }
}
