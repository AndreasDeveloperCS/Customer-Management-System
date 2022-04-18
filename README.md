# Customer-Management-System

Requirements:
Implement an API in C# which will manage Customers and Orders.
No user interface is required, only an API to create, update and delete Customers and manage Orders. 
Customer entity should contain: name (first and last), address, postal code. 
Each Order should contain one or more Items, order date and total price. 
Item should contain Product and quantity of product and Product should contain name and price.

 For persistence entity framework should be used and repository pattern.

 Also, unit of work pattern is required to be implemented.

 In addition to creating/editing/deleting it is required also to support iterating over the customer orders by order date.

 Implement focusing on domain logic using Domain Driven Design.

 Segregate commands and queries using CQRS.

 Include unit tests in your project (preferably with NUnit)

 XML documentation of the API

 A short list of the assumptions that you made when designing/implementing the API.
