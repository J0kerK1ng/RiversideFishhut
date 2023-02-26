using System.ComponentModel.DataAnnotations;

namespace RiversideFishhut.API.Data
{
    public class Admin
    {
        [Key]
        public int AdminId { get; set; }
        public string AdminName { get; set; }
        public string AdminAddress { get; set; }
        public string AdminPassword { get; set; }

    }
}
