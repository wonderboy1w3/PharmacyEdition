using PharmacyEdition.Domain.Entities;
using PharmacyEdition.Domain.Enums;
using PharmacyEdition.Service.DTOs;
using PharmacyEdition.Service.Helpers;

namespace PharmacyEdition.Service.Interfaces;

public interface IOrderService
{
    Task<Response<Order>> CreateAsync(OrderCreationDto medicine);
    Task<Response<Order>> UpdateAsync(long id, OrderCreationDto medicine);
    Task<Response<Order>> GetByIdAsync(long id);
    Task<Response<List<Order>>> GetAllAsync();
    Task<Response<Order>> ChangeStatus(long id, StatusType status);
}
