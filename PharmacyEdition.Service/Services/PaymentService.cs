using PharmacyEdition.Data.IRepositories;
using PharmacyEdition.Data.Repositories;
using PharmacyEdition.Domain.Entities;
using PharmacyEdition.Service.DTOs;
using PharmacyEdition.Service.Helpers;
using PharmacyEdition.Service.Interfaces;

namespace PharmacyEdition.Service.Services;

public class PaymentService : IPaymentService
{
    private readonly GenericRepository<Payment> genericRepository = new GenericRepository<Payment>();

    public async Task<Response<Payment>> CreateAsync(PaymentCreationDto payment)
    {
        var mappedModel = new Payment()
        {
            Type = payment.Type,
            OrderId = payment.OrderId,
            IsPaid = true
        };
        var result = await this.genericRepository.CreateAsync(mappedModel);

        return new Response<Payment>()
        {
            StatusCode = 201,
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
                Message = "Payment is not found",
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

    public async Task<Response<List<Payment>>> GetAllAsync()
    {
        var models = await this.genericRepository.GetAllAsync();
        return new Response<List<Payment>>()
        {
            StatusCode = 200,
            Message = "Success",
            Value = models
        };
    }

    public async Task<Response<Payment>> GetByIdAsync(long id)
    {
        var model = await this.genericRepository.GetByIdAsync(id);
        if (model is null)
            return new Response<Payment>()
            {
                StatusCode = 404,
                Message = "Payment is not found",
                Value = null
            };

        return new Response<Payment>()
        {
            StatusCode = 200,
            Message = "Success",
            Value = model
        };
    }

    public async Task<Response<Payment>> UpdateAsync(long id, PaymentCreationDto payment)
    {
        var model = await this.genericRepository.GetByIdAsync(id);
        if (model is null)
            return new Response<Payment>()
            {
                StatusCode = 404,
                Message = "Payment is not found",
                Value = null
            };

        var mappedModel = new Payment()
        {
            Type = payment.Type
        };
        var result = await this.genericRepository.UpdateAsync(id, mappedModel);

        return new Response<Payment>()
        {
            StatusCode = 200,
            Message = "Success",
            Value = result
        };
    }
}
