using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet8.APIEndpoints;

public static class ServiceProviderExtensions
{
    public static TWorkerType GetHostedService<TWorkerType>
        (this IServiceProvider serviceProvider) =>
        serviceProvider
            .GetServices<IHostedService>()
            .OfType<TWorkerType>()
            .FirstOrDefault();
}

