# 🛒 Product Management Module - Grocery Store System

> 📦 A backend module for managing products in an online grocery store system.  
> Built with `.NET`, `MySQL`, and `Entity Framework Core`. Follows layered architecture with stored procedures for robust data operations.

---

## 📌 Table of Contents

- [🎯 Introduction](#-introduction)
- [📚 Technologies Used](#-technologies-used)
- [⚙️ Main Features](#️-main-features)
- [🧱 Architecture & Models](#-architecture--models)
- [🚀 Getting Started](#-getting-started)
- [📁 Project Structure](#-project-structure)
- [📌 Notes](#-notes)

---

## 🎯 Introduction

This module handles product management operations for an online grocery store system. It supports full CRUD operations, integrates with cart and order modules, and interacts with the database through stored procedures to ensure performance and consistency.

---

## 📚 Technologies Used

| Technology        | Purpose |
|-------------------|---------|
| .NET (6 or Core)  | Application development |
| Entity Framework Core | ORM for database access |
| MySQL             | Relational database |
| Stored Procedures | Business logic in database layer |
| PlantUML / Draw.io | UML diagrams for design |
| LINQ / Lambda     | Query syntax and data filtering |

---

## ⚙️ Main Features

### 📦 Products
- [x] View all products
- [x] View product details
- [x] Add new product
- [x] Update product info
- [x] Delete product

### 🛒 Cart
- [x] Add product to cart
- [x] Update product quantity
- [x] Remove product from cart
- [x] Get user cart

### 📦 Orders
- [x] Create order
- [x] Get orders by user
- [x] Save order details

---

## 🧱 Architecture & Models

### 📐 Layers

- `Domain`: Entity classes like `Product`, `Order`, `Cart`, etc.
- `Repositories`: Contains business logic and database access
- `IRepositories`: Interfaces for abstraction
- `Context`: EF Core's `AppDbContext` configuration
- `Stored Procedures`: Called from repository for:
  - `GetAllProducts`
  - `GetProductById`
  - `AddProduct`
  - `UpdateProduct`
  - `DeleteProduct`

### 👤 Use Case Actor: **Manager**

- Can perform all operations on products
- Can confirm critical actions (e.g., deletion)

---

## 🚀 Getting Started

### 1. Clone the repository
```bash
git clone https://github.com/HuyLearnProgram/product_management.git
cd product_management
```
### 2. Setup database connection
Edit the connection string in appsettings.json:
```bash
"ConnectionStrings": {
  "DefaultConnection": "server=localhost;port=3306;database=product_management;user=root;password=your_password;"
}
```

### 3. Create the database and tables
Use MySQL Workbench or CLI to create the product_management database
Using database in data.sql in folder Database

### 4. Build and run the project
```bash
dotnet build
dotnet run
```

## 📁 Project Structure
```bash
ProductManagementModule/
├── Context/               # AppDbContext setup
├── Domain/                # Entity models (Product, Cart, Order...)
├── Repositories/          # Business logic and data operations
├── IRepositories/         # Interfaces for dependency injection
├── appsettings.json       # Database configuration
├── Program.cs             # Entry point
└── README.md              # Project documentation
```

## 📌 Notes
- Ensure MySQL is running on port 3306
- This is a backend module and should be integrated into a full system
- No authentication or user roles implemented yet
- Designed for educational and demo purposes

## 🤝 Contributing
Contributions, feature requests, and pull requests are welcome!
