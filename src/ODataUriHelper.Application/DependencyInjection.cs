using ODataUriHelper.Application;

namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static void AddODataUriHelper(IServiceCollection services)
    {
        services.AddTransient<ODataUriBuilder>();
    }
}
