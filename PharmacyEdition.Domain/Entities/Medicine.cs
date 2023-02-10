using PharmacyEdition.Domain.Commons;

namespace PharmacyEdition.Models;

public class Medicine : Auditable
{
    public string Name { get; set; }
    public int Count { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
}
