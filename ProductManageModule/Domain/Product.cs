using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ProductManagementModule.Domain
{
    [Table("products")]
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Column(TypeName = "mediumtext")]
        public string? Description { get; set; }

        [Column("image_url")]
        [MaxLength(255)]
        public string? ImageUrl { get; set; }

        public double Price { get; set; }

        [Column("product_name")]
        [Required]
        [MaxLength(255)]
        public string? ProductName { get; set; }

        public int? Quantity { get; set; }

        [MaxLength(20)]
        public string? Unit { get; set; }


        public double? Rating { get; set; }

        public int? Sold { get; set; }



        // Navigation property to Cart
        public ICollection<Cart>? Carts { get; set; }
        public ICollection<OrderDetail>? OrderDetails { get; set; }

    }
}

