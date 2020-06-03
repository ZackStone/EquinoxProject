using Couchbase.Extensions.DependencyInjection;
using Equinox.Domain.Interfaces.Buckets;
using Equinox.Domain.Models;

namespace Equinox.Infra.Data.Couchbase
{
    public class CustomerBucket : CouchbaseBucketBase<Customer>, ICustomerBucket
    {
        //public ICouchbaseCollection OtherNonDefaultCollection { get; private set; }

        public CustomerBucket(IClusterProvider clusterProvider) 
            : base(clusterProvider, "customers")
        {
            //OtherNonDefaultCollection = GetCollection("OtherNonDefault");
        }
    }
}
