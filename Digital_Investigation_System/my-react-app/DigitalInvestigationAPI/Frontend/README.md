TracePoint Investigations

1. Student Information

Student Names & Numbers : Palesa Khuthadzo Mudau - 202559729
                        :Phiwokuhle Zibete - 202514168
                        :Mazwi Bonginkosi Mkhonza - 202430145
                        :Inam Zamathaba Mxabo - 202504315

2. Application Description

TracePoint Investigations is a digital investigation system developed
for the Operation Digital Detective practical assignment. It allows
users to view cases, suspects, and evidence, examine evidence details,
and submit investigation information.

The application uses a React frontend connected to an ASP.NET Core Web
API. The API communicates with the database.

3. Technologies Used

React

JavaScript

Vite

ASP.NET Core Web API

Entity Framework Core

Dapper

Microsoft SQL Server

React Router DOM

Unit and integration testing tools configured in the project

4. API Endpoints

The API runs locally at https://localhost:7031.

Cases

HTTP Method   Endpoint            Description

GET           /api/Cases        Retrieve cases
POST          /api/Cases        Create a case
GET           /api/Cases/{id}   Retrieve a case by ID
PUT           /api/Cases/{id}   Update a case
DELETE        /api/Cases/{id}   Delete a case

Evidence

HTTP Method   Endpoint               Description

GET           /api/Evidence        Retrieve evidence
POST          /api/Evidence        Create evidence
GET           /api/Evidence/{id}   Retrieve evidence by ID
PUT           /api/Evidence/{id}   Update evidence
DELETE        /api/Evidence/{id}   Delete evidence

Investigations

HTTP Method             Endpoint                       Description

GET                     /api/Investigation           Retrieve investigations

POST                    /api/Investigation           Submit an investigation

GET                     /api/Investigation/{id}      Retrieve an
investigation by ID

Suspects

HTTP Method   Endpoint               Description

GET           /api/Suspects        Retrieve suspects
POST          /api/Suspects        Create a suspect
GET           /api/Suspects/{id}   Retrieve a suspect by ID
PUT           /api/Suspects/{id}   Update a suspect
DELETE        /api/Suspects/{id}   Delete a suspect

5. Database Setup Instructions

Open Microsoft SQL Server Management Studio (SSMS).

Create or open the database used by the application.

Ensure the database contains the tables used by the API: Cases,
Evidence, Investigations, and Suspects.

Open appsettings.json in the ASP.NET Core API project.

Update the DefaultConnection connection string so it points to
your SQL Server instance and database.

Ensure the required case, suspect, and evidence data is present in
the database.

If the project uses Entity Framework Core migrations, run the
following command from the API project folder:

dotnet ef database update

6. Instructions for Running the API

Open the TracePointAPI project folder in a terminal or open the
solution in Visual Studio.

Check that the database connection string in appsettings.json is
correct.

Restore the project dependencies:

dotnet restore

Start the API:

dotnet run

Open Swagger in the browser:

https://localhost:7031/swagger/index.html

Use Swagger to view and test the available API endpoints.

7. Instructions for Running React

Open a separate terminal in the TracePointClient folder.

Install the dependencies:

npm install

Start the React development server:

npm run dev

This will open URL will open our react frontend:

http://localhost:5173

Keep the ASP.NET Core API running while using the React application.

8. Testing Instructions

API Testing

Start the ASP.NET Core API.

Open Swagger at https://localhost:7031/swagger/index.html.

Test the GET, POST, PUT, and DELETE endpoints for Cases, Evidence,
and Suspects.

Test the available GET and POST endpoints for Investigations.

Check the API responses to confirm that requests succeed or return
an appropriate error.

React Testing

Open a terminal in the TracePointClient folder.

Install the dependencies if necessary:

npm install

Run the test command configured in the project's package.json. If
the project has a test script, run:

npm test

Confirm that the tests execute and review whether they pass.

Integration Testing


Test that the React application can communicate with the ASP.NET Core
API and that investigation data submitted through the application is
received by the API and saved in the database.