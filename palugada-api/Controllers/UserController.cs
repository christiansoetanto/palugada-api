using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using palugada_api.Dto;
using palugada_api.Entities;
using palugada_api.Helpers;
using palugada_api.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace palugada_api.Controllers {
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]

    public class UserController : ControllerBase {
        private readonly UserService userService;
        private readonly IJwtAuthenticationManager jwtAuthenticationManager;
        public UserController(UserService userService, IJwtAuthenticationManager jwtAuthenticationManager)
        {
            this.userService = userService;
            this.jwtAuthenticationManager = jwtAuthenticationManager;
        }
    
        // GET api/<UserController>/5
        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id) {
            UserDto? user = await userService.Get(id);
            return user == null ? NotFound() : Ok(new 
            {
                user,
                Token = jwtAuthenticationManager.GenerateToken(user.Username)
            });
        }

        [AllowAnonymous]
        // GET api/<UserController>/5
        [HttpGet("anon/{id:int}")]
        public async Task<IActionResult> GetAnon(int id) {
            UserDto? user = await userService.Get(id);
            return user == null ? NotFound() : Ok(new {
                user,
                Token = jwtAuthenticationManager.GenerateToken(user.Username)
            });
        }


        [AllowAnonymous]
        // POST api/<UserController>
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserDto user)
        {
            UserDto registeredUser = await userService.Register(user);
            return Ok(user);

        }

        [AllowAnonymous]
        // POST api/<UserController>
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserDto u) {
            UserDto? user = await userService.Login(u);
            return user == null ? NotFound() : Ok(new {
                user,
                Token = jwtAuthenticationManager.GenerateToken(user.Username)
            });

        }


    }
}
