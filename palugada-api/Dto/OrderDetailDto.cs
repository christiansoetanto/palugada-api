using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;
using palugada_api.Entities;

namespace palugada_api.Dto {
    public class OrderDetailDto {
        public int OrderDetailId { get; set; }
        public int Amount { get; set; }
        public int OrderHeaderId { get; set; }
        public int MenuId { get; set; }
        public MenuDto? Menu { get; set; } = new();
    }
}
