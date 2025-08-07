
using Microsoft.AspNetCore.Mvc;
using ProductClient.ViewModels;
using ProductManagementModule.Domain;
using ProductManagementModule.Services;
using ProductManagementModule.Utils;

public class ProductController : Controller
{
    private readonly ProductService _productService;
    private const int PageSize = 18;

    public ProductController(ProductService productService)
    {
        _productService = productService;
    }

    public ActionResult Index()
    {
        var products = _productService.GetAllProducts();
        return View(products);
    }
    public ActionResult Details(long id)
    {
        var product = _productService.GetProductById(id);
        if (product == null)
            return NotFound();
        return View(product);
    }
    public ActionResult ProductView(int page = 1)
    {
        int pageSize = 18;
        var paginatedProducts = _productService.GetPaginatedProducts(page, pageSize);
        var model = new ProductViewModel
        {
            Products = (List<Product>)paginatedProducts.Products,
            CurrentPage = paginatedProducts.CurrentPage,
            TotalPages = paginatedProducts.TotalPages
        };

        return View("ProductView", model);
    }

    // GET: /product/add
    [HttpGet]
    [Route("/product/add")]
    public IActionResult AddProduct()
    {
        return View("AddProductView");
    }

    // POST: /product/add
    [HttpPost]
    [Route("/product/add")]
    public async Task<IActionResult> AddProduct(ProductViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View("AddProductView", model);
        }
        if (ModelState.IsValid)
        {
            var product = new Product
            {
                ProductName = model.ProductDetail?.ProductName,
                Price = model.ProductDetail?.Price ?? 0,
                Quantity = model.ProductDetail?.Quantity ?? 0,
                Unit = model.ProductDetail?.Unit,
                Description = model.ProductDetail?.Description,
                Rating = 0,
                Sold = 0,
            };

            if (model.ImageFile != null)
            {
                product.ImageUrl = await _productService.SaveProductImageAsync(model.ImageFile);
            }

            _productService.AddProduct(product);

            var totalProducts = _productService.GetAllProducts().Count();
            var lastPage = (int)Math.Ceiling((double)totalProducts / PageSize);
            return RedirectToAction("ProductView", new { page = lastPage });
        }

        //return View(model);
        return View("AddProductView", model);
    }

    public ActionResult ProductDetailView(int id = 1)
    {
        var product = _productService.GetProductById(id);
        var model = new ProductViewModel
        {
            ProductDetail = product,
        };
        return View("ProductDetailView", model);
    }

    [HttpPost]
    [Route("/product/Edit")]
    public async Task<IActionResult> Edit(ProductViewModel model)
    {
        if (ModelState.IsValid)
        {
            var productToUpdate = new Product
            {
                Id = model.ProductDetail.Id,
                ProductName = model.ProductDetail.ProductName,
                Price = model.ProductDetail.Price,
                Quantity = model.ProductDetail.Quantity,
                Unit = model.ProductDetail.Unit,
                Description = model.ProductDetail.Description
            };

            var updateResult = await _productService.UpdateProductAsync(productToUpdate, model.ImageFile);
            if (!updateResult)
            {
                return NotFound();
            }

            return RedirectToAction("ProductDetailView", new { id = model.ProductDetail.Id });
        }
        return RedirectToAction("ProductDetailView", new { id = model.ProductDetail.Id });
    }

    [HttpGet]
    [Route("/product/delete/{id}")]
    public IActionResult Delete(long id)
    {
        var product = _productService.GetProductById(id);
        if (product == null)
        {
            return NotFound();
        }

        _productService.DeleteProduct(id);
        var totalProducts = _productService.GetAllProducts().Count();
        var totalPages = (int)Math.Ceiling((double)totalProducts / PageSize);
        var currentPageString = HttpContext.Request.Query["page"].ToString();
        int page = string.IsNullOrEmpty(currentPageString) ? 1 : int.Parse(currentPageString);
        if (page > totalPages)
            page = totalPages > 0 ? totalPages : 1;

        return RedirectToAction("ProductView", new { page = page });
    }
}
