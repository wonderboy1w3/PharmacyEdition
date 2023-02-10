using PharmacyEdition.Domain.Commons;
using PharmacyEdition.Domain.Enums;

namespace PharmacyEdition.Domain.Entities;

public class Payment : Auditable
{
    public PaymentType Type { get; set; }
    public bool IsPaid { get; set; }
    public long OrderId { get; set; }
}
