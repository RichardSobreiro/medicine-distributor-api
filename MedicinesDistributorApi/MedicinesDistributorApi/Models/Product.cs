using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MedicinesDistributorApi.Models
{
    [BsonIgnoreExtraElements]
    public class Product
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        public string Name { get; set; } = "";
        public string Description { get; set; } = "";
        public DateTime CreationDate { get; set; }
        public DateTime LastUpdated { get; set; }
        public string? MeasurementUnitId { get; set; }
        public List<Concentration> SelectedDrugsConcentrations { get; set; } = new();
        public decimal PurchasePrice { get; set; }
        public decimal SellingPrice { get; set; }
        public int TotalStockQuantity { get; set; }
    }
}
