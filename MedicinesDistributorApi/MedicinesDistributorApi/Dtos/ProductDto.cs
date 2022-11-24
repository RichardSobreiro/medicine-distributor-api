﻿namespace MedicinesDistributorApi.Dtos
{
    public class ProductDto
    {
        public string? Id { get; set; } = "";
        public string Name { get; set; } = "";
        public string Description { get; set; } = "";
        public List<ConcentrationDto> SelectedDrugsConcentrations { get; set; } = new();
    }
}
