using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RiversideFishhut.API.Data
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }

        [Required(ErrorMessage = "Please select a order type.")]
        [ForeignKey(nameof(OrderTypeId))]
        public int OrderTypeId { get; set; }

        public OrderType OrderType { get; set; }

        public string? notes { get; set; }

        [ForeignKey(nameof(StaffId))]
        public int StaffId { get; set; }

        public Staff Staff { get; set; }

        public int OrderStatusId { get; set; }

        public bool PaymentStatus { get; set; }

        public DateTime OrderDate { get; set; }
    }
}
