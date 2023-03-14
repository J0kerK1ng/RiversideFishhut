using System.ComponentModel.DataAnnotations;

namespace RiversideFishhut.API.Data
{
    public class FoodType
    {
        [Key]
        public int TypeId { get; set; }
        public string TypeName { get; set; }
        public string Description { get; set; }

        public virtual IList<Category> Categories { get; set; }

		public virtual ICollection<CategoryFoodType> CategoryFoodTypes { get; set; }
	}
}