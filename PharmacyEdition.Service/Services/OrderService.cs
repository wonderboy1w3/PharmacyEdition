using PharmacyEdition.Data.IRepositories;
using PharmacyEdition.Data.Repositories;
using PharmacyEdition.Domain.Entities;
using PharmacyEdition.Domain.Enums;
using PharmacyEdition.Service.DTOs;
using PharmacyEdition.Service.Helpers;
using PharmacyEdition.Service.Interfaces;

namespace PharmacyEdition.Service.Services;

public class OrderService : IOrderService
{
    private readonly GenericRepository<Order> genericRepository = new GenericRepository<Order>();
    private readonly IPaymentService paymentService = new PaymentService();

    public async Task<Response<Order>> CreateAsync(OrderCreationDto order)
    {
        // Checking that there are enough medicines or they exist
        foreach (var med in order.Medicines)
        {
            var ourMed = (await medicineService.GetByIdAsync(med.Id)).Value;

            // Checking for existence of medicine
            if (ourMed is null)
                return new Response<Order>
                {
                    StatusCode = 409,
                    Message = "Some of these medicines does not exist",
                    Value = null
                };

            // Checking does the amount of medicine enought
            if (ourMed.Count < med.Count)
                return new Response<Order>
                {
                    StatusCode = 410,
                    Message = "Some of these medicine does not enough",
                    Value = null
                };
        }

        // Decreasing count of all medicines
        foreach (var med in order.Medicines)
        {
            var ourMed = (await medicineService.GetByIdAsync(med.Id)).Value;

            var mappedMed = new MedicineCreationDto
            {
                // Decreasing the count
                Count = ourMed.Count - med.Count,
                Description = ourMed.Description,
                Name = ourMed.Name,
                Price = ourMed.Price
            };
            await medicineService.UpdateAsync(ourMed.Id, mappedMed);
        }
    
        var paymentResult = await paymentService.CreateAsync(order.Payment);

        if (paymentResult.Value.IsPaid)
        {
            var mappedOrder = new Order()
            {
                Medicines = order.Medicines,
                Payment = paymentResult.Value,
                Status = StatusType.Pending
            };
            var orderResult = await genericRepository.CreateAsync(mappedOrder);

            return new Response<Order>()
            {
                StatusCode = 200,
                Message = "Success",
                Value = orderResult
            };
        }

        return new Response<Order>()
        {
            StatusCode = 402,
            Message = "Payment is not valid",
            Value = null
        };
    }

    public async Task<Response<List<Order>>> GetAllAsync()
    {
        var orders = await this.genericRepository.GetAllAsync();
        return new Response<List<Order>>()
        {
            StatusCode = 200,
            Message = "Success",
            Value = orders
        };
    }

    public async Task<Response<Order>> GetByIdAsync(long id)
    {
        var order = await this.genericRepository.GetByIdAsync(id);
        if(order is null)
            return new Response<Order>()
            {
                StatusCode = 404,
                Message = "Success",
                Value = null
            };

        return new Response<Order>()
        {
            StatusCode = 200,
            Message = "Success",
            Value = order
        };
    }

    public async Task<Response<Order>> UpdateAsync(long id, OrderCreationDto order)
    {
        var model = await this.genericRepository.GetByIdAsync(id);
        if (model is null)
            return new Response<Order>()
            {
                StatusCode = 404,
                Message = "Success",
                Value = null
            };
        var mappedPayment = new Payment()
        {
            OrderId = order.Payment.OrderId,
            Type = order.Payment.Type,
            IsPaid = order.Payment.IsPaid
        };
        var mappedOrder = new Order()
        {
            Medicines = order.Medicines,
            Payment = mappedPayment,
            Status = model.Status,
        };

        var orderResult = await this.genericRepository.UpdateAsync(id, mappedOrder);
        return new Response<Order>()
        {
            StatusCode = 200,
            Message = "Success",
            Value = orderResult
        };
    }

    public async Task<Response<Order>> ChangeStatus(long id, StatusType status)
    {
        var order = await genericRepository.GetByIdAsync(id);
        if(order is not null)
        {
            order.Status = status;
            var orderResult = await genericRepository.UpdateAsync(id, order);
            return new Response<Order>()
            {
                StatusCode = 200,
                Message = "Success",
                Value = orderResult
            };
        }

        return new Response<Order>()
        {
            StatusCode = 404,
            Message = "Order is not found",
            Value = null
        };
    }
}
