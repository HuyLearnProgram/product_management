using Microsoft.EntityFrameworkCore;
using ProductManagementModule.Domain;
using ProductManagementModule.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagementModule.Services
{
    public class OrderDetailService
    {
        private readonly IOrderDetailRepositories _orderDetailRepositories;
        public OrderDetailService(IOrderDetailRepositories orderDetailRepositories)
        {
            _orderDetailRepositories = orderDetailRepositories;
        }
        public void AddRange(IEnumerable<OrderDetail> orderDetails)
        {
            _orderDetailRepositories.AddRange(orderDetails);
        }
        public OrderDetail GetById(long orderId)
        {
            return _orderDetailRepositories.GetById(orderId);

        }
        public IEnumerable<OrderDetail> GetByOrderId(long orderId)
        {
            return _orderDetailRepositories.GetByOrderId(orderId);
        }
    }
}
