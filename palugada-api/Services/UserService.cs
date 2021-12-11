using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using palugada_api.Dto;
using palugada_api.Entities;

namespace palugada_api.Services {
    public class UserService {
        private readonly PalugadaDbContext dbContext;

        public UserService(PalugadaDbContext dbContext) {
            this.dbContext = dbContext;
        }

        public async Task<UserDto?> Get(int id)
        {
            return await dbContext.User
                .Where(e => e.UserId == id)
                .Select(e => new UserDto()
                {
                    Username = e.Username,
                    UserId = e.UserId
                })
                .FirstOrDefaultAsync();
        }
        public async Task<UserDto?> Login(UserDto u) {
            return await dbContext.User
                .Where(e => e.Username == u.Username)
                .Where(e => e.Password == u.Password)
                .Select(e => new UserDto() {
                    Username = e.Username,
                    UserId = e.UserId
                })
                .FirstOrDefaultAsync();
        }

        public async Task<UserDto> Register(UserDto user)
        {
            EntityEntry<User> insertedUser = await dbContext.User.AddAsync(new User()
            {
                Username = user.Username,
                Password = user.Password
            });

            await dbContext.SaveChangesAsync();

            return new UserDto
            {
                UserId = insertedUser.Entity.UserId,
                Username = insertedUser.Entity.Username
            };
        }
    }
   
}
