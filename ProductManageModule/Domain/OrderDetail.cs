//using Microsoft.EntityFrameworkCore;
//using System;
//using System.Collections;
//using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations.Schema;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace ProductManagementModule.Domain
//{
//    [PrimaryKey(nameof(OrderId), nameof(ProductId))]
//    public class OrderDetail
//    {
//        [Column("order_id")]
//        [ForeignKey("Orders")]
//        public long OrderId { get; set; }
//        [Column("product_id")]
//        [ForeignKey("Product")]
//        public long ProductId { get; set; }
//            public int Quantity { get; set; }

//            public ICollection <Order> Order { get; set; }
//            public ICollection <Product> Product { get; set; }

//    }
//}

using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductManagementModule.Domain
{
    [PrimaryKey(nameof(OrderId), nameof(ProductId))]
    [Table("order_detail")] // Nếu bạn có tên bảng cụ thể trong DB
    public class OrderDetail
    {
        [Column("order_id")]
        public long OrderId { get; set; }

        [Column("product_id")]
        public long ProductId { get; set; }

        [Column("quantity")]
        public int Quantity { get; set; }

        // Navigation properties
        //[ForeignKey(nameof(OrderId))]
        [ForeignKey("OrderId")]
        public Order Order { get; set; }

        //[ForeignKey(nameof(ProductId))]
        [ForeignKey("ProductId")]
        public Product Product { get; set; }
    }
}
