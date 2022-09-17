using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Security.IoC
{
    public static class ServiceTool
    {
        public static IServiceProvider serviceProvider { get; private set; }
       public static IServiceCollection Create(IServiceCollection services)
        {
            serviceProvider = services.BuildServiceProvider();
            return services;
        }
    }
}
