using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ProductManagementModule.Domain
{
    [PrimaryKey(nameof(UserId), nameof(ProductId))]
    public class Cart
    {
        //[Key]
        [Column("user_id")]
        [ForeignKey("User")]
        public long UserId { get; set; }

        //[Key]
        [Column("product_id")]
        [ForeignKey("Product")]
        public long ProductId { get; set; }

        [Column("quantity")]
        public int Quantity { get; set; }

        // Navigation properties
        public User User { get; set; }
        public Product Product { get; set; }
    }
}
