using Couchbase;
using Couchbase.Extensions.DependencyInjection;
using Couchbase.KeyValue;
using Couchbase.Query;
using Equinox.Domain.Interfaces.Buckets;
using System.Threading.Tasks;

namespace Equinox.Infra.Data.Couchbase
{
    public class CouchbaseBucketBase<TEntity> : ICouchbaseBucketBase<TEntity> where TEntity : class
    {
        public ICluster Cluster { get; private set; }
        public IBucket Bucket { get; private set; }
        public ICouchbaseCollection DefaultCollection { get; private set; }

        public CouchbaseBucketBase(IClusterProvider clusterProvider, string bucketName)
        {
            Cluster = clusterProvider.GetClusterAsync().GetAwaiter().GetResult();
            Bucket = Cluster.BucketAsync(bucketName).GetAwaiter().GetResult();
            DefaultCollection = GetCollection();
        }

        protected ICouchbaseCollection GetCollection(string collectionName = null) =>
            !string.IsNullOrWhiteSpace(collectionName) ?
            Bucket.Collection(collectionName) : Bucket.DefaultCollection();

        public async Task<TEntity> GetById(string id, string collectionName = null)
        {
            var collection = GetCollection(collectionName);
            var getResult = await collection.GetAsync(id.ToString());
            var entity = getResult.ContentAs<TEntity>();
            return entity;
        }

        public async Task<IQueryResult<TEntity>> GetAll()
        {
            string statement = $"select x.* from {Bucket.Name} x";
            var queryResult = await QueryAsync(statement);
            return queryResult;
        }

        public async Task<IQueryResult<TEntity>> QueryAsync(string statement)
        {
            var queryResult = await Cluster.QueryAsync<TEntity>(statement);
            return queryResult;
        }
    }
}
