using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RiversideFishhut.API.Data
{
    public class WebsiteInfo
    {
        [Key]
        public int InfoId { get; set; }

        public string StoreName { get; set; }

        public string LogoImage { get; set; }

        public string Description { get; set; }
		public string Address { get; set; }
		public string PhoneNumber { get; set; }
        public string OnlineOrderLink { get; set; }
        

	}
}
