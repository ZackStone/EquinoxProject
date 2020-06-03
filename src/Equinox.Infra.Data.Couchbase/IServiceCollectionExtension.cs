using Couchbase.Extensions.DependencyInjection;
using Equinox.Domain.Interfaces.Buckets;
using Microsoft.Extensions.DependencyInjection;

namespace Equinox.Infra.Data.Couchbase
{
    public static class IServiceCollectionExtension
    {
        public static IServiceCollection AddCouchbaseConnector(this IServiceCollection services)
        {
            services.AddCouchbase(opt =>
            {
                opt.ConnectionString = "couchbase://localhost";
                opt.UserName = "Administrator";
                opt.Password = "asd123";
                opt.Buckets = new[] { "customers" };
            });

            services.AddTransient<ICustomerBucket, CustomerBucket>();

            return services;
        }
    }
}
