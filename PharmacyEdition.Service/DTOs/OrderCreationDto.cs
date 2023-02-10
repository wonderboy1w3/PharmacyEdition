using PharmacyEdition.Domain.Entities;
using PharmacyEdition.Domain.Enums;
using PharmacyEdition.Models;

namespace PharmacyEdition.Service.DTOs;

public class OrderCreationDto
{
    public ICollection<Medicine> Medicines { get; set; }
    public PaymentCreationDto Payment { get; set; } 
}
