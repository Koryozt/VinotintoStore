# VinotintoStore

Vinotinto Store is just basically a template or portfolio project to show a fully developed e-commerce based on Venezuela's national team. This is for showcase purposes.

## Project Structure

The project follows the Clean Architecture for its structure, it's divided by layers, each layer contains files based on its purpose. Here is the structure:

- Domain: This contains the entities, the primitives, enums and repositories interfaces (Found in the abstract folder), also the value objects, domain events and exceptions/errors.
- Application: The application layer contains the business logic of the project, here is found the implementation of CQRS for the project, the interfaces of the external services and the validations.
- Persistence + Infrastructure: These projects are part of the "Infrastructure Layer" but separated in two sub-layers. The persistence layer contains everything related to the Database, including the Application context, the implementation of the repositories and more. The Infrastructure layer implements external services like Authentication, Authorization, JWT, Email Service, Idempotence (to avoid duplicated outbox messages) and background jobs.
- Presentation: Contains the definition of each controller for the API, and the ApiController base class.
- WebApi: The API of the project, contains the program.cs class and includes all the DI made in the other layers.
- App: The frontend application, made with React.

## Project Database Diagram
![VinotintoMarketplaceDiagram](https://github.com/Koryozt/VinotintoMarketplace/assets/93677342/995cc586-c12b-4653-aaef-3eaf5038a908)

## Project information
NET version: 7.0 <br>
ASP.NET Core version: 7.0 <br>
React version: React 18. <br>
Libraries used: Quartz, Scrutor, FluentValidation, EntityFrameworkCore, Serilog (for logging). <br>
Database: MS SQL Server.
