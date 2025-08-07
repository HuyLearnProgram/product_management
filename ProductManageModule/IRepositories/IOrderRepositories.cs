using ProductManagementModule.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagementModule.IRepositories
{
    public interface IOrderRepositories
    {
        public Order CreateOrder(long userId, string address, string phone, DateTime? deliveryTime, decimal totalPrice, List<Cart> cartItems);
        public Order GetOrderById(long orderId);
        public void UpdateOrderStatus(long orderId, int newStatus);
        public IEnumerable<Order> GetOrderByUserId(long userId);

    }
}
