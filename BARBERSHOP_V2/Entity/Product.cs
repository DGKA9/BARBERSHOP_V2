using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BARBERSHOP_V2.Entity
{
    public class Product
    {
        [Key]
        public int proID { get; set; }
        public string? proName { get; set; }
        public string? proImage { get; set; }
        public float price { get; set; }
        public int quantity { get; set; }
        public string? proDescription { get; set;}



        #region QH

        // MO
        // producer
        public int producerID { get; set; }
        [ForeignKey("producerID")]
        public Producer Producer { get; set; } = null!;
        // warehouse
        public int warehouseID { get; set; }
        [ForeignKey("warehouseID")]
        public Warehouse Warehouse { get; set; } = null!;
        // category
        public int cateID { get; set; }
        [ForeignKey("cateID")]
        public Category category { get; set; } = null!;

        // OM productOrder
        public ICollection<ProductOrder> ProductOrders { get; } = new HashSet<ProductOrder>();
        #endregion
    }
}
