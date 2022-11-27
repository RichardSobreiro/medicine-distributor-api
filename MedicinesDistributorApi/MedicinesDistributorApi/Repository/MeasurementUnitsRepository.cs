using MedicinesDistributorApi.Models;
using MedicinesDistributorApi.Repository.DatabaseSettings;
using MedicinesDistributorApi.Repository.IRepository;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace MedicinesDistributorApi.Repository
{
    public class MeasurementUnitsRepository : IMeasurementUnitsRepository
    {
        private readonly IMongoCollection<MeasurementUnit> _measurementUnitsCollection;

        public MeasurementUnitsRepository(
            IOptions<MedicineDistributorDatabaseSettings> medicineDistributorDatabaseSettings)
        {
            var mongoClient = new MongoClient(
            medicineDistributorDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                medicineDistributorDatabaseSettings.Value.DatabaseName);

            _measurementUnitsCollection = mongoDatabase.GetCollection<MeasurementUnit>(
                medicineDistributorDatabaseSettings.Value.MeasurementUnitsCollectionName);
        }

        public async Task CreateAsync(MeasurementUnit measurementUnit)
        {
             await _measurementUnitsCollection.InsertOneAsync(measurementUnit);
        }

        public async Task<MeasurementUnit?> GetAsync(string id)
        {
            return await _measurementUnitsCollection.Find(x => x.MeasurementUnitId == id).FirstOrDefaultAsync();
        }

        public async Task<List<MeasurementUnit>> GetAsync()
        {
            return await _measurementUnitsCollection.Find(_ => true).ToListAsync();
        }

        public async Task RemoveAsync(string id)
        {
            await _measurementUnitsCollection.DeleteOneAsync(x => x.MeasurementUnitId == id);
        }

        public async Task UpdateAsync(string id, MeasurementUnit measurementUnit)
        {
            await _measurementUnitsCollection.ReplaceOneAsync(x => x.MeasurementUnitId == id, 
                measurementUnit);
        }
    }
}
