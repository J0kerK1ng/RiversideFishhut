using System.ComponentModel.DataAnnotations;

namespace RiversideFishhut.API.Data
{
    public class WebsiteInfo
    {
        [Key]
        public int InfoId { get; set; }

        public string StoreName { get; set; }

        public string Description { get; set; }
        public string Phone { get; set; }
        public string OnlineOrderLink { get; set; }
        public string Address { get; set; }
        public string BusinessHour { get; set; }

    }
}
