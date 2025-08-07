using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagementModule.Domain
{
    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public long Id { get; set; }

        [Column("address", TypeName = "varchar(255)")]
        public string Address { get; set; }

        [Column("phone", TypeName = "varchar(255)")]
        public string Phone { get; set; }

        [Column("delivery_time", TypeName = "datetime(6)")]
        public DateTime? DeliveryTime { get; set; }

        [Column("order_time", TypeName = "datetime(6)")]
        public DateTime OrderTime { get; set; }

        [Column("status")]
        public int Status { get; set; }

        [Column("total_price", TypeName = "decimal(10, 2)")]
        public decimal TotalPrice { get; set; }

        [Column("user_id", TypeName = "BIGINT")]
        public long UserId { get; set; }

        //public ICollection<User> Users { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }

        public ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
