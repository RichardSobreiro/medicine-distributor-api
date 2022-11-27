using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MedicinesDistributorApi.Models
{
    [BsonIgnoreExtraElements]
    public class MeasurementUnit
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? MeasurementUnitId { get; set; }
        public string MeasurementUnitDescription { get; set; } = "";
        public decimal PurchasePrice { get; set; }
        public decimal SellingPrice { get; set; }
    }
}
