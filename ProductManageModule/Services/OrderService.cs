using ProductManagementModule.Domain;
using ProductManagementModule.IRepositories;
using ProductManagementModule.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagementModule.Services
{
    public class OrderService
    {
        private readonly IOrderRepositories _orderRepositories ;

        public OrderService(IOrderRepositories repository)
        {
            _orderRepositories = repository;
        }

        public Order CreateOrder(long userId, string address, string phone, DateTime? deliveryTime, decimal totalPrice, List<Cart> cartItems)
        {
            return _orderRepositories.CreateOrder(userId, address, phone, deliveryTime, totalPrice, cartItems);
        }
        public Order GetOrderById(long orderId)
        {
            return _orderRepositories.GetOrderById(orderId);
        }
        public void UpdateOrderStatus(long orderId, int newStatus)
        {
            _orderRepositories.UpdateOrderStatus(orderId, newStatus);
        }
        public IEnumerable<Order> GetOrderByUserId(long userId) { 
            return _orderRepositories.GetOrderByUserId(userId);
        }
    }
}
