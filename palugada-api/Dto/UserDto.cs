using System.Text.Json.Serialization;

namespace palugada_api.Dto {
    public class UserDto {
        public string Username { get; set; }
        public string Password { get; set; }
        public int UserId { get; set; }
    }
}
