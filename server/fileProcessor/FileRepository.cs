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

        public async Task<bool> InsertFile(File file)
        {
            var db = dbConnection();
            var sql = @"INSERT INTO fileprocessordb.file (name, timestamp) VALUES(@Name, @Timestamp);";
            var result = await db.ExecuteAsync(sql, new { file.Name, file.Timestamp });
            return result > 0; //is > 0 if it executes successfully
        }

        public async Task<bool> InsertCountry(Country country)
        {
            var db = dbConnection();
            var sql = @"INSERT INTO fileprocessordb.country (name, value, color, idfile) 
                        VALUES(
                            @Name,
                            @Value,
                            @Color,
                            (SELECT idFile From fileprocessordb.file ORDER BY idFile DESC LIMIT 1)
                        );"; // idFile belongs to the last record of File
            var result = await db.ExecuteAsync(sql, new { country.Name, country.Value, country.Color });
            return result > 0; //is > 0 if it executes successfully
        }

        public Task<bool> DeleteFile(int id)
        {
            throw new NotImplementedException();
        }
    }
}
