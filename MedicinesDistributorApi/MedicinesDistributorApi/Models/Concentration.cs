using MongoDB.Bson.Serialization.Attributes;

namespace MedicinesDistributorApi.Models
{
    [BsonIgnoreExtraElements]
    public class Concentration
    {
        public double ConcentrationValue { get; set; }
        public decimal PurchasePrice { get; set; }
        public decimal SellingPrice { get; set; }
        public int TotalStockQuantity { get; set; }
    }
}
