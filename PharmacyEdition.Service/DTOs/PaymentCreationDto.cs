using PharmacyEdition.Domain.Entities;
using PharmacyEdition.Domain.Enums;

namespace PharmacyEdition.Service.DTOs;

public class PaymentCreationDto
{
    public PaymentType Type { get; set; }
    public long OrderId { get; set; }
    public bool IsPaid { get; set; }
}
