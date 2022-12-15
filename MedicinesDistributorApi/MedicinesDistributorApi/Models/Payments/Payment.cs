using MedicinesDistributorApi.PagSeguro;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MedicinesDistributorApi.Models.Payments
{
    [BsonIgnoreExtraElements]
    public class Payment
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        public string CpfCnpj { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public bool BillingEqualShippingAddress { get; set; } = true;
        public ShoppingCartModel ShoppingCart { get; set; } = new();
        public Address BillingAddress { get; set; } = new();
        public Address ShippimentAddress { get; set; } = new();
        public bool Boleto { get; set; } = true;
        public bool Pix { get; set; } = false;
        public bool CreditCard { get; set; } = false;
        public DateTime DueDate { get; set; }
        public string Status { get; set; } = string.Empty;
        public BoletoResponse BoletoResponse { get; set; } = new();

        public class Address
        {
            public string Cep { get; set; } = string.Empty;
            public string Street { get; set; } = string.Empty;
            public string Complement { get; set; } = string.Empty;
            public string District { get; set; } = string.Empty;
            public string City { get; set; } = string.Empty;
            public string State { get; set; } = string.Empty;
            public string Number { get; set; } = string.Empty;
        }
        public class ShoppingCartModel
        {
            public string ShoppingCartId { get; set; } = "";
            public List<ProductInCart> ProductsInCart { get; set; } = new();
        }
        public class ProductInCart
        {
            public string? Id { get; set; } = "";
            public string Name { get; set; } = "";
            public string Description { get; set; } = "";
            public string? MeasurementUnitId { get; set; }
            public List<ConcentrationInCart> Concentrations { get; set; } = new();
        }
        public class ConcentrationInCart
        {
            public double ConcentrationValue { get; set; }
            public string ConcentrationDescription { get; set; } = "";
            public decimal SellingPrice { get; set; }
            public int Quantity { get; set; }
        }
    }
}
