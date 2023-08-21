using fileProcessor.models;
using fileProcessor.Models;
using File = fileProcessor.Models.File;

namespace fileProcessor
{
    public interface IFileRepository
    {
        Task<IEnumerable<File>> GetAllFiles();
        Task<IEnumerable<File>> GetFile(int id);
        Task<IEnumerable<Country>> GetAllCountriesByFileId(int id);
        Task<Country> GetCountry(int id);
        Task<bool> InsertCountry(Country country);
        Task<bool> UpdateCountry(Country country);
        Task<bool> DeleteCountry(Country country);
        
    }
}
