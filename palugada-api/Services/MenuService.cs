using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using palugada_api.Dto;
using palugada_api.Entities;

namespace palugada_api.Services {
    public class MenuService {
        private readonly PalugadaDbContext dbContext;

        public MenuService(PalugadaDbContext dbContext) {
            this.dbContext = dbContext;
        }

        public async Task<MenuDto?> Get(int id)
        {
            return await dbContext.Menu.Where(e => e.MenuId == id).Select(e => new MenuDto() {
                MenuId = e.MenuId,
                Name = e.Name,
                Price = e.Price
            }).FirstOrDefaultAsync();
            
        }


        public async Task<MenuDto> Insert(int userId, MenuDto menu)
        {
            Menu inserted = (await dbContext.Menu.AddAsync(new Menu()
            {
                Price = menu.Price,
                Name = menu.Name,
                UserId = userId
            })).Entity;
            await dbContext.SaveChangesAsync();

            MenuDto insertedMenuDto = new()
            {
                Name = inserted.Name,
                Price = inserted.Price,
                MenuId = inserted.MenuId
            };
                
            return insertedMenuDto;
        }

        public async Task Delete(int id)
        {
            Menu menu = await  dbContext.Menu.FindAsync(id) ?? throw new NullReferenceException();
            dbContext.Menu.Remove(menu);
            await dbContext.SaveChangesAsync();
        }

        public async Task<MenuDto> Put(int id, MenuDto menuDto)
        {
            Menu menu = await dbContext.Menu.FindAsync(id) ?? throw new NullReferenceException();
            menu.Name = menuDto.Name;
            menu.Price = menuDto.Price;
            await dbContext.SaveChangesAsync();
            return new MenuDto
            {
                MenuId = menu.MenuId,
                Name = menu.Name,
                Price = menu.Price
            };
        }

        public async Task<List<MenuDto>> GetByUserId(int userId)
        {
            return await dbContext.Menu.Where(e => e.UserId == userId).Select(e => new MenuDto() {
                MenuId = e.MenuId,
                Name = e.Name,
                Price = e.Price
            }).ToListAsync();
        }
    }
   
}
