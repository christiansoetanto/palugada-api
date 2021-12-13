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
    public class OrderController : ControllerBase {
        private readonly OrderService orderService;
        private readonly IJwtAuthenticationManager jwtAuthenticationManager;
        public OrderController(OrderService orderService, IJwtAuthenticationManager jwtAuthenticationManager) {
            this.orderService = orderService;
            this.jwtAuthenticationManager = jwtAuthenticationManager;
        }

        // GET api/<OrderController>/5
        [HttpGet("user/{userId:int}")]
        public async Task<IActionResult> GetByUserId(int userId) {
            List<OrderHeaderDto> Order = await orderService.GetByUserId(userId);
            return Ok(Order);
        }


        // GET api/<OrderController>/5
        [HttpGet("user/{userId:int}/{month:int}/{year:int}")]
        public async Task<IActionResult> GetByMonthAndYear(int userId, int month, int year) {
            List<OrderHeaderDto> Order = await orderService.GetByMonthAndYear(userId, month, year);
            return Ok(Order);
        }

        [HttpGet("user/{userId:int}/get-range/{firstDate:datetime}/{lastDate:datetime}")]
        public async Task<IActionResult> GetByRange(int userId, DateTime firstDate, DateTime lastDate) {
            List<OrderHeaderDto> Order = await orderService.GetByRange(userId, firstDate, lastDate);
            return Ok(Order);
        }


        // GET api/<OrderController>/5
        [HttpGet("user/{userId:int}/{date:datetime}")]
        public async Task<IActionResult> GetByExactDate(int userId, DateTime date) {
            List<OrderHeaderDto> Order = await orderService.GetByExactDate(userId, date);
            return Ok(Order);
        }

        // GET api/<OrderController>/5
        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id) {
            OrderHeaderDto? Order = await orderService.Get(id);
            return Order == null ? NotFound() : Ok(Order);
        }

        // POST api/<OrderController>
        [HttpPost("{userId:int}")]
        public async Task<IActionResult> Insert(int userId, [FromBody] OrderHeaderDto Order) {
            return Ok(await orderService.Insert(userId, Order));

        }

        //// PUT api/<OrderController>/5
        //[HttpPut("{id:int}")]
        //public async Task<IActionResult> Put(int id, [FromBody] OrderDto Order) {
        //    return Ok(await orderService.Put(id, Order));
        //}

        // DELETE api/<OrderController>/5
        [HttpDelete("{userId:int}/{id:int}")]
        public async Task<IActionResult> Delete(int userId, int id) {
            await orderService.Delete(userId, id);
            return Ok(new { status = 200 });
        }
    }
}
