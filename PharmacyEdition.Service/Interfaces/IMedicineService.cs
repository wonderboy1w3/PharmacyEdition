using PharmacyEdition.Models;
using PharmacyEdition.Service.DTOs;
using PharmacyEdition.Service.Helpers;

namespace PharmacyEdition.Service.Interfaces
{
    public interface IMedicineService
    {
        Task<Response<Medicine>> CreateAsync(MedicineCreationDto medicine);
        Task<Response<Medicine>> UpdateAsync(long id, MedicineCreationDto medicine);
        Task<Response<bool>> DeleteAsync(long id);
        Task<Response<Medicine>> GetByIdAsync(long id);
        Task<Response<List<Medicine>>> GetAllAsync();
    }
}
