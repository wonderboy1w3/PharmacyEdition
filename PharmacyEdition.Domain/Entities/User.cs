using PharmacyEdition.Domain.Commons;

namespace PharmacyEdition.Domain.Entities;

public class User:Auditable
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int Age { get; set; }
    public string Phone { get; set; }
}
