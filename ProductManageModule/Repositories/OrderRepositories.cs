using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductManagementModule.Context;
using ProductManagementModule.Domain;
using ProductManagementModule.IRepositories;
namespace ProductManagementModule.Repositories
{
    public class OrderRepositories : IOrderRepositories
    {
        private readonly AppDbContext _dbContext;

        public OrderRepositories(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public Order CreateOrder(long userId, string address, string phone, DateTime? deliveryTime, decimal totalPrice, List<Cart> cartItems)
        {
            var order = new Order
            {
                Address = address,
                Phone = phone,
                DeliveryTime = deliveryTime,
                OrderTime = DateTime.Now,
                Status = 0, // Ví dụ: 0 là trạng thái "Đang xử lý"
                TotalPrice = totalPrice,
                UserId = userId
            };

            _dbContext.orders.Add(order);
            _dbContext.SaveChanges();
            return order;
        }

        public Order GetOrderById(long orderId)
        {
            return _dbContext.orders.Find(orderId);
        }

        //public Order GetOrderByUserId(long userId)
        //{
        //    return _dbContext.orders.
        //}

        public void UpdateOrderStatus(long orderId, int newStatus)
        {
            var order = _dbContext.orders.Find(orderId);

            if (order != null)
            {
                order.Status = newStatus;
                _dbContext.SaveChanges();
            }
        }

        IEnumerable<Order> GetOrderByUserId(long userId)
        {
            return _dbContext.orders
                     .Where(o => o.UserId == userId)
                     .OrderByDescending(o => o.OrderTime)
                     .ToList(); ;
        }

        IEnumerable<Order> IOrderRepositories.GetOrderByUserId(long userId)
        {
            return GetOrderByUserId(userId);
        }
    }
}
