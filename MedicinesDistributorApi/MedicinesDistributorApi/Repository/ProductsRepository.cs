using MedicinesDistributorApi.Models;
using MedicinesDistributorApi.Repository.DatabaseSettings;
using MedicinesDistributorApi.Repository.IRepository;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace MedicinesDistributorApi.Repository
{
    public class ProductsRepository : IProductsRepository
    {
        private readonly IMongoCollection<Product> _productsCollection;

        public ProductsRepository(
            IOptions<MedicineDistributorDatabaseSettings> medicineDistributorDatabaseSettings)
        {
            var mongoClient = new MongoClient(
            medicineDistributorDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                medicineDistributorDatabaseSettings.Value.DatabaseName);

            _productsCollection = mongoDatabase.GetCollection<Product>(
                medicineDistributorDatabaseSettings.Value.ProductsCollectionName);
        }

        public async Task CreateAsync(Product product)
        {
            await _productsCollection.InsertOneAsync(product);
        }

        public async Task<Product?> GetAsync(string id)
        {
            return await _productsCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<List<Product>> GetByNameAsync(string partialName)
        {
            //$"/^{partialName}$/i"
            ///.*dipiron.*/mig
            var filter = Builders<Product>.Filter.Regex("Name", new BsonRegularExpression($"/.*{partialName}.*/mi"));
            return await _productsCollection.Find(filter).ToListAsync();
        }

        public async Task<List<Product>> GetAsync()
        {
            return await _productsCollection.Find(_ => true).ToListAsync();
        }

        public async Task RemoveAsync(string id)
        {
            await _productsCollection.DeleteOneAsync(x => x.Id == id);
        }

        public async Task UpdateAsync(string id, Product product)
        {
            await _productsCollection.ReplaceOneAsync(x => x.Id == id, product);
        }
    }
}
