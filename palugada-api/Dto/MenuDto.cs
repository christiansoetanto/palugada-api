using System.Text.Json.Serialization;

namespace palugada_api.Dto {
    public class MenuDto {
        public string? Name { get; set; }
        public int Price { get; set; }
        public int MenuId { get; set; }
    }
}
