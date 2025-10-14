# ShopVerse - Microservice E-commerce Platform

ShopVerse is a scalable, microservice-based e-commerce platform built with .NET 9 and Domain-Driven Design (DDD) principles. It leverages asynchronous communication and modern architectural patterns to handle product catalog, inventory management, order processing, payments, and user authentication efficiently.

## 🏗️ Architecture Overview

ShopVerse follows a microservices architecture with the following key components:

### Core Services
- **Catalog Service** - Product catalog management and search
- **Inventory Service** - Stock management and inventory tracking
- **Sales Service** - Order processing and management
- **Finance Service** - Payment processing and financial operations
- **Identity Service** - User authentication and authorization
- **Identity Server** - OAuth2/OpenID Connect identity provider

### Communication Patterns
- **Synchronous**: gRPC for inter-service communication
- **Asynchronous**: RabbitMQ for event-driven messaging
- **REST APIs**: External service interfaces

## 🚀 Technology Stack

- **.NET 9** - Core framework
- **Entity Framework Core 9.0** - Data access and ORM
- **SQL Server 2022** - Primary database for most services
- **PostgreSQL 16** - Finance service database
- **RabbitMQ** - Message broker for asynchronous communication
- **gRPC** - High-performance inter-service communication
- **Docker** - Containerization and orchestration
- **IdentityServer4** - OAuth2/OpenID Connect identity provider
- **MassTransit** - Message bus implementation
- **Swagger/OpenAPI** - API documentation
- **JWT Bearer Authentication** - Token-based security

## 📁 Project Structure

```
ShopVerse/
├── CatalogService/           # Product catalog management
├── InventoryService/         # Inventory and stock management
├── SalesService/            # Order processing
├── FinanceService/          # Payment and financial operations
├── IdentityService/         # User management
├── IdentityServer/          # OAuth2/OpenID Connect server
├── ShopVerse.BuildingBlocks/ # Shared building blocks
├── ShopVerse.BuildingBlocks.Messaging/ # Messaging infrastructure
├── Shared/                  # Shared contracts and DTOs
├── tests/                   # Unit and integration tests
└── docker-compose.yml       # Container orchestration
```

## 🛠️ Prerequisites

- **.NET 9 SDK**
- **Docker Desktop**
- **Visual Studio 2022** (recommended) or **VS Code**
- **SQL Server** (or use Docker container)
- **PostgreSQL** (or use Docker container)

## 🚀 Getting Started

### 1. Clone the Repository
```bash
git clone <repository-url>
cd ShopVerse
```

### 2. Run with Docker Compose (Recommended)
```bash
# Navigate to the ShopVerse directory
cd ShopVerse

# Start all services
docker-compose up -d
```

This will start:
- SQL Server 2022 (port 1435)
- PostgreSQL 16 (port 5432)
- RabbitMQ Management (ports 5672, 15672)
- All microservices with their respective databases

### 3. Access Services

| Service | URL | Description |
|---------|-----|-------------|
| Inventory API | http://localhost:5001 | Inventory management API |
| Sales API | http://localhost:5056 | Order processing API |
| Finance API | http://localhost:5196 | Payment processing API |
| Catalog API | http://localhost:5234 | Product catalog API |
| Identity API | http://localhost:5203 | User management API |
| Identity Server | http://localhost:5016 | OAuth2/OpenID Connect server |
| Inventory gRPC | http://localhost:5292 | Inventory gRPC service |
| Product Inventory gRPC | http://localhost:5294 | Product inventory gRPC service |
| RabbitMQ Management | http://localhost:15672 | Message broker management |

### 4. Development Setup

#### Individual Service Development
Each service can be run independently for development:

```bash
# Run Inventory Service
cd ShopVerse/InventoryService/Presentation
dotnet run

# Run Sales Service
cd ShopVerse/SalesService/Presentation
dotnet run

# Run Finance Service
cd ShopVerse/FinanceService/Presentation
dotnet run

# Run Catalog Service
cd ShopVerse/CatalogService/Catalog.Presentation/Catalog.Presentation
dotnet run

# Run Identity Service
cd ShopVerse/IdentityService/Identity.Presentation
dotnet run

# Run Identity Server
cd ShopVerse/IdentityServer/IdentityServer.Api/ShopVerse.IdentityServer.Api
dotnet run
```

#### Database Migrations
Migrations are automatically applied on startup in development mode. For manual migration:

```bash
# For Inventory Service
cd ShopVerse/InventoryService/Infrastructure
dotnet ef database update

# For Finance Service
cd ShopVerse/FinanceService/Infrastructure
dotnet ef database update

# For Sales Service
cd ShopVerse/SalesService/Infrastructure
dotnet ef database update

# For Catalog Service
cd ShopVerse/CatalogService/Catalog.Infrastructure/Catalog.Infrastructure
dotnet ef database update

# For Identity Service
cd ShopVerse/IdentityService/Identity.Infrastructure
dotnet ef database update

# For Identity Server
cd ShopVerse/IdentityServer/IdentityServer.Api/ShopVerse.IdentityServer.Api
dotnet ef database update
```

## 🔧 Configuration

### Environment Variables
Key configuration settings can be modified in `docker-compose.yml`:

- **Database Connections**: SQL Server and PostgreSQL connection strings
- **Message Broker**: RabbitMQ configuration (guest/guest)
- **Service URLs**: Inter-service communication endpoints
- **Ports**: Service exposure ports
- **gRPC Settings**: Inter-service gRPC communication URLs

### App Settings
Each service has its own `appsettings.json` with service-specific configuration:

- **Inventory Service**: SQL Server connection, RabbitMQ configuration
- **Finance Service**: PostgreSQL connection, RabbitMQ configuration
- **Sales Service**: SQL Server connection, gRPC settings for inventory
- **Catalog Service**: SQL Server connection, gRPC settings for product inventory
- **Identity Service**: SQL Server connection, JWT settings
- **Identity Server**: SQL Server connection, OAuth2/OpenID Connect configuration

## 🧪 Testing

The project includes comprehensive test suites:

```bash
# Run all tests
dotnet test

# Run specific test project
dotnet test tests/Sales.UnitTests/
dotnet test tests/Catalog.UnitTests/
```

## 📚 API Documentation

Each service exposes Swagger documentation when running in development mode:

- Inventory API: http://localhost:5001/swagger
- Sales API: http://localhost:5056/swagger
- Finance API: http://localhost:5196/swagger
- Catalog API: http://localhost:5234/swagger
- Identity API: http://localhost:5203/swagger
- Identity Server: http://localhost:5016/swagger

## 🔐 Authentication & Authorization

The platform uses IdentityServer4 for OAuth2/OpenID Connect authentication:

1. **Register/Login** through Identity Service (port 5203)
2. **Obtain JWT tokens** from Identity Server (port 5016)
3. **Use tokens** to access protected APIs

### Example API Call
```bash
curl -H "Authorization: Bearer <your-jwt-token>" \
     http://localhost:5001/api/inventory/items
```

### Default Credentials
- **RabbitMQ Management**: guest/guest
- **SQL Server**: sa/SwN12345678
- **PostgreSQL**: postgres/postgres

## 📊 Monitoring & Health Checks

- **Health Checks**: Available at `/health` endpoints for each service
- **Logging**: Structured logging with different levels (Information, Warning, Error)
- **RabbitMQ Management**: Monitor message queues at http://localhost:15672
- **Database Health**: SQL Server and PostgreSQL health checks integrated

## 🏗️ Building Blocks

### Shared Components
- **ShopVerse.BuildingBlocks**: Common utilities, CQRS patterns, and base classes
- **ShopVerse.BuildingBlocks.Messaging**: MassTransit integration for message bus
- **Shared.GrpcContracts**: gRPC service contracts and protobuf definitions

### DDD Implementation
Each service follows DDD principles with Clean Architecture:
- **Domain Layer**: Business logic, entities, value objects, and domain events
- **Application Layer**: Use cases, commands/queries, DTOs, and application services
- **Infrastructure Layer**: Data access, repositories, external services, and persistence
- **Presentation Layer**: API controllers, gRPC services, and external interfaces

### Service Architecture
- **Inventory Service**: Manages product stock levels and inventory operations
- **Sales Service**: Handles order creation, processing, and management
- **Finance Service**: Processes payments and financial transactions
- **Catalog Service**: Manages product information and catalog operations
- **Identity Service**: Handles user registration, login, and profile management
- **Identity Server**: Provides OAuth2/OpenID Connect authentication services

## 🤝 Contributing

1. Fork the repository
2. Create a feature branch
3. Make your changes
4. Add tests for new functionality
5. Ensure all tests pass
6. Submit a pull request

## 📄 License

This project is licensed under the terms specified in the LICENSE file.

## 🆘 Support

For issues and questions:
1. Check existing issues in the repository
2. Create a new issue with detailed information
3. Include logs and error messages when applicable

## 🔄 CI/CD

The project includes GitHub Actions workflows for:
- Building and testing
- Docker image creation
- Deployment automation

## 🚀 Quick Start Commands

```bash
# Clone and start the entire platform
git clone <repository-url>
cd ShopVerse/ShopVerse
docker-compose up -d

# Check service status
docker-compose ps

# View logs
docker-compose logs -f [service-name]

# Stop all services
docker-compose down

# Rebuild and start
docker-compose up -d --build
```

## 🔧 Troubleshooting

### Common Issues
1. **Port conflicts**: Ensure ports 5001, 5056, 5196, 5203, 5234, 5016, 5292, 5294, 1435, 5432, 5672, 15672 are available
2. **Database connection issues**: Verify SQL Server and PostgreSQL containers are running
3. **gRPC communication errors**: Check service dependencies and network connectivity
4. **Authentication failures**: Ensure Identity Server is running before accessing protected endpoints

### Health Check Endpoints
- http://localhost:5001/health (Inventory)
- http://localhost:5056/health (Sales)
- http://localhost:5196/health (Finance)
- http://localhost:5234/health (Catalog)
- http://localhost:5203/health (Identity)

---

**Note**: This is a development setup. For production deployment, ensure proper security configurations, environment variables, and infrastructure setup.
