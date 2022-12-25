using MedicinesDistributorApi.Dtos.Payments.PagSeguro;

namespace MedicinesDistributorApi.Dtos.Payments
{
    public class PaymentDto
    {
        public string UserEmail { get; set; } = string.Empty;

        public string CpfCnpj { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public bool BillingEqualShippingAddress { get; set; } = true;
        public ShoppingCartDto ShoppingCart { get; set; } = new();
        public AddressDto BillingAddress { get; set; } = new();
        public AddressDto ShippimentAddress { get; set; } = new();
        public bool Boleto { get; set; } = true;
        public bool Pix { get; set; } = false;
        public bool CreditCard { get; set; } = false;
        public DateTime DueDate { get; set; }
        public string Status { get; set; } = string.Empty;
        public BoletoResponseDto BoletoResponse { get; set; } = new();

        public class AddressDto
        {
            public string Cep { get; set; } = string.Empty;
            public string Street { get; set; } = string.Empty;
            public string Complement { get; set; } = string.Empty;
            public string District { get; set; } = string.Empty;
            public string City { get; set; } = string.Empty;
            public string State { get; set; } = string.Empty;
            public string Number { get; set; } = string.Empty;
        }
        public class ShoppingCartDto
        {
            public string ShoppingCartId { get; set; } = "";
            public List<ProductInCartDto> ProductsInCart { get; set; } = new();
        }
        public class ProductInCartDto
        {
            public string? Id { get; set; } = "";
            public string Name { get; set; } = "";
            public string Description { get; set; } = "";
            public string? MeasurementUnitId { get; set; }
            public List<ConcentrationInCartDto> Concentrations { get; set; } = new();
        }
        public class ConcentrationInCartDto
        {
            public double ConcentrationValue { get; set; }
            public string ConcentrationDescription { get; set; } = "";
            public decimal SellingPrice { get; set; }
            public int Quantity { get; set; }
        }
    }
}
