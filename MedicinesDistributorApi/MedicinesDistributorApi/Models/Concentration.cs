namespace MedicinesDistributorApi.Models
{
    public class Concentration
    {
        public string MeasurementUnitId { get; set; }
        public double ConcentrationValue { get; set; }
        public string ConcentrationDescription { get; set; } = "";
    }
}
