using Microsoft.EntityFrameworkCore;
using ProductManagementModule.Repositories;
using ProductManagementModule.Context;
using ProductManagementModule.Services;
using ProductManagementModule.IRepositories;
var builder = WebApplication.CreateBuilder(args);


// Đăng ký DbContext với chuỗi kết nối từ appsettings.json

//builder.Services.AddDbContext<AppDbContext>(options =>
//    options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),
//    ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection"))));

//string connectionString = "Server=localhost;Port=3303;Database=Product;User=root;Password=123456;SslMode=Required";
string connectionString = "Server=localhost;Port=3306;Database=product_management;User=root;Password=123456;SslMode=Required";
//string connectionString = ""; //Xóa vì sợ bị phá
// Configure DbContext with MySQL connection string
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(
        connectionString,
        ServerVersion.AutoDetect(connectionString))
);

// Đăng ký các dịch vụ
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ProductService>(); // Đảm bảo dòng này có mặt và sử dụng namespace đúng

builder.Services.AddScoped<ICartRepositories, CartRepositories>();
builder.Services.AddScoped<CartService>();

builder.Services.AddScoped<IOrderRepositories, OrderRepositories>();
builder.Services.AddScoped<OrderService>();

builder.Services.AddScoped<IOrderDetailRepositories, OrderDetailRepositories>();
builder.Services.AddScoped<OrderDetailService>();

builder.Services.AddControllersWithViews();  // Đăng ký MVC controllers

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapGet("/", () => Results.Redirect("/products/view/1"));

app.MapControllerRoute(
    name: "addProduct",
    pattern: "product/add",
    defaults: new {controller = "Product", action = "AddProductView"}
    );

app.MapControllerRoute(
    name: "productView",
    pattern: "products/view/{page}",
    defaults: new { controller = "Product", action = "ProductView", page = 1 });

app.MapControllerRoute(
    name: "productDetailView",
    pattern: "product/{id}",
    defaults: new { controller = "Product", action = "ProductDetailView" });

app.Run();
