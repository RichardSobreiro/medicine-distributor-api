namespace MedicinesDistributorApi.Repository.DatabaseSettings
{
    public class MedicineDistributorDatabaseSettings
    {
        public string ConnectionString { get; set; } = null!;

        public string DatabaseName { get; set; } = null!;

        public string ProductsCollectionName { get; set; } = null!;
        public string MeasurementUnitsCollectionName { get; set; } = null!;
        public string PaymentsCollectionName { get; set; } = null!;
    }
}
