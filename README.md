# 🛍️ ShopVerse

**ShopVerse** is a learning-focused e-commerce platform built with **.NET 9** to explore **Domain-Driven Design (DDD)**, service boundaries, distributed communication, and modern backend architecture.

The solution separates major business capabilities into independent services, including Catalog, Inventory, Sales, Finance, and Identity.

## 🏗️ Architecture

```text
ShopVerse
│
├── CatalogService
├── InventoryService
├── SalesService
├── FinanceService
├── IdentityService
├── IdentityServer
│
├── ShopVerse.BuildingBlocks
├── ShopVerse.BuildingBlocks.Messaging
├── Shared
│
└── docker-compose.yml
```

The project is structured around separate business capabilities rather than a single monolithic application.

## 🧩 Services

### 📦 Catalog Service
Responsible for product catalog operations and product-related data.

### 📊 Inventory Service
Handles stock and inventory-related operations.

### 🛒 Sales Service
Contains order and sales-related application logic.

### 💳 Finance Service
Handles finance and payment-related responsibilities and uses PostgreSQL for persistence.

### 👤 Identity Service
Provides user-related functionality and authentication support.

### 🔐 Identity Server
Provides the identity-provider component used by the platform.

## 🧠 Domain-Driven Design

ShopVerse is organized to practice DDD and separation of concerns.

Services use concepts such as:

- Domain models
- Application use cases
- Commands and queries
- DTOs
- Infrastructure abstractions
- Persistence
- Service-specific presentation layers

The goal is to keep business concerns separated from infrastructure and external interfaces.

## 📨 Asynchronous Messaging

The solution contains shared messaging infrastructure implemented with:

**MassTransit + RabbitMQ**

```text
Service
   │
   │ Integration Event
   ▼
RabbitMQ
   │
   ▼
Consumer / Service
```

MassTransit configuration is centralized in the shared messaging building blocks, allowing services to participate in asynchronous communication without duplicating broker configuration.

## 🔗 Service Communication

The project explores communication between independently structured services using approaches including:

- REST APIs
- Asynchronous messaging
- Shared contracts
- gRPC-related service contracts and integrations

This provides a practical environment for studying communication trade-offs in distributed .NET applications.

## 💾 Persistence

ShopVerse uses service-specific persistence rather than requiring every service to use the same database configuration.

Technologies used across the solution include:

- Entity Framework Core
- SQL Server
- PostgreSQL

## 🧱 Shared Building Blocks

Common functionality is separated into reusable projects:

### `ShopVerse.BuildingBlocks`
Contains shared application and architectural abstractions.

### `ShopVerse.BuildingBlocks.Messaging`
Contains messaging infrastructure, including MassTransit and RabbitMQ integration.

### `Shared`
Contains contracts and shared types used for communication between parts of the system.

## 🐳 Docker

The repository includes Docker Compose configuration for running the application's services and infrastructure locally.

This provides a reproducible development environment for working with multiple services and their dependencies.

## 🚀 Technology Stack

- .NET 9
- ASP.NET Core
- Entity Framework Core
- SQL Server
- PostgreSQL
- RabbitMQ
- MassTransit
- gRPC
- Docker / Docker Compose
- Swagger / OpenAPI
- JWT-based authentication

## ▶️ Getting Started

### Requirements

- .NET 9 SDK
- Docker Desktop

Clone the repository:

```bash
git clone https://github.com/MehrnazMirahmadi/ShopVerse-ddd-platform.git
cd ShopVerse-ddd-platform/ShopVerse
```

Start the environment:

```bash
docker compose up --build
```

Individual services can also be started separately during development using the .NET CLI or Visual Studio.

## 🎯 Project Purpose

ShopVerse was developed as a practical environment for studying and applying backend architecture concepts with .NET.

The project focuses especially on:

- Domain-Driven Design
- Service boundaries
- Separation of concerns
- Distributed communication
- Integration events
- Messaging with RabbitMQ and MassTransit
- Service-specific persistence
- Authentication and identity
- Containerized development

It is intended as a learning and portfolio project rather than being presented as a production-ready commercial e-commerce platform.

---

Built with **.NET 9 and ASP.NET Core**.
