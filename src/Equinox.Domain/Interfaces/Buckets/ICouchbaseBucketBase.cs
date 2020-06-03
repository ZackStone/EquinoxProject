using Couchbase;
using Couchbase.KeyValue;
using Couchbase.Query;
using System.Threading.Tasks;

namespace Equinox.Domain.Interfaces.Buckets
{
    public interface ICouchbaseBucketBase<TEntity> where TEntity : class
    {
        public ICluster Cluster { get; }
        public IBucket Bucket { get; }
        public ICouchbaseCollection DefaultCollection { get; }

        Task<TEntity> GetById(string id, string collectionName = null);
        Task<IQueryResult<TEntity>> GetAll();
        Task<IQueryResult<TEntity>> QueryAsync(string statement);
    }
}
