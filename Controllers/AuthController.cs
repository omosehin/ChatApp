using System.Threading.Tasks;
using DatingApp.API.Data;
using DatingApp.API.Dtos;
using DatingApp.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace DatingApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController:ControllerBase
    {
        private readonly IAuthRepository _repo;
        public AuthController(IAuthRepository repo)
        {
            _repo = repo;

        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDtos registerDtos)
        {
                 registerDtos.Username = registerDtos.Username.ToLower();
                if(await _repo.UserExists(registerDtos.Username))
                    return BadRequest("Username already exists");

                var userToCreate = new User{
                      Username = registerDtos.Username
                };

                var createdUser = await _repo.Register(userToCreate,registerDtos.Password);

                return StatusCode(201);
        }
    }
}