using System.ComponentModel.DataAnnotations;

namespace BARBERSHOP_V2.Entity
{
    public class Category
    {
        [Key]
        public int cateID { get; set; }
        public string? cateName { get; set; }


        #region QH

        //OnetoMany product
        public ICollection<Product> products { get; } = new HashSet<Product>();

        #endregion
    }
}
