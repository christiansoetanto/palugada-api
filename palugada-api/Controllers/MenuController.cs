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
    public class MenuController : ControllerBase {
        private readonly MenuService menuService;
        private readonly IJwtAuthenticationManager jwtAuthenticationManager;
        public MenuController(MenuService menuService, IJwtAuthenticationManager jwtAuthenticationManager) {
            this.menuService = menuService;
            this.jwtAuthenticationManager = jwtAuthenticationManager;
        }

        // GET api/<MenuController>/5
        [HttpGet("user/{userId:int}")]
        public async Task<IActionResult> GetByUserId(int userId)
        {
            List<MenuDto> Menu = await menuService.GetByUserId(userId);
            return Ok(Menu);
        }


        // GET api/<MenuController>/5
        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id) {
            MenuDto? Menu = await menuService.Get(id);
            return Menu == null ? NotFound() : Ok(Menu);
        }

        // POST api/<MenuController>
        [HttpPost("{userId:int}")]
        public async Task<IActionResult> Insert(int userId, [FromBody] MenuDto Menu) {
            return Ok(await menuService.Insert(userId, Menu));

        }

        // PUT api/<MenuController>/5
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put(int id, [FromBody] MenuDto Menu) {
            return Ok(await menuService.Put(id, Menu));
        }

        // DELETE api/<MenuController>/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            await menuService.Delete(id);
            return Ok(new {status= 200});
        }
    }
}
