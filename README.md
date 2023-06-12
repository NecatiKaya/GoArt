# GoArt-MiniWallet

Backend Api project written in C# with Vertical Slice Application Architecture manner

## Development Equipment

1. MacBook Pro M2 Max 
2. VS 2022 For Mac
3. Postman
4. MS SQL Edge On Docker

### Prerequisites

1. To run the application you need to compile with NET compiler. Easy way is to load solution on Visual Studio and run it. 
2. Please dont forget to change connection string which is inside appsettings.json file to point MS SQL Server. 
3. Create a databse on MS SQL called GoArt.
4. Run the script (GoArt.sql) placed  **GoArt.Applications.MiniWallet.Api** project **setup folder**. 
5. Ensure the sript creates 2 tables insde GoArt database

### Testing

1. Run the **GoArt.Applications.MiniWallet.Api** project from VS 2022
2. Import postman collection scripts from **setup folder** located in **GoArt.Applications.MiniWallet.Api** project.
3. You can call any api inside the postman collection

### Architecture

During development, 2 project is created on the solution. One for the Application and the one for the application hosting. 

1. **GoArt.Applications.MiniWallet:** is the core application contains domain/business logic and services.
2. **GoArt.Applications.MiniWallet.Api:** is a hosting application. May contain middleware, appsettings and configuration to run the app.

### Techniques/Frameworks/Coding Styles Used

Frameworks packages which were used are as below:

1. Dapper for db objects mapping to domain object mappings
2. ADO.NET to run queries
3. CQRS to seperate command andd queries.
4. Mediator to use CQRS
5. FluentValidation to validate Api request
6. DDD to create domain objects. Objects contains business during creation. You can check DDD objects inside **GoArt.Applications.MiniWallet** project. There should be a domain folder.
7. You can see a **Problem** type in the application, to return a problem for the results other than HTTP 2XX. The type is used to give a lot more information to user about why he/she got error
8. Repository operations are located inside **repository** folder
9. Localization is also added as a feature to the application. Only English and Turkish langs are supported for easy development. To see error details in English or Turkish, Accept-Language header should be added to API headers and must have only one value en or tr. If the header is not specified English lang is default
10. Inside the application there should be a **Features** folder. By structering like this, it is easyly understandable that the app has features like CreateWallet, Deposit, Withdraw. Inside each feature there request, response and handler types to be used my Meditor. Handler has the business logic.

### Domain Objects

As said before, domain objects and value types are created inside Domain folder in MiniWallet applcation. 

Value types are created using as **record** objects. Also, they contain business logic during creation so that even duplicate validations are no need to be written. For example, only USD, TRY, GBP and EURO curriencies are allowed. For every api request which has currency parameter, no need to be validated via FluentValidation. Because the business is inside the type. Another example is money amount type. This type must point an amoun greater than zero

Type          | Decsription
------------- | -------------
Wallet        | Aggregate root for the app. Contains operation for deposit, withdraw etc. 
MoneyTransactionCollection  | Container for transactions. Also can be thought as a transactions view
Currency | Value Type to hold a currency value. Currencies other than TRY, USD, GBP, EUR can not be created
MoneyAmount | Value Type to show amount of money to deposit/withdraw. For the currency seperator problem between country to country, this type has two fields. Whole part and penny part. For example, 20.50 TRY is shown as 20 for whole part property, 50 is for penny part property
MoneyAmountWithCurrency | Value Type contain MoneyAmount plus currency

### What Can Be Done Else

1. Project structure can be seperated into multiple projects. For example, Core folder inside application project can be a seperate project. Also for repository 
2. Caching can be used for many things such as currency converting, localization. 
3. Api Logging
