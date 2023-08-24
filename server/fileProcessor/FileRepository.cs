using fileProcessor.models;
using fileProcessor.Models;
using MySql.Data.MySqlClient;
using Dapper;
using System.IO;
using File = fileProcessor.Models.File;
using System.Collections.Generic;
using MySqlX.XDevAPI.Common;

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

        public async Task<IEnumerable<File>> GetFile(int id)
        {
            var db = dbConnection();
            // query: ger a File with idfile
            var sql = @"SELECT * FROM fileprocessordb.file AS f WHERE f.idfile = @id;";
            IEnumerable<File> fileList = await db.QueryAsync<File>(sql, new { Id = id });

            // query all countries with an specific idfile
            sql = @"SELECT idcountry, name, value, color, idfile FROM fileprocessordb.country WHERE idfile = @id;";
            IEnumerable<Country> countriesList = await db.QueryAsync<Country>(sql, new { Id = id });
            fileList.First().Countries = (ICollection<Country>)countriesList;

            return fileList;
        }

        // return int idFile value from inserted file 
        public int InsertFile(File file)
        {
            var db = dbConnection();
            var sql = @"INSERT INTO fileprocessordb.file (name, timestamp) VALUES(@Name, @Timestamp); SELECT LAST_INSERT_ID();";
            var result = db.QuerySingle<int>(sql, new { file.Name, file.Timestamp });

            return result; //is > 0 if it executes successfully
        }

        public async Task<bool> InsertCountry(Country country)
        {
            var db = dbConnection();
            var sql = @"INSERT INTO fileprocessordb.country (name, value, color, idfile) 
                        VALUES(
                            @Name,
                            @Value,
                            @Color,
                            @IdFile
                        );";
            var result = await db.ExecuteAsync(sql, new { country.Name, country.Value, country.Color, country.Idfile });
            return result > 0; //is > 0 if it executes successfully
        }

        public async Task<bool> DeleteFile(int id)
        {
            var db = dbConnection();
            var sql = @"DELETE FROM fileprocessordb.file WHERE (idfile = @Id);";
            var result = await db.ExecuteAsync(sql, new { Id = id });
            return result > 0; //is > 0 if it executes successfully
        }
        public async Task<bool> DeleteCountry(int id)
        {
            var db = dbConnection();
            var sql = @"DELETE FROM fileprocessordb.country WHERE (idfile = @Id);";
            var result = await db.ExecuteAsync(sql, new { Id = id });
            return result > 0; //is > 0 if it executes successfully
        }

    }
}
