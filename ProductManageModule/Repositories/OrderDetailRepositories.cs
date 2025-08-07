using Microsoft.EntityFrameworkCore;
using ProductManagementModule.Context;
using ProductManagementModule.Domain;
using ProductManagementModule.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagementModule.Repositories
{
    public class OrderDetailRepositories : IOrderDetailRepositories
    {
        private readonly AppDbContext _dbContext;

        public OrderDetailRepositories(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void AddRange(IEnumerable<OrderDetail> orderDetails)
        {
            _dbContext.order_detail.AddRange(orderDetails);
            _dbContext.SaveChanges();
        }

        public OrderDetail GetById(long orderId)
        {
            //var cartItem = _dbContext.cart.FirstOrDefault(c => c.UserId == userId && c.ProductId == productId);
            return _dbContext.order_detail.FirstOrDefault(od => od.OrderId == orderId);
        }

        public IEnumerable<OrderDetail> GetByOrderId(long orderId)
        {
            return _dbContext.order_detail
                    .Include(od => od.Order)
                    .Include(od => od.Product)
                    .Where(od => od.OrderId == orderId)
                    .ToList();
        }
    }
}
