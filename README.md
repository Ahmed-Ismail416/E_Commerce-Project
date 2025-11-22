# 🛒 E-Commerce Platform – ASP.NET Core Web API

A modern, scalable, and production‑ready **E-Commerce Backend** built with **ASP.NET Core 9** and designed using **Clean Architecture**. The system supports authentication, product catalog, shopping cart operations, ordering, payments, caching, and full API documentation.

---

## 🎯 Project Overview

A powerful backend designed to:

* Manage product catalogs with filtering, sorting, and pagination
* Securely authenticate and authorize users (JWT + Identity)
* Handle shopping cart operations with Redis
* Process orders with delivery methods and statuses
* Integrate Stripe for payments
* Follow Clean Architecture & SOLID principles

---

## ✨ Key Features

### 🔐 Authentication & Authorization

* JWT Authentication (Access + Refresh tokens)
* ASP.NET Identity with custom `ApplicationUser`
* Role support (Admin, SuperAdmin)
* User profile + address management

### 🛍️ Product Management

* Brands, types, and full product catalog
* Specification Pattern for filtering, searching, sorting
* Product images & descriptions

### 🛒 Shopping Cart

* Redis-backed cart storage
* Persistent user carts
* Real‑time item updates

### 📦 Order Management

* Full order lifecycle (Pending → Payment Received/Failed)
* Pricing snapshots per item
* Delivery methods (UPS1–UPS4)
* Shipping address handling

### 💳 Payment Processing (Stripe)

* Create Payment Intents
* Validate & confirm payments
* Webhook support for payment verification

### ⚡ Performance & Optimization

* Redis caching layer
* Pagination & optimized DB queries
* Exceptions middleware and unified error response

### 📚 API Documentation

* Full Swagger/OpenAPI support
* Auto‑generated API specification

---

## 🏛️ Architecture

### 🧱 Project Structure

```
E-Commerce/
├── E_Commerce/                 # API entry point
│   ├── Controllers/
│   ├── Middlewares/
│   ├── Extensions/
│   └── Program.cs
├── Core/                       # Domain + Services
│   ├── DomainLayer/
│   │   ├── Models/
│   │   └── Contracts/
│   └── Services/
│       ├── Specifications/
│       ├── Mapping/
│       ├── OrderService.cs
│       ├── PaymentService.cs
│       ├── CacheService.cs
│       └── AuthenticationService.cs
├── ServiceAbstraction/         # Service interfaces
├── Presentation/               # DTOs + Controllers
├── Persistence/                # EF Core, Repos, Migrations
└── Shared/                     # DTOs + Error Models
```

### Clean Architecture Layers

1. **Domain** – Entities, business rules
2. **Application Services** – Business logic
3. **Service Abstractions** – Interfaces for DI
4. **Presentation** – Controllers + DTOs
5. **Persistence** – EF Core + Repositories
6. **Shared** – Cross‑cutting concerns

---

## 🛠️ Technologies

| Technology       | Version | Purpose           |
| ---------------- | ------- | ----------------- |
| .NET             | 9.0     | Runtime           |
| ASP.NET Core     | 9.0     | Web API Framework |
| EF Core          | 9.0.10  | ORM               |
| SQL Server       | Latest  | Database          |
| Redis            | 2.9.32  | Caching           |
| Stripe.NET       | 50.0.0  | Payments          |
| AutoMapper       | 15.1.0  | Mapping           |
| Swagger          | 9.0.6   | API Docs          |
| ASP.NET Identity | 9.0.10  | Auth              |

---

## 🚀 Getting Started

### 📌 Prerequisites

* .NET 9 SDK
* SQL Server
* Redis
* VS 2022 / VS Code

### 💾 Installation

```bash
git clone <your-repository>
cd E-Commerce
dotnet restore
```

### 🔧 Configuration

Update **appsettings.json**:

```
ConnectionStrings: {
  DefaultConnection: "Server=...;Database=ECommerceDB;...",
  IdentityConnection: "Server=...;Database=ECommerceIdentityDB;..."
},
Redis: { ConnectionString: "localhost:6379" },
Stripe: {
  SecretKey: "your-key",
  PublishableKey: "your-key"
},
JWT: {
  SecretKey: "your-secret",
  ExpirationMinutes: 60,
  ValidIssuer: "ExampleIssuer",
  ValidAudience: "ExampleAudience"
}
```

### 🗄️ Database Migration

```bash
dotnet ef database update -p Persistence -s E_Commerce
```

### ▶️ Run the API

```bash
dotnet run --project E_Commerce
```

### 📘 Swagger UI

`https://localhost:7001/swagger`

---

## 📡 API Endpoints

### 🛒 Products

```
GET  /api/products
GET  /api/products/{id}
GET  /api/product-brands
GET  /api/product-types
```

### 📦 Orders

```
POST /api/orders
GET  /api/orders
GET  /api/orders/{id}
GET  /api/orders/delivery
```

### 💳 Payments

```
POST /api/payments
POST /api/payments/webhook
GET  /api/payments/{orderId}
```

### 🔐 Auth

```
POST /api/auth/register
POST /api/auth/login
POST /api/auth/refresh
POST /api/auth/logout
```

---

## 🗄️ Database Schema

**Modules:**

* **Products**: Brands, Types, Catalog
* **Orders**: Delivery methods, Shipping, Items
* **Identity**: Users, Roles, Addresses

---

## 🔐 Authentication (JWT)

Flow:

1. User logs in
2. Server issues JWT token
3. Client stores & sends token in headers
4. Server validates & authorizes

---

## 💳 Payment Flow (Stripe)

1. User places order
2. Backend creates Payment Intent
3. User pays through Stripe UI
4. Webhook notifies backend
5. Order marked as *Paid*

---

## 📦 Seeding

Automated seeding on startup:

* Products
* Brands
* Types
* Delivery methods
* Roles (Admin, SuperAdmin)
* Admin test accounts

---

## 🐛 Error Handling

Unified error format:

```
{
  "statusCode": 400,
  "message": "Validation failed",
  "errors": [...]
}
```

---

## 🧪 Testing

```bash
dotnet test
```

---

## 📊 Performance

* Redis Caching
* Pagination
* Database Indexing
* Async/Await

---

## 📝 Contributing

1. `git checkout -b feature/my-feature`
2. Make changes
3. `git commit -m "Add feature"`
4. `git push origin feature/my-feature`
5. Open PR

---

## 👨‍💻 Author

**Ahmed Ismail**

* Email: [ahmedesm416@gmail.com](mailto:ahmedesm416@gmail.com)
* LinkedIn: <www.linkedin.com/in/ahmed-ismail-536a71191>

---


