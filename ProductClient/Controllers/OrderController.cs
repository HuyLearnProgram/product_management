
//using global::ProductManagementModule.Domain;
//using global::ProductManagementModule.Services;
//using Microsoft.AspNetCore.Mvc;
//using ProductManagementModule.Domain;
//using ProductManagementModule.Services;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Security.Claims;
//using System.Threading.Tasks;
//namespace ProductClient.Controllers;
//{
//    public class OrderController : Controller
//    {
//        private readonly OrderService _orderService;

//        public OrderController(OrderService orderService)
//        {
//            _orderService = orderService;
//        }

//        // GET: /Order/Details/{id}
//        public IActionResult Details(long id)
//        {
//            var order = _orderService.GetOrderById(id);
//            if (order == null)
//            {
//                return NotFound();
//            }
//            return View(order);
//        }

//        // POST: /Order/Create
//        [HttpPost]
//        public IActionResult Create(string address, string phone, DateTime? deliveryTime, decimal totalPrice, [FromBody] List<Cart> cartItems)
//        {
//            // Lấy ID người dùng từ Claims (ví dụ: sau khi xác thực)
//            if (!User.Identity.IsAuthenticated)
//            {
//                return Unauthorized(); // Hoặc chuyển hướng đến trang đăng nhập
//            }
//            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
//            if (userIdClaim == null || !long.TryParse(userIdClaim.Value, out long userId))
//            {
//                return BadRequest("Không tìm thấy ID người dùng hợp lệ.");
//            }

//            try
//            {
//                _orderService.CreateOrder(userId, address, phone, deliveryTime, totalPrice, cartItems);
//                // Chuyển hướng đến trang xác nhận đơn hàng hoặc danh sách đơn hàng
//                return RedirectToAction("OrderConfirmation");
//            }
//            catch (Exception ex)
//            {
//                // Ghi log lỗi
//                ModelState.AddModelError(string.Empty, "Đã xảy ra lỗi khi tạo đơn hàng.");
//                // Có thể trả về View với thông báo lỗi hoặc BadRequest
//                return BadRequest(ex.Message);
//            }
//        }

//        // GET: /Order/OrderConfirmation
//        public IActionResult OrderConfirmation()
//        {
//            return View(); // Tạo một view để hiển thị thông báo xác nhận đơn hàng
//        }

//        // POST: /Order/UpdateStatus/{orderId}
//        [HttpPost]
//        public IActionResult UpdateStatus(long orderId, int newStatus)
//        {
//            try
//            {
//                _orderService.UpdateOrderStatus(orderId, newStatus);
//                return Ok(); // Trả về trạng thái thành công
//            }
//            catch (Exception ex)
//            {
//                // Ghi log lỗi
//                return BadRequest(ex.Message);
//            }
//        }
//    }
//}
using global::ProductManagementModule.Domain;
using global::ProductManagementModule.Services;
using Microsoft.AspNetCore.Mvc;
using ProductManagementModule.Domain;
using ProductManagementModule.IRepositories;
using ProductManagementModule.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ProductClient.Controllers;

public class OrderController : Controller
{
    private readonly OrderService _orderService;
    private readonly CartService _cartService;
    private readonly OrderDetailService _orderDetailService;

    public OrderController(OrderService orderService, CartService cartService, OrderDetailService orderDetailService)
    {
        _orderService = orderService;
        _cartService = cartService;
        _orderDetailService = orderDetailService;
    }

    // GET: /Order/Details/{id}
    [HttpGet("Order/Details/{id}")]
    public IActionResult Details(long id)
    {
        //var order = _orderService.GetOrderById(id);
        //var orderDetail = _orderDetailService.GetById(id);
        var orderDetails = _orderDetailService.GetByOrderId(id);
        if (orderDetails == null)
        {
            return NotFound();
        }
        return View("OrderDetail",orderDetails);
    }

    [HttpPost("Order/Create")]
    public IActionResult Create(string address, string phone, DateTime? deliveryTime, decimal totalPrice)
    {
        // Lấy ID người dùng từ Claims (ví dụ: sau khi xác thực)
        //if (!User.Identity.IsAuthenticated)
        //{
        //    return Unauthorized(); // Hoặc chuyển hướng đến trang đăng nhập
        //}
        //var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
        //if (userIdClaim == null || !long.TryParse(userIdClaim.Value, out long userId))
        //{
        //    return BadRequest("Không tìm thấy ID người dùng hợp lệ.");
        //}
        long userId = GetCurrentUserId();
        //userId = 1;

        try
        {
            List<Cart> cartItems = _cartService.GetCartByUserId(GetCurrentUserId());
            Order newOrder = _orderService.CreateOrder(userId, address, phone, deliveryTime, totalPrice, cartItems);
            List<OrderDetail> orderDetailItems = new List<OrderDetail>();
            foreach (Cart cartItem in cartItems)
            {
                orderDetailItems.Add(new OrderDetail
                {
                    OrderId = newOrder.Id,
                    ProductId = cartItem.ProductId,
                    Quantity = cartItem.Quantity,
                    //Price = cartItem.Product?.Price ?? 0
                });
            }
            _orderDetailService.AddRange(orderDetailItems);
            _cartService.RemoveCartByUserId(userId);
            return UserOrder();
        }
        catch (Exception ex)
        {
            // Ghi log lỗi
            ModelState.AddModelError(string.Empty, "Đã xảy ra lỗi khi tạo đơn hàng.");
            // Có thể trả về View với thông báo lỗi hoặc BadRequest
            return BadRequest(ex.Message);
        }
    }
    [HttpGet("Order/OrderHistory")]
    public IActionResult UserOrder()
    {
        long userId = GetCurrentUserId();
        var model = _orderService.GetOrderByUserId(userId);
        return View("UserOrder", model);
    }

    //[H]


    [HttpGet("Order/OrderConfirmation")]
    public IActionResult OrderConfirmation()
    {
        return View(); // Tạo một view để hiển thị thông báo xác nhận đơn hàng
    }

    [HttpPost("Order/UpdateStatus/{orderId}")]
    public IActionResult UpdateStatus(long orderId, int newStatus)
    {
        try
        {
            _orderService.UpdateOrderStatus(orderId, newStatus);
            return Ok(); // Trả về trạng thái thành công
        }
        catch (Exception ex)
        {
            // Ghi log lỗi
            return BadRequest(ex.Message);
        }
    }
    //public OrderDetailService(IOrderDetailRepositories orderDetailRepositories)
    //{
    //    _orderDetailRepositories = orderDetailRepositories;
    //}
    //public void AddRange(IEnumerable<OrderDetail> orderDetails)
    //{
    //    _orderDetailRepositories.AddRange(orderDetails);
    //}
    //public OrderDetail GetById(long orderId)
    //{
    //    return _orderDetailRepositories.GetById(orderId);

    //}
    private long GetCurrentUserId()
    {
        // Logic để lấy User ID hiện tại.
        // Ví dụ (cần điều chỉnh theo hệ thống xác thực của bạn):
        // return long.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        return 1; // Giá trị mặc định hoặc logic tạm thời
    }
}