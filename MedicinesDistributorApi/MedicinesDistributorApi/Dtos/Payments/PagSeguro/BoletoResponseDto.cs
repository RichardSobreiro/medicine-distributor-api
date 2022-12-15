namespace MedicinesDistributorApi.Dtos.Payments.PagSeguro
{
    public class BoletoResponseDto
    {
        public string id { get; set; } = string.Empty;
        public string reference_id { get; set; } = string.Empty;
        public string status { get; set; } = string.Empty;
        public string created_at { get; set; } = string.Empty;
        public string paid_at { get; set; } = string.Empty;
        public string description { get; set; } = string.Empty;
        public BoletoDto Boleto { get; set; } = new();

        public class BoletoDto
        {
            public string id { get; set; } = string.Empty;
            public string barcode { get; set; } = string.Empty;
            public string formatted_barcode { get; set; } = string.Empty;
            public string link { get; set; } = string.Empty;
        }
    }
}
