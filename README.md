# 🛒 CartOrderAPI (Products, Carts & Orders)

A simple ASP.NET Core Web API that demonstrates an **e-commerce workflow** with Products, Carts, and Orders.
Supports **InMemory database** for quick testing and can be switched to **SQL Server** with EF Core migrations.

---

## 📦 Prerequisites

* [.NET SDK 8.0](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
* SQL Server (optional, only if using persistent DB)
* REST client: Swagger UI (built-in), Postman, or cURL

---

## 🚀 How to Run

### 🟢 Option 1: InMemory (default & easiest)

1. Clone this repository.
2. Run the API:

   ```bash
   dotnet run --project src/CartOrderApi
   ```
3. Open Swagger UI at:
   👉 [http://localhost:5000/swagger](http://localhost:5000/swagger)

---

### 🟠 Option 2: SQL Server (optional)

1. Update `appsettings.Development.json` with your SQL Server connection string:

   ```json
   "ConnectionStrings": {
     "DefaultConnection": "Server=.;Database=ShopDb;Trusted_Connection=True;TrustServerCertificate=True"
   }
   ```
2. Run EF Core migrations:

   ```bash
   dotnet ef migrations add InitialCreate --project src/ShopApi --startup-project src/ShopApi
   dotnet ef database update --project src/ShopApi --startup-project src/ShopApi
   ```
3. Run the API and open Swagger as above.

---

## 📖 API Endpoints

### Products

* **POST** `/api/products` → Create new product
* **GET** `/api/products` → List products (supports `?search=, ?skip=, ?take=`)
* **GET** `/api/products/{id}` → Get product by ID

### Carts

* **POST** `/api/carts` → Create new cart
* **GET** `/api/carts/{id}` → Get cart with items
* **POST** `/api/carts/{cartId}/items` → Add product to cart
* **GET** `/api/carts/{cartId}/items` → List cart items

### Orders

* **POST** `/api/orders/from-cart` → Create order from existing cart
* **GET** `/api/orders/{id}` → Get order by ID

---

## 📌 Sample Requests (curl)

### 🛍️ Create Product

```bash
curl -X POST http://localhost:5000/api/products \
-H "Content-Type: application/json" \
-d '{
  "sku": "P1001",
  "name": "Laptop",
  "description": "Gaming laptop",
  "unitPrice": 1200.50
}'
```

### 🛒 Create Cart

```bash
curl -X POST http://localhost:5000/api/carts \
-H "Content-Type: application/json" \
-d '{
  "sessionId": "abc123",
  "userEmail": "test@example.com"
}'
```

### ➕ Add Item to Cart

```bash
curl -X POST http://localhost:5000/api/carts/1/items \
-H "Content-Type: application/json" \
-d '{
  "productId": 1,
  "quantity": 2
}'
```

### 📃 List Cart Items

```bash
curl http://localhost:5000/api/carts/1/items
```

### 📝 Create Order from Cart

```bash
curl -X POST http://localhost:5000/api/orders/from-cart \
-H "Content-Type: application/json" \
-d '{
  "cartId": 1,
  "taxRate": 0.15
}'
```

---

## ⚖️ Business Rules

* Quantity must be positive (>0).
* Cannot add items to **Converted carts**.
* Only **active products** can be added.
* Order requires at least one cart item.
* All dates use **UTC**.
* Money stored as **decimal(18,2)** (not double).
* Adding the same product again → merge quantities.
* Once converted, the cart is **read-only**.

---

## 📝 Assumptions & Limitations

* InMemory DB resets on app restart.
* Basic validation and error handling included.
* Order numbers generated as `"ORD-{yyyy}-{guid}"`.
* Example project only → not production ready.

---
