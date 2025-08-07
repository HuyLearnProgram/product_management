using ProductManagementModule.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagementModule.IRepositories
{
    public interface IOrderDetailRepositories
    {
        //IEnumerable<OrderDetail> GetAll();
        OrderDetail GetById(long orderId);
        IEnumerable<OrderDetail> GetByOrderId(long orderId);
        void AddRange(IEnumerable<OrderDetail> orderDetails);
        //void Update(OrderDetail orderDetail);
        //void Delete(long orderId, long productId);
    }
}
