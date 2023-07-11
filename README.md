# PoqApplication
Tech assignement for POQ project

This is an implementation of tech task, actual requirments are here
[Tech Assesment](https://docs.google.com/viewer?url=https://drive.google.com/file/d/19JF7Ta7PuCknoR1lMfYcWxUCZktC0ysK/view?usp=sharing)

Technologies which are being used:
- .NET 6
- Serilog
- AutoMapper
- xUnit 

## How it is working?

As suggested in assignement API uses mocky.io to get JSON with products and filter it according to parameters:

``` /filter?minprice=5&maxprice=20&size=medium&highlight=green,blue ```

- MinPrice
- MaxPrice
- Size
- Highlight

MinPrice and MaxPrice are being used to limit number of products by value of their Price field.

Size is the parameter which describes us which size of a product user is looking for.

Highligh helps us to find keywords in descpription of filtered products and highlight them.

(!) Endpoint does not correspond to REST conventions, but this was strongly suggested by requirments of the assignement.

1. All products if the request has no parameters
2. A filtered subset of products if the request has filter parameters
3. A filter object to contain:
   - The minimum and the maximum price of all products in the source URL
   - An array of strings to contain all sizes of all products in the source URL
   - A string array of size ten to contain most common words in the productdescriptions, excluding the most common five.
   
## Are requirments satisfied?

- Clean, readable, easy-to-understand code ( is explained below )
- Performance, scalability, and security ( is explained below )
- Unit tests +
- Dependency injection +
- Appropriate logging including the full mocky.io response +
- Documentation for the users of your API +

## Which decisions were made?

1) Application is splitted to 4 project
   - Poq.Api
   Responsible for handling Request, validating input parameters and forming response model. Also Exception Middleware is implemented inside.
   - Poq.Application
   Responsible for business logic.
   - Poq.Infrtastructure
   Responsible for getting products data.
   - Poq.UnitTests
   Contains unit tests.
   
   This decision allows to separate logical entities and easy change them in case of a need.
2) Previous decision also gives strong bound to a ProductClient.cs which is the only way of getting data. This can be reworked in future, but developer was limited in the time.

3) Application layer is using static *Helper.cs classes to simplify code and make it cleaner.

## What can be improved?

- Cacheing can be added as an option to improve performance and to avoid API rate limiting of the data source.

## How to use?
You can download .zip, open it in your IDE and launch it with using default launch configuration. Here is step-by-step guide using VisualStudio2022.
After getting .zip and unzipping it into appropriate folder launch VisualStudio.

![image](https://user-images.githubusercontent.com/44468481/220938203-fc681693-c28f-452e-83b7-a03e8668ebba.png)

select "Open project or solution" then navigate to your folder and select PoqApplication.sln.

![image](https://user-images.githubusercontent.com/44468481/220939233-919a461f-aa68-4f9d-ae91-4c8c815a1bcd.png)

***!!! Important***
You will need to add these values in MockyConfiguration section of appsettings.json.They are deleted from appsettings.json in terms of security.

    "BaseUrl": "http://www.mocky.io/v2",
    "ApiKeyy": "5e307edf3200005d00858b49"

Also in this file you can change default log level to see trace logs.

In the very top click on the "Run" icon and you are all set to use this API.

Or you can use following commands in CLI to run application.

``` cd {yourFolderPath}\Poq.Api\ ```

```dotnet run ```

Thanks for your attention!
