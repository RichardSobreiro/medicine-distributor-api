namespace MedicinesDistributorApi.PagSeguro
{
    public class BoletoRequest
    {
        public string reference_id { get; set; } = string.Empty;
        public string description { get; set; } = string.Empty;
        public Amount amount { get; set; } = new();
        public PaymentMethod payment_method { get; set; } = new();
        public List<string> notification_urls { get; set; } = new();

        public class Amount
        {
            public decimal value { get; set; } = 0;
            public string currency { get; set; } = "BRL";
        }
        public class PaymentMethod 
        {
            public string type { get; set; } = string.Empty;
            public Boleto boleto { get; set; } = new();
        }
        public class Boleto
        {
            public DateTime due_date { get; set; }
            public InstructionLines instruction_lines { get; set; } = new();
            public Holder holder { get; set; } = new();
        }
        public class InstructionLines
        {
            public string line_1 { get; set; } = string.Empty;
            public string line_2 { get; set; } = string.Empty;
        }
        public class Holder
        {
            public string name { get; set; } = string.Empty;
            public string tax_id { get; set; } = string.Empty;
            public string email { get; set; } = string.Empty;
            public Address address { get; set; } = new();
        }
        public class Address
        {
            public string street { get; set; } = string.Empty;
            public string number { get; set; } = string.Empty;
            public string locality { get; set; } = string.Empty;
            public string city { get; set; } = string.Empty;
            public string region { get; set; } = string.Empty;
            public string region_code { get; set; } = string.Empty;
            public string country { get; set; } = string.Empty;
            public string postal_code { get; set; } = string.Empty;
        }
    }
}
