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
    public class CartService
    {
        private readonly ICartRepositories _cartRepository;

        public CartService(ICartRepositories cartRepository)
        {
            _cartRepository = cartRepository;
        }

        public void AddToCart(long userId, long productId, int quantity)
        {
            _cartRepository.AddToCart(userId, productId, quantity);
        }
        public List<Cart> GetCartByUserId(long userId)
        {
            return _cartRepository.GetCartByUserId(userId);
        }
        public void UpdateQuantity(long userId, long productId, int newQuantity)
        {
            _cartRepository.UpdateQuantity(userId, productId, newQuantity);
        }
        public void RemoveFromCart(long userId, long productId)
        {
            _cartRepository.RemoveFromCart(userId, productId);
        }
        public void RemoveCartByUserId(long userId)
        {
            _cartRepository.RemoveCartByUserId(userId);
        }
    }
}
