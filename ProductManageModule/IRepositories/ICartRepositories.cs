using ProductManagementModule.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagementModule.IRepositories
{
    public interface ICartRepositories
    {
        void AddToCart(long userId, long productId, int quantity);
        List<Cart> GetCartByUserId(long userId);
        void UpdateQuantity(long userId, long productId, int newQuantity);
        void RemoveFromCart(long userId, long productId);
        void RemoveCartByUserId(long userId);
    }
}
