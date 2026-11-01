CollegeApp Web API
An ASP.NET Core Web API for managing student records, built with .NET 8. The project demonstrates REST endpoints, Entity Framework Core with SQL Server, repository patterns, AutoMapper, JWT authentication, role-based authorization, Swagger, and CORS.
Technologies
- ASP.NET Core Web API (.NET 8)
- Entity Framework Core and SQL Server
- AutoMapper
- JWT bearer authentication
- Swagger / OpenAPI
- Repository pattern and dependency injection
- log4net logging
Prerequisites
- .NET 8 SDK
- SQL Server or SQL Server Express
- Visual Studio 2022 or another .NET-compatible editor
Getting started
1. Clone the development branch
git clone --branch development --single-branch https://github.com/hassan335/College_WebAPI.git CollegeApp-Development
cd CollegeApp-Development
2. Configure the application
The API uses a SQL Server connection string named CollegeAppDbConnection and a JWT signing key named SecretKey.

For local development, set these through .NET User Secrets rather than committing credentials to Git:
cd CollegeApp
dotnet user-secrets init
dotnet user-secrets set "ConnectionStrings:CollegeAppDbConnection" "Server=YOUR_SERVER;Database=CollegeDb;Trusted_Connection=True;TrustServerCertificate=True"
Generate a cryptographically random 64-byte key (for HS512) and store its Base64 value in SecretKey using User Secrets. If you store a Base64 key, update both JWT generation and JWT validation code to use Convert.FromBase64String(...) instead of Encoding.ASCII.GetBytes(...). Both sides must use the same key bytes.

For an application that keeps Encoding.ASCII.GetBytes(...), configure a securely generated key compatible with that encoding and long enough for HS512; do not use a short example password.

The project currently uses a development SQL Server connection. Adjust it to match your environment.
3. Restore and run
From the directory containing CollegeApp.csproj:
dotnet restore
dotnet run
If the project uses Entity Framework Core migrations, apply them to your configured database as appropriate:
dotnet ef database update
The CLI migration command requires the dotnet-ef tool and the relevant EF Core packages.
4. Explore the API
When running in Development mode with Swagger enabled, visit:
https://localhost:<your-port>/swagger
Use the HTTPS port printed by dotnet run or configured in the project's launch profile.
API endpoints
The following routes are based on the project's LoginController and StudentController.
Method	Route	Description
POST	/api/Login	Authenticate and obtain a JWT
GET	/api/Student/All	Retrieve all students
GET	/api/Student/{id}	Retrieve a student by numeric ID
GET	/api/Student/{name}	Retrieve a student by alphabetic name
POST	/api/Student/Create	Create a student
PUT	/api/Student/Update	Update a student
PUT	/api/Student/{id}/UpdatePartial	Partially update a student
DELETE	/api/Student/{id}	Delete a student


Authorization: The student controller uses [Authorize(Roles = "Admin,SuperAdmin")]. Individual actions with [AllowAnonymous] bypass that requirement. Check the current branch's attributes for the effective access policy.

Login request example
POST /api/Login
Content-Type: application/json
Accept: application/json
{
  "username": "YOUR_USERNAME",
  "password": "YOUR_PASSWORD"
}
The response includes a JWT token when login succeeds. Do not hardcode actual credentials in client-side code.
Calling a protected endpoint
const response = await fetch("https://localhost:7223/api/Student/All", {
  method: "GET",
  headers: {
    Accept: "application/json",
    Authorization: `Bearer ${token}`
  }
});

if (response.status === 204) {
  console.log("No content returned");
} else if (!response.ok) {
  console.error(`Request failed: ${response.status}`);
} else {
  console.log(await response.json());
}
Replace the port with your running API's HTTPS port. The example assumes the endpoint requires authentication; an action marked [AllowAnonymous] does not require the bearer token.
CORS
The project defines a named CORS policy, AllowOnyCoogle, for allowed browser origins. For a local IIS-hosted HTML page at http://localhost/Web_API/, the origin to configure is http://localhost (without the /Web_API/ path).

When using a local browser client on another port or host, explicitly add its origin to the policy. Avoid unrestricted origins in production.
Common HTTP responses
Status	Meaning
200 OK	Request completed successfully
201 Created	Resource created
204 No Content	Request succeeded without a response body
400 Bad Request	Invalid input
401 Unauthorized	Missing, invalid, or expired authentication
403 Forbidden	Authenticated, but missing the required role or permission
404 Not Found	Route or requested resource not found
415 Unsupported Media Type	Request Content-Type is not supported for the expected body


For JSON POST/PUT requests, include Content-Type: application/json and serialize the body with JSON.stringify(...).
Security notes
- Never commit signing secrets, tokens, database passwords, or production connection strings.
- Use strong, randomly generated JWT signing keys. Keep the signing and validation key formats consistent.
- Keep sensitive student endpoints protected by appropriate authorization rules.
- Do not expose configuration secrets through diagnostic API endpoints.
- Use HTTPS outside local-only experiments.
Git workflow
The development branch is the primary working branch for this project. To see the latest commit:
git log -1 --oneline
To push committed updates from development:
git push origin development
License
No license has been specified for this repository.
