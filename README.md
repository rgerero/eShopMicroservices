# eShopMicroservices

# Tools
**MediatR** - .NET library that implements the Mediator design pattern, which promotes loose coupling between objects by ensuring they interact through a mediator rather than directly.

**Carter** - is a lightweight and simple library for building modular and minimal APIs in ASP.NET Core

**Mapster** - is a high-performance object-to-object mapping library for .NET, designed to map data between different object types (e.g., DTOs and domain models) with minimal configuration

**Marten** - an ORM that leverages Postgre's JSON and transform into .NET transactional document DB.

# APIs
**Catalog.API** - Vertical Slice architecture + CQRS, Postgre, BRUD operations

**Basket.API** - Vertical Slice architecture + CQRS & Repository pattern, Redis, Postgre, CRUD & gRPC operations

**Discount.API** - n-tier architecture + EF Core + SQlite & gRPC operations
Add-Migration InitialCreate

# Docker Commands
docker compose up
docker exec -it 4f6f528741cc bash

#bash command to access postgress
psql -U postgres
\l - list tables
\c <DNName> - go to db
\d
