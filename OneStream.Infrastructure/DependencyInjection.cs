using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneStream.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection Addinfrastructure(this IServiceCollection services)
        {
            return services;
        }
    }
}
