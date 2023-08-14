using fileProcessor.models;
using fileProcessor.Models;

namespace fileProcessor
{
    public interface IFileRepository
    {
        Task<IEnumerable<Country>> GetAllCountries();
        Task<Country> GetCountry(int id);
        Task<bool> InsertCountry(Country country);
        Task<bool> UpdateCountry(Country country);
        Task<bool> DeleteCountry(Country country);
        
    }
}
