using MongoDB.Bson.Serialization.Attributes;

namespace MedicinesDistributorApi.Dtos
{
    public class MeasurementUnitDto
    {
        public string? MeasurementUnitId { get; set; }
        public string MeasurementUnitDescription { get; set; } = "";
        public decimal PurchasePrice { get; set; }
        public decimal SellingPrice { get; set; }
    }
}
