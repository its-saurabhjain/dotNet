The repository and unit of work patterns are intended to create an abstraction layer between the data access layer and the business logic layer of an application. 
Implementing these patterns can help insulate your application from changes in the data store and can facilitate automated unit testing or test-driven development (TDD).


The unit of work class coordinates the work of multiple repositories by creating a single database context class shared by all of them. 
If you wanted to be able to perform automated unit testing, you'd create and use interfaces for these classes in the same way you did for the Student repository. 

https://docs.microsoft.com/en-us/aspnet/mvc/overview/older-versions/getting-started-with-ef-5-using-mvc-4/implementing-the-repository-and-unit-of-work-patterns-in-an-asp-net-mvc-application



