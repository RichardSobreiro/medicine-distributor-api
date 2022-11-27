using MedicinesDistributorApi.Models;

namespace MedicinesDistributorApi.Repository.IRepository
{
    public interface IMeasurementUnitsRepository
    {
        Task CreateAsync(MeasurementUnit product);
        Task UpdateAsync(string id, MeasurementUnit measurementUnit);
        Task RemoveAsync(string id);
        Task<MeasurementUnit?> GetAsync(string id);
        Task<List<MeasurementUnit>> GetAsync();
    }
}
