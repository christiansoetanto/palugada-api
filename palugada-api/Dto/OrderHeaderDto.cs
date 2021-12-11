using System.Text.Json.Serialization;

namespace palugada_api.Dto {
    public class OrderHeaderDto {
        public string Title { get; set; }
        public int OrderHeaderId { get; set; }
        public DateTime Date { get; set; }

        public int UserId { get; set; }
        public List<OrderDetailDto> OrderDetail { get; set; }

    }
}
