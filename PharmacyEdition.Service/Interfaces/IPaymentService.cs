using PharmacyEdition.Domain.Entities;
using PharmacyEdition.Service.DTOs;
using PharmacyEdition.Service.Helpers;

namespace PharmacyEdition.Service.Interfaces;

public interface IPaymentService
{
    Task<Response<Payment>> CreateAsync(PaymentCreationDto medicine);
    Task<Response<Payment>> UpdateAsync(long id, PaymentCreationDto medicine);
    Task<Response<bool>> DeleteAsync(long id);
    Task<Response<Payment>> GetByIdAsync(long id);
    Task<Response<List<Payment>>> GetAllAsync();
}
