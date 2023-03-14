using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.ComponentModel.DataAnnotations;

namespace RiversideFishhut.API.Data
{
    public class Staff
    {
        [Key]
        public int StaffId { get; set; }
        public string roleId { get; set; }
        public string StaffName { get; set; }
        public string Description { get; set; }
        public string Password { get; set; }
    }
}
