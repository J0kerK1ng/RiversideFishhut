using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RiversideFishhut.API.Data
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }
        public string Description { get; set; }
        public string TypeName { get; set; }

        public virtual IList<Product> Products { get; set;}


		[ForeignKey(nameof(FoodTypeId))]
		public int FoodTypeId { get; set; }

		public FoodType foodType { get; set; }

		public virtual ICollection<CategoryFoodType> CategoryFoodTypes { get; set; }

	}
}