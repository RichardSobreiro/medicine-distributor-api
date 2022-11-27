using AutoMapper;
using MedicinesDistributorApi.Business.IBusiness;
using MedicinesDistributorApi.Dtos;
using MedicinesDistributorApi.Models;
using MedicinesDistributorApi.Repository.IRepository;

namespace MedicinesDistributorApi.Business
{
    public class MeasurementUnitsBusiness : IMeasurementUnitsBusiness
    {
        private readonly IMapper _mapper;
        readonly IMeasurementUnitsRepository _measurementUnitsRepository;

        public MeasurementUnitsBusiness(IMapper mapper, IMeasurementUnitsRepository measurementUnitsRepository)
        {
            _measurementUnitsRepository = measurementUnitsRepository;
            _mapper = mapper;
        }

        public async Task<List<MeasurementUnitDto>> GetAll()
        {
            try
            {
                var measurementUnits = await _measurementUnitsRepository.GetAsync();
                return _mapper.Map<List<MeasurementUnit>, List<MeasurementUnitDto>>(measurementUnits); ;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
