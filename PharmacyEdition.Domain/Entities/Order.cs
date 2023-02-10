using PharmacyEdition.Domain.Commons;
using PharmacyEdition.Domain.Enums;
using PharmacyEdition.Models;

namespace PharmacyEdition.Domain.Entities;

public class Order : Auditable
{
    public ICollection<Medicine> Medicines { get; set; }
    public StatusType Status { get; set; } = StatusType.Pending;
    public Payment Payment { get; set; }
}
