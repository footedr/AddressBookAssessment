# Dayton Freight Lines - Coding Assessment
### Address Book API
This is the beginnings of a address book API implemenation. 

It is written with the c sharp language and leverages dotnet10, ASPNET Core, and Entity Framework Core.

It utilizes several development patterns such as Domain Driven Design (DDD), CQRS (command/query responsiblity segregation), and mediator. The purpose is to serve as a 'proving grounds' of sorts, in order to assess the candidates ability to follow instructions and add a set of features given the existing architecture. The idea being, can the candidate follow existing patterns and practices in order to make a value-add change to the repository.

The 'create' action has been coded end-to-end. This assessment requires you to complete the 'list', 'update', and 'delete' actions. 

Please complete the 'list', 'update', and 'delete' actions, and submit a pull request.

* Clone the repo to your local device
* Open Visual Studio
* Click 'File -> Open Project/Solution'
* Find and open the project folder that you cloned
* Select 'AddressBookAssessment.slnx' and click 'Open'
* Once open, click the 'Build' menu and select 'Rebuild Solution'
* In the Solution Explorer, ensure that 'AddressBookAssessment.Services.AppHost' is in bold. If not, right-click that project and select 'Set as Startup Project'
* Click the play button on the toolbar, or hit F5 to run the solution
* A browser window should open. This is the application's 'Aspire' dashboard.
* Once the state of each service displays a green check icon and is 'Running', you can click the link for 'Open API Docs' in the 'URLs' column for the 'addressbookassessment-services-web' service. This should open a new browser tab showing the ScalarUI representation of the OpenAPI document. You should be able to test the 'Create Address Book Entry' endpoint.
* Hint: the endpoint requires an 'API Key'. You should be able to find it in a settings file. You'll have to poke around to find it.


You will need Visual Studio to complete this exercise. You can download [here](https://visualstudio.microsoft.com/downloads/).

Note that the Community version is free for students, OSS contributors, and individuals. Professional offers a free trial. Either should suffice.