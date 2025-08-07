//using Microsoft.AspNetCore.Mvc;
//using ProductManagementModule.Services;

//namespace ProductClient.Controllers
//{
//    public class CartController:Controller
//    {
//        private readonly CartService _cartService;
//        //private const int PageSize = 18;
//        public CartController(CartService cartService)
//        {
//            _cartService = cartService;
//        }
//        [HttpGet]
//        [Route("/cart/index")]
//        public IActionResult Index()
//        {
//            // Lấy thông tin giỏ hàng của người dùng (ví dụ: dựa trên User ID từ session/cookie/authentication)
//            long userId = GetCurrentUserId(); // Hàm này bạn cần triển khai

//            var cartItems = _cartService.GetCartItems(userId);
//            return View(cartItems); // Bạn cần tạo một View tương ứng
//        }

//        // POST: /cart/add/{productId}
//        [HttpPost]
//        [Route("/cart/add/{productId}")]
//        public IActionResult AddToCart(long productId, int quantity = 1)
//        {
//            long userId = GetCurrentUserId(); // Lấy User ID

//            _cartService.AddToCart(userId, productId, quantity);

//            // Chuyển hướng người dùng đến trang giỏ hàng hoặc trang sản phẩm
//            return RedirectToAction("Index");
//        }

//        // POST: /cart/update
//        [HttpPost]
//        [Route("/cart/update")]
//        public IActionResult UpdateCart(List<CartItemViewModel> cartItems)
//        {
//            long userId = GetCurrentUserId();

//            _cartService.UpdateCart(userId, cartItems);

//            return RedirectToAction("Index");
//        }

//        // GET: /cart/remove/{productId}
//        [HttpGet]
//        [Route("/cart/remove/{productId}")]
//        public IActionResult RemoveFromCart(long productId)
//        {
//            long userId = GetCurrentUserId();

//            _cartService.RemoveFromCart(userId, productId);

//            return RedirectToAction("Index");
//        }

//        // GET: /cart/clear
//        [HttpGet]
//        [Route("/cart/clear")]
//        public IActionResult ClearCart()
//        {
//            long userId = GetCurrentUserId();

//            _cartService.ClearCart(userId);

//            return RedirectToAction("Index");
//        }

//        // Hàm giả định để lấy User ID hiện tại (bạn cần triển khai logic thực tế)
//        private long GetCurrentUserId()
//        {
//            // Đây chỉ là một ví dụ, bạn cần lấy User ID từ hệ thống xác thực của mình
//            // Ví dụ: từ User.Identity.GetUserId() nếu bạn đang sử dụng ASP.NET Identity
//            return 1; // Thay thế bằng logic thực tế
//        }
//    }
//}

using Microsoft.AspNetCore.Mvc;
using ProductClient.ViewModels; // Có thể bạn sẽ cần một ViewModel cho Cart
using ProductManagementModule.Domain;
using ProductManagementModule.Services;

public class CartController : Controller
{
    private readonly CartService _cartService;

    public CartController(CartService cartService)
    {
        _cartService = cartService;
    }

    // GET: /cart/index
    [HttpGet]
    [Route("/cart/index")]
    public IActionResult Index()
    {
        // Lấy thông tin giỏ hàng của người dùng
        long userId = GetCurrentUserId(); // Hàm này bạn cần triển khai

        var cartItems = _cartService.GetCartByUserId(userId);
        var model = new CartViewModel {
            cartItems = (List<Cart>)cartItems
        };
        //return View(cartItems); // Bạn cần tạo một View tương ứng để hiển thị danh sách Cart
        return View("Cart",model);
    }

    // POST: /cart/add/{productId}
    [HttpPost]
    [Route("/cart/add/{productId}")]
    public IActionResult AddToCart(long productId, int quantity = 1)
    {
        long userId = GetCurrentUserId();

        _cartService.AddToCart(userId, productId, quantity);

        return RedirectToAction("Index");
    }

    // POST: /cart/update
    [HttpPost]
    [Route("/cart/update")]
    public IActionResult UpdateCart(List<Cart> cartItems) // Hoặc bạn có thể tạo một ViewModel phù hợp
    {
        long userId = GetCurrentUserId();

        foreach (var item in cartItems)
        {
            _cartService.UpdateQuantity(userId, item.ProductId, item.Quantity);
        }

        return RedirectToAction("Index");
    }

    // GET: /cart/remove/{productId}
    [HttpGet]
    [Route("/cart/remove/{productId}")]
    public IActionResult RemoveFromCart(long productId)
    {
        long userId = GetCurrentUserId();

        _cartService.RemoveFromCart(userId, productId);

        return RedirectToAction("Index");
    }

    // Bạn có thể thêm các action khác như xem chi tiết giỏ hàng, thanh toán, v.v.

    private long GetCurrentUserId()
    {
        // Logic để lấy User ID hiện tại.
        // Ví dụ (cần điều chỉnh theo hệ thống xác thực của bạn):
        // return long.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        return 1; // Giá trị mặc định hoặc logic tạm thời
    }
}