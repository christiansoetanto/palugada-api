using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using palugada_api.Dto;
using palugada_api.Entities;

namespace palugada_api.Services {
    public class OrderService {
        private readonly PalugadaDbContext dbContext;

        public OrderService(PalugadaDbContext dbContext) {
            this.dbContext = dbContext;
        }

      
        public async Task<OrderHeaderDto?> Get(int id) {
            return await dbContext.OrderHeader
                .Include(e => e.OrderDetail)
                .Where(e => e.OrderHeaderId == id)
                .Select(e => new OrderHeaderDto {
                    OrderHeaderId = e.OrderHeaderId,
                    UserId = e.UserId,
                    Title = e.Title,
                    Date = e.Date,

                    OrderDetail = e.OrderDetail.Select(od => new OrderDetailDto() {
                        MenuId = od.MenuId,
                        OrderHeaderId = od.OrderHeaderId,
                        Amount = od.Amount,
                        OrderDetailId = od.OrderDetailId
                    }).ToList()
                }).FirstOrDefaultAsync();

        }


        public async Task<OrderHeaderDto> Insert(int userId, OrderHeaderDto order) {
            OrderHeader insertedOrderHeader = (await dbContext.OrderHeader.AddAsync(new OrderHeader() {
                Title = order.Title,
                UserId = userId,
                Date = order.Date
                
            })).Entity;
            await dbContext.SaveChangesAsync();
            List<OrderDetail> orderDetails = order.OrderDetail.Select(e => new OrderDetail()
            {
                MenuId = e.MenuId,
                Amount = e.Amount,
                OrderHeaderId = insertedOrderHeader.OrderHeaderId,
            }).ToList();
            await dbContext.OrderDetail.AddRangeAsync(orderDetails);
            await dbContext.SaveChangesAsync();

            OrderHeaderDto insertedOrderDto = new() {
                OrderHeaderId = insertedOrderHeader.OrderHeaderId
            };

            return insertedOrderDto;
        }

        //public async Task Delete(int id)
        //{
        //    Order Order = await  dbContext.Order.FindAsync(id) ?? throw new NullReferenceException();
        //    dbContext.Order.Remove(Order);
        //    await dbContext.SaveChangesAsync();
        //}

        //public async Task<OrderDto> Put(int id, OrderDto OrderDto)
        //{
        //    Order Order = await dbContext.Order.FindAsync(id) ?? throw new NullReferenceException();
        //    Order.Name = OrderDto.Name;
        //    Order.Price = OrderDto.Price;
        //    await dbContext.SaveChangesAsync();
        //    return new OrderDto
        //    {
        //        OrderId = Order.OrderId,
        //        Name = Order.Name,
        //        Price = Order.Price
        //    };
        //}

        //public async Task<List<OrderDto>> GetByUserId(int userId)
        //{
        //    return await dbContext.Order.Where(e => e.UserId == userId).Select(e => new OrderDto() {
        //        OrderId = e.OrderId,
        //        Name = e.Name,
        //        Price = e.Price
        //    }).ToListAsync();
        //}
        public async Task<List<OrderHeaderDto>> GetByUserId(int userId)
        {
            return await dbContext.OrderHeader
                .Include(e => e.OrderDetail)
                .Where(e => e.UserId == userId)
                .Select(e => new OrderHeaderDto {
                    OrderHeaderId = e.OrderHeaderId,
                    UserId = e.UserId,
                    Title = e.Title,
                    Date = e.Date,

                    OrderDetail = e.OrderDetail.Select(od => new OrderDetailDto() {
                        MenuId = od.MenuId,
                        OrderHeaderId = od.OrderHeaderId,
                        Amount = od.Amount,
                        OrderDetailId = od.OrderDetailId
                    }).ToList()
                }).ToListAsync();

        }

        public async Task<List<OrderHeaderDto>> GetByMonthAndYear(int userId, int month, int year)
        {
            return await dbContext.OrderHeader
                .Where(e => e.UserId == userId)
                .Where(e => e.Date.Month == month + 1)
                .Where(e => e.Date.Year == year)
                .Select(e => new OrderHeaderDto {
                    OrderHeaderId = e.OrderHeaderId,
                    UserId = e.UserId,
                    Title = e.Title,
                    Date = e.Date,
                }).ToListAsync();
        }

        public async Task<List<OrderHeaderDto>> GetByRange(int userId, DateTime firstDate, DateTime lastDate) {
            return await dbContext.OrderHeader
                .Where(e => e.UserId == userId)
                .Where(e => firstDate <= e.Date && e.Date <= lastDate)
                .Select(e => new OrderHeaderDto {
                    OrderHeaderId = e.OrderHeaderId,
                    UserId = e.UserId,
                    Title = e.Title,
                    Date = e.Date,
                }).ToListAsync();
        }
        public async Task<List<OrderHeaderDto>> GetByExactDate(int userId, DateTime date) {
            return await dbContext.OrderHeader
                .Include(e => e.OrderDetail)
                .ThenInclude(od => od.Menu)
                .Where(e => e.UserId == userId)
                .Where(e => e.Date == date)
                .Select(e => new OrderHeaderDto {
                    OrderHeaderId = e.OrderHeaderId,
                    UserId = e.UserId,
                    Title = e.Title,
                    Date = e.Date,
                    OrderDetail = e.OrderDetail.Select(od => new OrderDetailDto() {
                        MenuId = od.MenuId,
                        OrderHeaderId = od.OrderHeaderId,
                        Amount = od.Amount,
                        OrderDetailId = od.OrderDetailId,
                        Menu =  new MenuDto
                        {
                            MenuId = od.MenuId,Name = od.Menu.Name, Price = od.Menu.Price
                        }
                    }).ToList()
                }).ToListAsync();
        }

        public async Task Delete(int userId, int id)
        {
            OrderHeader orderHeader = await dbContext.OrderHeader.Where(e => e.UserId == userId).Where(e => e.OrderHeaderId == id)
                .FirstOrDefaultAsync() ?? throw new NullReferenceException("No order found");
            List<OrderDetail> orderDetails = await dbContext.OrderDetail
                .Where(e => e.OrderHeaderId == orderHeader.OrderHeaderId).ToListAsync();
             dbContext.RemoveRange(orderDetails);
             dbContext.Remove(orderHeader);
             await dbContext.SaveChangesAsync();
        }
    }

}
