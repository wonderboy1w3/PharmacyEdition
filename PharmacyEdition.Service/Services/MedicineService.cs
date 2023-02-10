using PharmacyEdition.Data.IRepositories;
using PharmacyEdition.Data.Repositories;
using PharmacyEdition.Models;
using PharmacyEdition.Service.DTOs;
using PharmacyEdition.Service.Helpers;
using PharmacyEdition.Service.Interfaces;

namespace PharmacyEdition.Service.Services;

public class MedicineService : IMedicineService
{
    private readonly GenericRepository<Medicine> genericRepository = new GenericRepository<Medicine>();
    //public MedicineService(GenericRepository<Medicine> geniricRepository)
    //{
    //    this.genericRepository = genericRepository;
    //}

    public async Task<Response<Medicine>> CreateAsync(MedicineCreationDto medicine)
    {
        var models = await this.genericRepository.GetAllAsync();
        var model = models.FirstOrDefault(x => x.Name == medicine.Name);
        if (model is not null)
        {
            model.Count += medicine.Count;
            await genericRepository.UpdateAsync(model.Id, model);

            return new Response<Medicine>()
            {
                StatusCode = 403,
                Message = "Medicine already exists",
                Value = null
            };
        }

        var mappedModel = new Medicine()
        {
            Name = medicine.Name,
            Description = medicine.Description,
            Count = medicine.Count,
            Price = medicine.Price,
        };
        var result = await this.genericRepository.CreateAsync(mappedModel);

        return new Response<Medicine>()
        {
            StatusCode = 200,
            Message = "Success",
            Value = result
        };
    }

    public async Task<Response<bool>> DeleteAsync(long id)
    {
        var model = await this.genericRepository.GetByIdAsync(id);
        if (model is null)
            return new Response<bool>()
            {
                StatusCode = 404,
                Message = "Medicine is not found",
                Value = false
            };

        await this.genericRepository.DeleteAsync(id);
        return new Response<bool>()
        {
            StatusCode = 200,
            Message = "Success",
            Value = true
        };
    }

    public async Task<Response<List<Medicine>>> GetAllAsync()
    {
        var result = await this.genericRepository.GetAllAsync();
        return new Response<List<Medicine>>()
        {
            StatusCode = 200,
            Message = "Success",
            Value = result
        };
    }

    public async Task<Response<Medicine>> GetByIdAsync(long id)
    {
        var model = await this.genericRepository.GetByIdAsync(id);
        if (model is null)
            return new Response<Medicine>()
            {
                StatusCode = 404,
                Message = "Medicine is not found",
                Value = null
            };

        return new Response<Medicine>()
        {
            StatusCode = 200,
            Message = "Success",
            Value = model
        };
    }

    public async Task<Response<Medicine>> UpdateAsync(long id, MedicineCreationDto medicine)
    {
        var model = await this.genericRepository.GetByIdAsync(id);
        if (model is null)
            return new Response<Medicine>()
            {
                StatusCode = 404,
                Message = "Medicine is not found",
                Value = null
            };

        var mappedModel = new Medicine()
        {
            Name = medicine.Name,
            Description = medicine.Description,
            Count = medicine.Count,
            Price = medicine.Price,
        };

        var result = await this.genericRepository.UpdateAsync(id, mappedModel);
        return new Response<Medicine>()
        {
            StatusCode = 200,
            Message = "Success",
            Value = result
        };
    }
}
