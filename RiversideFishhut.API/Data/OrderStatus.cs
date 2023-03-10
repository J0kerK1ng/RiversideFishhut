using System.ComponentModel.DataAnnotations;

namespace RiversideFishhut.API.Data
{
    public class OrderStatus
    {
        [Key]
        public int OrderStatusId { get; set; }

        public string OrderStatusName { get; set; }
    }
}
