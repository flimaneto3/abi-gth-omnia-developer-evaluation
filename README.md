# ABI GTH Omnia Developer Evaluation

This project is a .NET 8 Web API for managing sales records, developed as part of the ABI GTH Omnia technical evaluation. It follows Domain-Driven Design (DDD) principles and uses clean architecture, applying external identity patterns and domain event handling.

---

## 🚀 Features

✅ Full CRUD operations for:

- Sales  
- Users (with authentication)

✅ Business rules enforced:

- 🎯 Discounts:
  - ≥ 4 items: 10%
  - 10–20 items: 20%
  - ❌ More than 20 items: Not allowed
- ❌ No discount for < 4 items
- Automatic discount calculation and total amount computation

✅ Domain events implemented:
- `SaleCreated`, `SaleModified`, `SaleCancelled`, `ItemCancelled`

✅ Token-based authentication with JWT  
✅ Postman collection for testing

---

## 🛠 Tech Stack

- ASP.NET Core 8.0
- Entity Framework Core
- PostgreSQL (via Docker)
- FluentValidation
- MediatR
- xUnit, FluentAssertions, NSubstitute (for unit testing)

---

## 🧪 How to Run the Project

### Prerequisites

- [Docker](https://www.docker.com/)
- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download)
- [Postman](https://www.postman.com/) (optional, for API testing)

### Clone the repository

Clone:
> git clone https://github.com/flimaneto3/abi-gth-omnia-developer-evaluation.git

Go to folder:
> cd abi-gth-omnia-developer-evaluation

### Start containers

docker compose up --build

> 📌 The API will be available at:  
> `http://localhost:8080`

---



## 🧪 Preloaded Test Data (Development Only)

To facilitate testing and speed up development, the project automatically seeds sample domain data when running in the development environment. You can use the following predefined records in your API tests or Postman collections:

### 🔹 Branches

| ID | Name | Address | Phone |
|----|------|---------|-------|
| `3fa85f64-5717-4562-b3fc-2c963f66afa6` | Main Branch | 123 Business St, São Paulo | +55 11 99999-8888 |

### 🔹 Customers

| ID | Name | Email | Phone |
|----|------|-------|-------|
| `4eb85f64-6789-1234-b3fc-2c963f66afa6` | Fernando Souza | fernando@example.com | +55 11 77777-6666 |

### 🔹 Products

| ID | Name | Description | Price | Stock |
|----|------|-------------|--------|--------|
| `5fb85f64-1234-5678-b3fc-2c963f66afa6` | Laptop Pro | High-end business laptop | 4999.99 | 50 |

### 🔹 Sales

| ID | Sale Number | Customer | Branch | Cancelled |
|----|-------------|----------|--------|-----------|
| `6db85f64-9876-5432-b3fc-2c963f66afa6` | SL-001 | Fernando Souza | Main Branch | No |

### 🔹 Sale Items

| ID | Product | Quantity | Unit Price | Discount |
|----|---------|----------|------------|----------|
| `7fb85f64-5555-4444-b3fc-2c963f66afa6` | Laptop Pro | 1 | 4999.99 | 0.00 |

### 🔹 Users

| ID | Username | Email | Role | Status |
|----|----------|-------|------|--------|
| `8db85f64-2222-1111-b3fc-2c963f66afa6` | admin_user | admin@example.com | Admin | Active |

> 🔐 Password for `admin_user` is securely hashed using BCrypt. The plain-text password is assumed to be available in a secure way if needed for authentication tests.

These test records are loaded **only in the development environment** and are intended to support automated tests, Postman requests, or manual validation.



---



# API Usage Examples (cURL)

> ⚠️ You must authenticate first using the `Authentication` endpoint. The token returned must be used in the `Authorization` header as a Bearer token in all subsequent requests.
>
> You can test the GET and DELETE endpoints using this sale ID: `7fb85f64-5555-4444-b3fc-2c963f66afa6`, or create a new sale and use its returned ID.

---

### 🔐 Authentication

```bash
curl -X POST http://localhost:8080/api/Auth \
  -H "Content-Type: application/json" \
  -d '{
    "email": "admin@example.com",
    "password": "securepasswordhash"
}'
```

---

### 🛒 Create Sale

```bash
curl -X POST http://localhost:8080/api/Sales \
  -H "Content-Type: application/json" \
  -H "Authorization: Bearer {YourTokenHere}" \
  -d '{
    "saleNumber": "SL-1234",
    "saleDate": "2025-05-04T21:02:56.346Z",
    "customerId": "4eb85f64-6789-1234-b3fc-2c963f66afa6",
    "branchId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    "items": [
      {
        "productId": "5fb85f64-1234-5678-b3fc-2c963f66afa6",
        "quantity": 10
      }
    ]
}'
```

---

### 📄 Select Sale

```bash
curl -X GET http://localhost:8080/api/Sales/7fb85f64-5555-4444-b3fc-2c963f66afa6 \
  -H "Authorization: Bearer {YourTokenHere}"
```

---

### ✏️ Update Sale

```bash
curl -X PUT http://localhost:8080/api/Sales/7fb85f64-5555-4444-b3fc-2c963f66afa6 \
  -H "Content-Type: application/json" \
  -H "Authorization: Bearer {YourTokenHere}" \
  -d '{
    "id": "7fb85f64-5555-4444-b3fc-2c963f66afa6",
    "saleNumber": "1234",
    "saleDate": "2025-05-04T21:02:56.346Z",
    "customerId": "4eb85f64-6789-1234-b3fc-2c963f66afa6",
    "branchId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    "items": [
      {
        "productId": "5fb85f64-1234-5678-b3fc-2c963f66afa6",
        "quantity": 20
      }
    ]
}'
```

---

### ❌ Cancel Sale

```bash
curl -X DELETE http://localhost:8080/api/Sales/7fb85f64-5555-4444-b3fc-2c963f66afa6 \
  -H "Authorization: Bearer {YourTokenHere}"
```



---

## 🧪 Postman

A collection is included to run integration tests.

- Tests include:
  - User authentication
  - Sale creation and validation
  - Sale update
  - Sale cancellation and verification

Open your Postman, click File > Import and choose the json file [End-to-End Tests.postman_collection.json](https://github.com/flimaneto3/abi-gth-omnia-developer-evaluation/blob/main/tests/End-to-End%20Tests.postman_collection.json), run the collection with 1000 milliseconds of delay.

---

## 📘 Notes

- This project was built with a focus on **readability**, **modularity**, and **maintainability**.
- Domain events are logged but can be extended for real event publishing.

---

## ✏️ To Do

- Create environment variables and secrets for database user and password.
- Implement the events, not only log the events.
- Check warning on database container: database "developer_evaluation" does not exist
- Check the architectural decision to use MongoDB and Redis.
