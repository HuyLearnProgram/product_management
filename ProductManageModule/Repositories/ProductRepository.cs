using System.Data;
using System.Data.Common;
using Microsoft.EntityFrameworkCore;
using ProductManagementModule.Context;
using ProductManagementModule.Domain;
using ProductManagementModule.IRepositories;

namespace ProductManagementModule.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _context;

        public ProductRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Product> GetAllProducts()
        {
            var products = new List<Product>();

            // Execute stored procedure using EF Core
            using (var cmd = _context.Database.GetDbConnection().CreateCommand())
            {
                // Ensure connection is open (EF manages this)
                if (_context.Database.GetDbConnection().State != ConnectionState.Open)
                    _context.Database.OpenConnection();

                cmd.CommandText = "GetAllProducts";
                cmd.CommandType = CommandType.StoredProcedure;

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        products.Add(MapProduct(reader));
                    }
                }

                // Close connection explicitly (EF will handle disposal)
                _context.Database.CloseConnection();
            }

            return products;
        }

        public Product? GetProductById(long id)
        {
            using (var cmd = _context.Database.GetDbConnection().CreateCommand())
            {
                if (_context.Database.GetDbConnection().State != ConnectionState.Open)
                    _context.Database.OpenConnection();

                cmd.CommandText = "GetProductById";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(CreateParam(cmd, "@pid", id));

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return MapProduct(reader);
                    }
                }

                _context.Database.CloseConnection();
            }

            return null;
        }

        public void AddProduct(Product product)
        {
            using (var cmd = _context.Database.GetDbConnection().CreateCommand())
            {
                if (_context.Database.GetDbConnection().State != ConnectionState.Open)
                    _context.Database.OpenConnection();

                cmd.CommandText = "AddProduct";
                cmd.CommandType = CommandType.StoredProcedure;

                AddCommonProductParams(cmd, product);

                cmd.ExecuteNonQuery();

                _context.Database.CloseConnection();
            }
        }

        public void UpdateProduct(Product product)
        {
            using (var cmd = _context.Database.GetDbConnection().CreateCommand())
            {
                if (_context.Database.GetDbConnection().State != ConnectionState.Open)
                    _context.Database.OpenConnection();

                cmd.CommandText = "UpdateProduct";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(CreateParam(cmd, "@pid", product.Id));
                AddCommonProductParams(cmd, product);

                cmd.ExecuteNonQuery();

                _context.Database.CloseConnection();
            }
        }

        public void DeleteProduct(long id)
        {
            using (var cmd = _context.Database.GetDbConnection().CreateCommand())
            {
                if (_context.Database.GetDbConnection().State != ConnectionState.Open)
                    _context.Database.OpenConnection();

                cmd.CommandText = "DeleteProduct";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(CreateParam(cmd, "@pid", id));

                cmd.ExecuteNonQuery();

                _context.Database.CloseConnection();
            }
        }

        private Product MapProduct(DbDataReader reader)
        {
            return new Product
            {
                Id = Convert.ToInt64(reader["id"]),
                ProductName = reader["product_name"].ToString(),
                Price = Convert.ToDouble(reader["price"]),
                Quantity = Convert.ToInt32(reader["quantity"]),
                Unit = reader["unit"].ToString(),
                Description = reader["description"]?.ToString(),
                Rating = Convert.ToDouble(reader["rating"]),
                Sold = Convert.ToInt32(reader["sold"]),
                ImageUrl = reader["image_url"]?.ToString()
                
            };
        }

        private void AddCommonProductParams(DbCommand cmd, Product product)
        {
            cmd.Parameters.Add(CreateParam(cmd, "@pname", product.ProductName));
            cmd.Parameters.Add(CreateParam(cmd, "@pprice", product.Price));
            cmd.Parameters.Add(CreateParam(cmd, "@pquantity", product.Quantity));
            cmd.Parameters.Add(CreateParam(cmd, "@punit", product.Unit));
            cmd.Parameters.Add(CreateParam(cmd, "@pdescription", product.Description ?? (object)DBNull.Value));
            cmd.Parameters.Add(CreateParam(cmd, "@pimageUrl", product.ImageUrl ?? (object)DBNull.Value));
        }

        private DbParameter CreateParam(DbCommand cmd, string name, object value)
        {
            var param = cmd.CreateParameter();
            param.ParameterName = name;
            param.Value = value ?? DBNull.Value;
            return param;
        }
    }
}