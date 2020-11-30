# USD-GTQ-ExchangeRate
Banco de Guatemala exchange rate USD/GTQ
ASP .NET Framework API and .NET Console application.

## Description

* API for data access (Part 1) is on the Currency folder
* Monitor application (Part 2) is located on CurrencyClient folder

## Database Access

The database model was generated using the EDM feature, to generate the database please use the "Generate Database from Model..." option within the ExchangeDb.edmx also the output SQL script is located on the root folder, execute it if necesary.

The database context uses a connectionStrings named ExchangeEntities, the connection string have defined the user sa with the password root, if the password for the user sa is differente please change it accordingly or update the Password in the Web.config configuration file Line 18:301.

## Console Application Considerations

Within the App.config are both the refresh rate configuration (RefreshRate) and the API URL (Url) value, it might be necesary to change the port in case the REST API re-deployment uses a different port from the one defined in the file.
