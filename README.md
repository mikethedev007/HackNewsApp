# HackNewsApp


This Web API App was written using .NET Core 8.0 C# and Visual Studio 2026 Community Edition (version 18.8.2). 
It makes API calls to Hacker News API to retrieve a requested number of the best stories. the App's API  allows you 
state how many of the best stories you want to retrieve and also sort the data by certain fields.

- The API Endpoints are protected by JWT Bearer Tokens and Security Policies (ASP.NET Core Identity). 
- It uses Clean Architecture to structure the code and functionality. 
- It uses Caching to store retrieved stories thereby not making extra trips to external API and improving performance (local IMemoryCache). 
- It uses best practice Repository, Service, Result software Design Patterns. 
- API versioning and Swagger versioning are implemented. 
- Pagination, Filtering and Sorting are implemented. 
- Error are captured and details are structured with ProblemDetails which the API returns back.
- API Rate Limiter Throttling as well as Polly Library's API Retry, Circuit Breaking are implemented to protect external API usage and performance. 
- Fluent Validation is used to provide validation on DTOs and other Models.

Future improvements include:
- Include more  Unit and Integration Tests.
- Database implementation.
- Better consistent error handling messages and logging (use serilog).
- Using Parallel.ForEachAsync approach for retrieving the stories back from Hacker News API. 
- Include more health checks.




INSTRUCTIONS to run HackNewsApp API in Visual Studio and test it in Swagger UI. 

1. Open project in Visual Studio.

2. Click on button to run HackNews.Api main startup project.

3. The Browser should open and Swagger Development API UI should load with list of API calls to test functionality.

4. In order to use the API, you need to authenticate first as the API Endpoints are protected by JWT Bearer Authentication.

5. To Authenticate, First run the Auth (/api/v1/Auth/login) API Call.

6. Click on the Auth (/api/v1/Auth/login) API Call row and the API details should appear with a Try it out button.

7. Click on the Try it out button.

8. Enter "admin" for username and "password" for password.

9. Click on the Execute button.

10. You should receive Access Token value in the body of the response with status code 200. It will consist of the access token value 
and it's expiration time for the Authenticated JWT Bearer Token.

11. Copy the actual access token value (with out the quotation marks).

12. Go to the top of the Swagger page and there should be an Authorize button. Click on the Authorize button.

13. Paste the acxcess token value into the Bearer Value Textbox and click on the Authorize button.

14. Click on the Close button and you can now make the HackNews API Call to retrieve N Stories from Hacker News API.

15. Click on the HackNews (/api/v1/HackNews) API Call row and the API details should appear with a Try it out button.

16. Click on the Try it out button.

17. If you want to change the number of stories retrieve the number of Stories from Hacker News API then enter the value in the PageSize textbox, 
otherwise it defaults to 10. The Other vaules are left blank if no values are entered except the SortByField which defaults to sorting the 
stories by Score Descending ("score_desc").

Sort values: Sort By Score  = "score" for Ascending OR "score_desc" for Descending Sort By Title = "title" for Ascending OR "title_desc" for Descending, 
Sort By Story Publishing Time = "title" for Ascending OR "title_desc" for Descending.

18. Click on the Execute button.

10. You should receive List of N Stories in the body of the response with status code 200. 

