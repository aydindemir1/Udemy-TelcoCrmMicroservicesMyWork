# TelcoCRM — Microservices Architecture

A production-grade **Telco CRM** (Customer Relationship Management) system built with **.NET 10.0 microservices**, Clean Architecture, and event-driven communication. The project demonstrates real-world enterprise patterns including CQRS, the Outbox Pattern, API Gateway routing, service discovery, distributed tracing, and multi-database persistence.

---

## Services

| Service | Port | Database | Responsibility |
|---------|------|----------|----------------|
| [ApiGateway](./ApiGateway/README.md) | 9500 | — | Single entry point — Ocelot routing + Polly resilience |
| [IdentityService](./IdentityService/README.md) | 5000 | SQL Server | Authentication, JWT tokens, user management, 2FA |
| [CustomerService](./CustomerService/README.md) | 5001 | PostgreSQL | Customer profiles and account management |
| [SearchService](./SearchService/README.md) | 5002 | Elasticsearch | Full-text search across catalog and customers |
| [CatalogService](./CatalogService/README.md) | 5003 | SQL Server | Product offerings, pricing, and catalog management |
| [BasketService](./BasketService/README.md) | 5004 | Redis | Shopping basket / cart |
| [SalesService](./SalesService/README.md) | 5005 | MongoDB | Sales orders and transactions |
| [InvoiceService](./InvoiceService/README.md) | 5006 | PostgreSQL | Invoice generation and billing |
| [Shared](./Shared/README.md) | NuGet | — | Shared contracts and DTOs across all services |

---

## Architecture Overview

```
                          ┌─────────────────────────────┐
                          │         Client Apps          │
                          └──────────────┬──────────────┘
                                         │ HTTP
                          ┌──────────────▼──────────────┐
                          │          API Gateway         │
                          │    Ocelot + Eureka + Polly   │
                          └──┬───┬───┬───┬───┬───┬───┬──┘
                             │   │   │   │   │   │   │
              ┌──────────────┘   │   │   │   │   │   └──────────────┐
              │                  │   │   │   │   │                   │
     ┌────────▼──────┐  ┌───────▼─┐ │  ┌▼───────┐  ┌──────────────▼──┐
     │ IdentityService│  │Customer │ │  │Catalog │  │  SearchService   │
     │  (SQL Server)  │  │(Postgres│ │  │(SQL Svr│  │(Elasticsearch)   │
     └────────────────┘  └─────────┘ │  └────────┘  └─────────────────┘
                                      │
                      ┌───────────────┼───────────────┐
                      │               │               │
             ┌────────▼───┐  ┌───────▼────┐  ┌───────▼────┐
             │BasketService│  │SalesService│  │InvoiceServ.│
             │  (Redis)    │  │ (MongoDB)  │  │(PostgreSQL)│
             └────────────┘  └────────────┘  └────────────┘

        ─────────────── Async Messaging (RabbitMQ) ─────────────────
                  All services communicate events via RabbitMQ
                     with the Outbox Pattern for reliability
```

---

## Infrastructure Stack

| Component | Technology | Port |
|-----------|-----------|------|
| Service Discovery | Steeltoe Eureka | 8761 |
| Message Broker | RabbitMQ | 5672 / 15672 |
| Distributed Tracing | Jaeger + OpenTelemetry | 16686 |
| Metrics | Prometheus | 9090 |
| Dashboards | Grafana | 3000 |
| Log Visualization | Kibana | 5601 |
| Email Testing | Mailhog | 8025 |

---

## Communication Patterns

### Synchronous (HTTP)
Services communicate directly via typed `HttpClient` with Polly resilience policies:
- **BasketService** → CatalogService (product details), CustomerService (billing account)
- **SalesService** → BasketService (checkout), CustomerService (customer info)

### Asynchronous (RabbitMQ + Outbox)
Integration events flow through RabbitMQ for decoupled, reliable messaging:

```
CustomerService ──[CustomerCreatedEvent]──▶ SearchService (index customer)
BasketService   ──[BasketClearedEvent]───▶  SalesService  (create order)
SalesService    ──[OrderPlacedEvent]─────▶  InvoiceService (generate invoice)
```

---

## Getting Started

### Prerequisites

- [.NET 10.0 SDK](https://dotnet.microsoft.com/download)
- [Docker Desktop](https://www.docker.com/products/docker-desktop/)
- [Visual Studio 2022](https://visualstudio.microsoft.com/) or JetBrains Rider

### Run with Docker Compose

```bash
# Start all infrastructure and services
docker-compose up -d

# Check that all containers are healthy
docker-compose ps
```

### Service URLs (after startup)

| Service | URL |
|---------|-----|
| API Gateway | http://localhost:9500 |
| Eureka Dashboard | http://localhost:8761 |
| RabbitMQ Management | http://localhost:15672 (admin / admin123) |
| Grafana | http://localhost:3000 |
| Prometheus | http://localhost:9090 |
| Jaeger UI | http://localhost:16686 |
| Kibana | http://localhost:5601 |
| Mailhog UI | http://localhost:8025 |

### Run a Single Service Locally

```bash
cd IdentityService/WebApi
dotnet run
```

Ensure the service's `appsettings.Development.json` points to your local infrastructure (databases, RabbitMQ).

---

## Project Structure

Each microservice follows **Clean Architecture** with four layers:

```
[ServiceName]/
├── Domain/          ← Entities, enums, domain events
├── Application/     ← CQRS handlers, validators, DTOs, repository interfaces
├── Persistence/     ← EF Core DbContext, repositories, migrations
├── Infrastructure/  ← External HTTP clients, email, messaging implementations
└── WebApi/          ← Controllers, DI setup, Dockerfiles, appsettings
```

---

## Key Design Patterns

| Pattern | Where Used |
|---------|-----------|
| CQRS + MediatR | All services — commands and queries are separate |
| Repository | All services — `Core.Persistence` generic repositories |
| Outbox Pattern | CustomerService, CatalogService — reliable event publishing |
| API Gateway | ApiGateway — single entry point with routing and resilience |
| Service Discovery | All services — Eureka registration and lookup |
| Circuit Breaker | ApiGateway, inter-service HTTP clients — Polly |
| Saga (choreography) | Order flow — via RabbitMQ integration events |
| Distributed Tracing | All services — OpenTelemetry → Jaeger |

---

## Core Package Dependencies

All services consume packages from the companion **[UdemyCourse-CorePackage](../UdemyCourse-CorePackage/README.md)** monorepo:

```
Core.Domain, Core.Abstractions, Core.Cqrs, Core.Application
Core.Persistence, Core.WebApi, Core.Extensions
Core.Security.* (Domain, Jwt, Hashing, Encryption, Redis)
Core.Messaging, Core.Messaging.Postgres, Core.Messaging.Transport.RabbitMq
Core.Events, Core.Tracing, Core.Monitoring, Core.Resiliency
Core.ElasticSearch, Core.Scheduling.Hangfire, Core.Mailing
```

---

## Security

- All endpoints (except `/auth/login` and `/auth/register`) require a valid **JWT Bearer** token
- Tokens are issued by **IdentityService** and validated by each service independently
- **Redis** (`authroledb`) caches role/claim lookups to avoid hitting the database on every request
- **Refresh tokens** are stored in Redis with IP binding and automatic rotation

---

## Observability

Every service is instrumented with:
- **Prometheus** metrics — scraped from `/metrics` (via `Core.Monitoring`)
- **OpenTelemetry** traces — exported to Jaeger (via `Core.Tracing`)
- **Health checks** — exposed at `/health` and `/health-ui`
- Pre-built **Grafana dashboard** — import `monitoring_dashboard.json`
- Pre-built **Jaeger dashboard** — import `jaeger_tracing_dashboard.json`

---

## License

This project is created for educational purposes as part of the Udemy course curriculum.
