using MedicinesDistributorApi.Dtos;

namespace MedicinesDistributorApi.Business.IBusiness
{
    public interface IMeasurementUnitsBusiness
    {
        Task<List<MeasurementUnitDto>> GetAll();
    }
}
