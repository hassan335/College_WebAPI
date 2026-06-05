# CollegeApp Web API

## Overview

CollegeApp is a RESTful Web API built with ASP.NET Core that manages student information. The API provides complete CRUD (Create, Read, Update, Delete) functionality and demonstrates modern .NET development practices such as Entity Framework Core, AutoMapper, DTOs, Logging, and JSON Patch operations.

## Features

* Retrieve all students
* Retrieve a student by ID
* Retrieve a student by name
* Create a new student
* Update existing student information
* Partial updates using JSON Patch
* Delete student records
* Entity Framework Core integration
* AutoMapper object mapping
* Structured logging
* DTO-based API design
* RESTful API architecture

## Technologies Used

* ASP.NET Core Web API
* C#
* Entity Framework Core
* SQL Server
* AutoMapper
* JSON Patch
* Dependency Injection
* Logging (ILogger)

## Project Structure

```text
CollegeApp/
│
├── Controllers/
│   └── StudentController.cs
│
├── Models/
│   ├── Student.cs
│   └── StudentDTO.cs
│
├── Data/
│   └── CollegeDbContext.cs
│
├── MyLogging/
│
└── Program.cs
```

## API Endpoints

### Get All Students

```http
GET /api/Student/All
```

Returns all student records.

### Get Student By ID

```http
GET /api/Student/{id}
```

Example:

```http
GET /api/Student/1
```

### Get Student By Name

```http
GET /api/Student/{name}
```

Example:

```http
GET /api/Student/John
```

### Create Student

```http
POST /api/Student/Create
```

Request Body:

```json
{
  "name": "John Doe",
  "email": "john@example.com",
  "address": "Toronto",
  "dob": "2000-01-01"
}
```

### Update Student

```http
PUT /api/Student/Update
```

Request Body:

```json
{
  "id": 1,
  "name": "John Doe",
  "email": "john@example.com",
  "address": "Updated Address",
  "dob": "2000-01-01"
}
```

### Partial Update Student

```http
PUT /api/Student/{id}/UpdatePartial
```

Example Request:

```json
[
  {
    "op": "replace",
    "path": "/address",
    "value": "New Address"
  }
]
```

### Delete Student

```http
DELETE /api/Student/{id}
```

Example:

```http
DELETE /api/Student/1
```

## Response Codes

| Status Code | Description           |
| ----------- | --------------------- |
| 200         | OK                    |
| 201         | Created               |
| 204         | No Content            |
| 400         | Bad Request           |
| 404         | Not Found             |
| 500         | Internal Server Error |

## Key Concepts Implemented

### Entity Framework Core

Used for database access and CRUD operations.

### AutoMapper

Simplifies mapping between Entity models and DTOs.

### DTO Pattern

Prevents exposing database entities directly to API consumers.

### Logging

Implemented using ASP.NET Core's built-in ILogger interface for monitoring and debugging.

### JSON Patch

Supports partial updates without sending the complete object.

## Getting Started

### Prerequisites

* .NET 8 SDK (or your project version)
* SQL Server
* Visual Studio 2022

### Installation

1. Clone the repository

```bash
git clone https://github.com/yourusername/CollegeApp.git
```

2. Navigate to the project directory

```bash
cd CollegeApp
```

3. Update the connection string in `appsettings.json`

4. Apply migrations

```bash
dotnet ef database update
```

5. Run the application

```bash
dotnet run
```

6. Open Swagger

```text
https://localhost:{port}/swagger
```

## Future Enhancements

* JWT Authentication
* Authorization and Role Management
* Repository Pattern
* Unit Testing
* Global Exception Handling Middleware
* Pagination and Filtering
* API Versioning
* Docker Support

## Author

Muhammad Hassan

ASP.NET Developer | Machine Learning Enthusiast | Software Engineering Student
