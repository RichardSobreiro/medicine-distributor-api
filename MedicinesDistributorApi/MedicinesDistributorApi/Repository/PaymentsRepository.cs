using MedicinesDistributorApi.Models.Payments;
using MedicinesDistributorApi.Repository.DatabaseSettings;
using MedicinesDistributorApi.Repository.IRepository;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace MedicinesDistributorApi.Repository
{
    public class PaymentsRepository : IPaymentsRepository
    {
        private readonly IMongoCollection<Payment> _paymentsCollection;

        public PaymentsRepository(
            IOptions<MedicineDistributorDatabaseSettings> medicineDistributorDatabaseSettings)
        {
            var mongoClient = new MongoClient(
            medicineDistributorDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                medicineDistributorDatabaseSettings.Value.DatabaseName);

            _paymentsCollection = mongoDatabase.GetCollection<Payment>(
                medicineDistributorDatabaseSettings.Value.PaymentsCollectionName);
        }

        public async Task CreateAsync(Payment payment)
        {
            await _paymentsCollection.InsertOneAsync(payment);
        }

        public async Task<Payment?> GetAsync(string id)
        {
            return await _paymentsCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<List<Payment>> GetAsync()
        {
            return await _paymentsCollection.Find(_ => true).ToListAsync();
        }

        public async Task<List<Payment>> GetByEmailAsync(string email)
        {
            var filter = Builders<Payment>.Filter.Regex("UserEmail", new BsonRegularExpression($"/.*{email}.*/mi"));
            return await _paymentsCollection.Find(filter).ToListAsync();
        }

        public async Task RemoveAsync(string id)
        {
            await _paymentsCollection.DeleteOneAsync(x => x.Id == id);
        }

        public async Task UpdateAsync(string id, Payment product)
        {
            await _paymentsCollection.ReplaceOneAsync(x => x.Id == id, product);
        }
    }
}
