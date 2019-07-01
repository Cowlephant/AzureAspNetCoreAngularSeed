# AzureAspNetCoreAngularSeed
Seed project for ASP.NET Core with Angular SPA and Azure Key Vault management.

## About
This project is intended to serve as a starting point for rapidly getting an application going with an `ASP.NET Core` back-end serving an `Angular` SPA front-end.

To manage server-side secrets, I have chosen to use `Azure Key Vault` which is an affordable way to protect and access secrets. When developing you'll likely need a `Client Id`
and `Client Secret`, but when it's actually hosted on Angular it only requires knowing the name of the Key Vault and ensuring your application is granted appropriate access.

This assumes the usage of Visual Studio, so some of the files are appropriate only for that IDE.

I will attempt to update this repository as updates come along for ASP.NET Core, Angular and Angular Material.

## Getting Started

### 1. Customize Project Name

There are several folders, files and entries within files that will be renamed to change things from `AzureaspNetCoreAngularSeed` to your project's name.

You can find and replace or whatever your preferred technique is to the following entries.

#### AzureAspNetCoreAngularSeed
  * **AzureAspNetCoreAngularSeed.sln** *(file name)* *(file contents - 3 occurences)*
  * **src\AzureAspNetCoreAngularSeed** *(folder name)*
  * **src\AzureAspNetCoreAngularSeed\AzureAspNetCoreAngularSeed.csproj** *(file name)*
  * **src\AzureAspNetCoreAngularSeed\Program.cs** *(file contents - 1 occurence)*
  * **src\AzureAspNetCoreAngularSeed\Startup.cs** *(file contents - 1 occurence)*
  * **src\AzureAspNetCoreAngularSeed\Properties\launchSettings.json** *(file contents - 1 occurence)*
  * **src\AzureAspNetCoreAngularSeed\ClientApp\karma.conf.js** *(file contents - 1 occurence)*
  * **src\AzureAspNetCoreAngularSeed\ClientApp\angular.json** *(file contents - 8 occurences)*
  * **src\AzureAspNetCoreAngularSeed\ClientApp\src\index.html** *(file contents - 1 occurences)*

#### azure-asp-net-core-angular-seed
  * **src\AzureAspNetCoreAngularSeed\ClientApp\package.json** *(file contents - 1 occurence)*
  
### 2. Creating an SSL Certificate

The client server will expect a developer `.crt` and .`key` files to be placed in src\<ProjectName>\ClientApp\ssl

If you wish to remove this requirement, you will need to perform the following steps
  * remove `--ssl true` parameters from the `start` and `hmr` scripts in `package.json`
  * remove the `sslKey` and `sslCert` options from `angular.json`
  * Find the line `spa.UseProxyToSpaDevelopmentServer("https://localhost:4200");` in Startup.cs and convert `https` to `http`

### 3. Add Secrets
  * Add `KeyVaultName` to your User Secrets json file. If this is being hosted in Azure, you'll want to add this as an environment variable.
  * Add `ClientId` to your User Secrets json file. This will be the client id of your Azure Active Directory App Registration.
  * Add `ClientSecret` to your User Secrets json file. This will be a user secret you've created for your Azure Active Directory App Registration.

## Angular Material

This project is currently configured to utilize Angular Material and Material Icons. All of the Angular Material modules are imported in the `CustomMaterialModule`.

If your project will not need all of these, remove the ones not needed from the import statement and the `import` and `export` sections of the `NgModule` configuration.

## Development server

### With Server SPA Proxy

If you have created an API from within this project to be called from Angular, when debugged the server will proxy to a separately served angular server at https://localhost:4200.

To access the application in your browser, you will want to navigate to http://localhost:58970 or https://localhost:44387

If there is not one detected, there will be an exception and the connection will not be made.

To serve the angular app, utilize the commands `npm run start` or `npm run hmr`. The latter will utilize webpack's [Hot Module Replacement](https://webpack.js.org/concepts/hot-module-replacement/).

### Without Server SPA Proxy

If you're only working on the client and have not yet built or do not need to access the server, you can simply utilize the command `npm run start` or `npm run hmr` and navigate to https://localhost:4200