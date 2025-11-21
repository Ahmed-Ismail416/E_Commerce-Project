# E-Commerce Web API

A production-ready E-Commerce backend solution built with **ASP.NET Core Web API**, following **Clean Architecture** principles. This project demonstrates advanced patterns like **Repository & Unit of Work**, **Specification Pattern**, and **Redis Caching** to ensure scalability, maintainability, and high performance.

## Project Overview

The goal of this project is to simulate a real-world e-commerce scenario, covering the full lifecycle from product browsing to secure payments and order management. It focuses heavily on code quality, separation of concerns, and preventing common security pitfalls (e.g., client-side price manipulation).

## Architecture

The solution is designed using **Clean Architecture** to decouple the core business logic from infrastructure and presentation details.

### Solution Structure
* **Core**
    * `DomainLayer`: Enterprise logic, Entities, and Enums.
    * `ServiceAbstraction`: Interfaces defining the contract for business logic (Dependency Inversion).
    * `Services`: Implementation of the business logic.
* **Infrastructure**
    * `Persistence`: Database context, migrations, and repository implementations.
    * `Presentation`: API Controllers and Endpoints.
* **Shared**: Common DTOs, Constants, and Helper classes used across layers.

## Key Features

### Product Module
* **Advanced Retrieval**: Full implementation of **Filtering, Searching, Sorting, and Pagination**.
* **Performance**: Utilizes the **Specification Pattern** to build dynamic, efficient database queries.
* **Data Shaping**: Uses **AutoMapper** with custom **URL Resolvers** to return optimized DTOs.

### Basket Module (Redis)
* **High-Speed Storage**: Utilizes **Redis** for temporary basket storage.
* **Security Logic**: Automatically recalculates item prices from the database during basket creation to prevent client-side price tampering.
* **Shipping**: Dynamic calculation of shipping costs added to the basket total.
* **TTL**: Baskets expire automatically after 30 days.

### Order & Payment Module (Stripe)
* **Stripe Integration**: Implements **PaymentIntents** for secure transactions.
* **Order Lifecycle**: Manages order creation, payment status updates, and shipping method selection.
* **Webhooks**: Handles Stripe webhooks to verify payments and update order status asynchronously.
* **Atomicity**: Ensures inventory and order consistency using Transactions.

### Security & Identity
* **JWT Authentication**: Secure stateless authentication using JSON Web Tokens.
* **Authorization**: Role-based access control for administrative endpoints.
* **User Management**: Registration, Login, and Address Management capabilities.
* **Validation**: Comprehensive input validation and custom error handling.

### Caching & Performance
* **Redis Caching**: Caches heavy API responses to reduce database load.
* **Custom Attributes**: Implemented custom attributes to easily toggle caching for specific GET endpoints.

## Tech Stack

* **Framework**: ASP.NET Core Web API (.NET 6/7/8)
* **Database**: Microsoft SQL Server
* **ORM**: Entity Framework Core
* **Caching**: Redis
* **Payments**: Stripe SDK
* **Object Mapping**: AutoMapper
* **Documentation**: Swagger / OpenAPI
* **Identity**: ASP.NET Core Identity (extended)

## Design Patterns & Concepts used
* **Clean Architecture**
* **Repository & Unit of Work Pattern**
* **Specification Pattern** (for generic querying)
* **Dependency Injection (DI)**
* **Middleware** (Global Exception Handling)

## Getting Started

### Prerequisites
* .NET SDK
* SQL Server
* Redis Server (Running locally or via Docker)
* Stripe Account (for API Keys)

### Installation
1. Clone the repository.
2. Update the `appsettings.json` with your connection strings (SQL & Redis) and Stripe keys.
3. Run database migrations:
    ```bash
    dotnet ef database update -p Infrastructure/Persistence -s E_Commerce
    ```
4. Run the application:
    ```bash
    dotnet run --project E_Commerce
    ```
5. Access the API documentation via Swagger UI at `https://localhost:{port}/swagger`.

---

## Contact & Resume

If you have any questions about the architecture or implementation, feel free to reach out!

* **Developed by:** Ahmed Ismail
* **LinkedIn:** [www.linkedin.com/in/ahmed-ismail-536a71191]
* **Resume (CV):** [https://drive.google.com/file/d/1Oety3Uk1wPNqSpLWyZKD8VaOqbnvgueQ/view?usp=sharing]
