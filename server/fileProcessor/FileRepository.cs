using fileProcessor.models;
using fileProcessor.Models;

namespace fileProcessor
{
    public class FileRepository : IFileRepository
    {
        public Task<bool> DeleteCountry(Country country)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Country>> GetAllCountries()
        {
            throw new NotImplementedException();
        }

        public Task<Country> GetCountry(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> InsertCountry(Country country)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateCountry(Country country)
        {
            throw new NotImplementedException();
        }
    }
}
