OData Uri Helper implementation in .NET

Helps build OData URIs and queries according to the [OData v4.01 Documentation](https://www.odata.org/documentation).

### Installing ODataUriHelper

You should install [ODataUriHelper with NuGet](http://www.nuget.org/packages)

    Install-Package ODataUriHelper

Or via the .Net Core command line interface:

    dotnet add package OdataUriHelper

Either commands, from Package Manager Consol or .NET Core CLI, will download and install ODataUriHelper and all required dependencies.

### Usage

### Registering with `IServicesCollection`

To register ODataUriHelper:

```
services.AddODataUriHelper();
```

This registers:

- `ODataUriBuilder` as transient.
