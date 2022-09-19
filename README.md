# SmartLock

## Run instructions:
- Run `dotnet restore` to restore the project
- Run `dotnet build` to build the project
- Run `dotnet publish -o ./publish`
- Run `dotnet test` to run the unit tests

## Approach:
The goal here is to allow users to open the doors remotely and show historical events to extend user experience beyond classical tags. So what it takes to have customers using such a cool feature? I have come up with a MVP of SmartLock which would allow the customers to login, view the doors they have access to and access them remotely either from Mobile/Laptop or equivalent gadgets. For quick & smoother onboarding of the customer, we can seed the data of the Users/Office/Door & their role details (In future we can have them via API's). Also we can make few of them as admins of the system among them to have access to the historical events of any users within their office(s).

Now we have successfully onboarded customer into our system, any user can login with their login credentials and should be able to view the doors they have access to and open the door remotely. Users can even provide comments if they are opening the door for someone else. As an admin will have a special access to look at the historical events of the user.

Sounds cool isn't it? Let's look at the technical view on it.

## Design:
SmartLock service follows a layered architecture with REST API, Service, Command/Query & Repository layers. 

#### API Layer:
Talks about the API's SmartLock service has to offer and internally does authentication & authorization of the users too.

#### Service Layer:
Mainly holds the business validation, logic involved.

#### Command/Query Layer:
Service has CQRS pattern where all CUD operations are treated as commands & reads are treated as Queries. On successful execution of commands, would raise some events which further can be handled by any interested parties within like either sending mails/notifications, audit.. etc. CQRS helps you scale, provides performance boost & security.

#### Repository Layer:
Abstraction & separation of concern for DB related activities. Adds adds advantages like if we decide to switch to different DB in future, you don't need to touch upon entire codebase, simply replace/modify repository layer.

Few key acitivites which takes place within servie:
- Permission Middleware: Validates the users trying to access audit logs has necessary permissions or not
- ErrorHandler Middleware: Global exception handler
- SmartLockApiException: Custom exception specific to SmartLock API, which can handle error codes too in future.

## Table Mapping:
![image](https://user-images.githubusercontent.com/21059833/190972293-9c625672-695f-4acd-8280-67f96d779781.png)

## Testing:
Unit tests are written using xUnit

## What next?
Let's look at how this service can be extended to different level:
1. Auth Server: We can abstract out the token generation part to different service or provider like Identity server/Auth0 or something similar. 
2. We can also separate out the User & Role into different micro service which may not be needed as highly available and rest of the services can be made highly available in a different microservice which the current code would let you separate easily. Also we can make users & roles sync with Door service as eventual consistency via message broker.
3. We can hook up event sourcing pattern along with CQRS which would let us handle events and perform chain of operations.
4. Comparitively CUD operations would be lesser compared to reads hence, CQRS lets you separate out the readable & writable DB's by making use of queries & commands.

### NOTE:
In the MVP solution, i have seeded the Office/Door/User/Roles information from backend. In future we will be needing them for sure. Listing the possible API's needed here:

##### UserController
  - [HttpPost] api/users
  - [HttpPut] api/users
  - [HttpDelete] api/users
  - [HttpGet] api/users/{userId}
  - [HttpGet] api/users
##### RoleController
  - [HttpPost] api/roles
  - [HttpPut] api/roles
  - [HttpDelete] api/roles
  - [HttpGet] api/roles/{roleId}
  - [HttpGet] api/offices/{officeId}/roles - To list all roles in UI when adding door
  - [HttpGet] api/roles/{roleId}/users/{userId} - To list all users belongs to this role
 ##### OfficeController
  - [HttpPost] api/offices
  - [HttpPut] api/offices
  - [HttpDelete] api/offices  
 ##### DoorController
  - [HttpPost] api/doors
  - [HttpPut] api/doors
  - [HttpDelete] api/doors
  - [HttpGet] api/offices/{officeId}/doors - To list all doors within an office
  - [HttpGet] api/users/{userId}/doors - In UI users should see all doors they have access to
