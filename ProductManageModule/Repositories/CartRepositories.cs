using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProductManagementModule.Context;
using ProductManagementModule.Domain;
using ProductManagementModule.IRepositories;
namespace ProductManagementModule.Repositories
{
    public class CartRepositories : ICartRepositories
    {
        private readonly AppDbContext _dbContext;

        public CartRepositories(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // Phương thức thêm sản phẩm vào giỏ hàng hoặc tăng số lượng nếu đã có
        public void AddToCart(long userId, long productId, int quantity)
        {
            var cartItem = _dbContext.cart.FirstOrDefault(c => c.UserId == userId && c.ProductId == productId);

            if (cartItem != null)
            {
                cartItem.Quantity += quantity;
            }
            else
            {
                _dbContext.cart.Add(new Cart { UserId = userId, ProductId = productId, Quantity = quantity });
            }

            _dbContext.SaveChanges();
        }

        // Phương thức lấy thông tin giỏ hàng của một người dùng
        public List<Cart> GetCartByUserId(long userId)
        {
            return _dbContext.cart.Include(c => c.Product).Where(c => c.UserId == userId).ToList();
        }

        // Phương thức cập nhật số lượng sản phẩm trong giỏ hàng
        //public void UpdateQuantity(long userId, long productId, int newQuantity)
        //{
        //    var cartItem = _dbContext.cart.FirstOrDefault(c => c.UserId == userId && c.ProductId == productId);

        //    if (cartItem != null)
        //    {
        //        cartItem.Quantity = newQuantity;
        //        _dbContext.SaveChanges();
        //    }
        //}
        public void UpdateQuantity(long userId, long productId, int newQuantity)
        {
            var cartItem = _dbContext.cart.FirstOrDefault(c => c.UserId == userId && c.ProductId == productId);
            var product = _dbContext.products.FirstOrDefault(p => p.Id == productId);

            if (cartItem != null && product != null)
            {
                if (product.Quantity.HasValue && newQuantity <= product.Quantity.Value)
                {
                    cartItem.Quantity = newQuantity;
                    _dbContext.SaveChanges();
                }
                else
                {
                    // Tùy bạn xử lý nếu vượt quá số lượng: thông báo lỗi, throw exception, v.v.
                    throw new InvalidOperationException("Số lượng yêu cầu vượt quá số lượng sản phẩm còn trong kho.");
                }
            }
        }

        // Phương thức xóa sản phẩm khỏi giỏ hàng
        public void RemoveFromCart(long userId, long productId)
        {
            var cartItem = _dbContext.cart.FirstOrDefault(c => c.UserId == userId && c.ProductId == productId);

            if (cartItem != null)
            {
                _dbContext.cart.Remove(cartItem);
                _dbContext.SaveChanges();
            }
        }

        public void RemoveCartByUserId(long userId)
        {
            var userCartItems = _dbContext.cart.Where(c => c.UserId == userId).ToList();

            if (userCartItems.Any())
            {
                _dbContext.cart.RemoveRange(userCartItems);
                _dbContext.SaveChanges();
            }
        }
    }
}
