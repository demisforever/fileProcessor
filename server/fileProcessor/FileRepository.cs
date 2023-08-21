using fileProcessor.models;
using fileProcessor.Models;
using MySql.Data.MySqlClient;
using Dapper;
using System.IO;
using File = fileProcessor.Models.File;
using System.Collections.Generic;

namespace fileProcessor
{
    public class FileRepository : IFileRepository
    {
        private readonly MySQLConfiguration _connectionString;

        public FileRepository(MySQLConfiguration connectionString)
        {
            _connectionString = connectionString;
        }

        //open MySQL connection
        protected MySqlConnection dbConnection()
        {
            return new MySqlConnection(_connectionString.ConnectionString);
        }

        public async Task<IEnumerable<File>> GetAllFiles()
        {
            var db = dbConnection();
            var sql = @"SELECT idfile, timestamp, name FROM fileprocessordb.file;";
            return await db.QueryAsync<File>(sql, new { });
        }

        public async Task<IEnumerable<Country>> GetAllCountriesByFileId(int id)
        {
            var db = dbConnection();
            var sql = @"SELECT idcountry, name, value, color, idfile FROM fileprocessordb.country WHERE idfile = @id;";
            return await db.QueryAsync<Country>(sql, new { Id = id });
        }

        // https://stackoverflow.com/questions/70385244/how-to-add-create-multiple-one-to-many-relationships-in-same-view-in-asp-net-cor
        public async Task<IEnumerable<File>> GetFile(int id)
        {
            var db = dbConnection();
            // query: ger a File with idfile
            var sql = @"SELECT * FROM fileprocessordb.file AS f WHERE f.idfile = @id;";
            IEnumerable<File> fileList = await db.QueryAsync<File>(sql, new { Id = id });
            fileList.First().Countries = new List<Country>();

            // query all countries with an specific idfile
            sql = @"SELECT idcountry, name, value, color, idfile FROM fileprocessordb.country WHERE idfile = @id;";
            IEnumerable<Country> countriesList = await db.QueryAsync<Country>(sql, new { Id = id });
            fileList.First().Countries = (ICollection<Country>)countriesList;

            return fileList;
        }

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
