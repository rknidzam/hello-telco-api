
# HelloTelco (REST API)

A simple REST API backend that provides functionalities below to the client app:

- Get all phone numbers.
- Get all phone numbers of a single customer.
- Add a new phone number to a customer’s account.
- Activate a phone number.

## Main Features

Application is built with two main controllers:
- CustomersController: To manage customer data.
- PhoneNumbersController: To manage phone number data.

## Environment

Application is built using these setup:
- .NET 8 LTS

## Database

- Microsoft SQL Server as datastore
- Entity Framework Core

## How to run locally

- Setup .NET SDK / Runtime
- Setup local MSSQL
- Add local database default connection to appsettings.json
- Perform EF Core migration
- Run command 'dotnet run'