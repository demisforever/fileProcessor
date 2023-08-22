using fileProcessor.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Net;
using System.Text;
using System.IO;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;
using fileProcessor.models;

namespace fileProcessor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly IFileRepository _fileRepository;

        public FileController(IFileRepository fileRepository)
        {
            _fileRepository = fileRepository;
        }

        // url to request https://localhost:7041/api/file
        [HttpGet]
        public async Task<IActionResult> GetAllFiles()
        {
            return Ok(await _fileRepository.GetAllFiles());
        }

        // url to request https://localhost:7041/api/file/uploading
        [HttpPost]
        [Route("uploading")]
        public async Task<string> Uploadf([FromForm] UdFile obj)
        {
            JObject json;
            if (obj.Ffile != null)
            {
                var result = new StringBuilder();
                using (var reader = new StreamReader(obj.Ffile.OpenReadStream()))
                {
                    // read line to line the file content
                    while (reader.Peek() >= 0)
                        result.AppendLine(await reader.ReadLineAsync());
                }

                try
                {
                    // convert string to Json
                    json = JObject.Parse(result.ToString());
                } catch (JsonReaderException jex)
                {
                    return "Incorrect JSON format: " + jex.Message;
                } catch (Exception ex)
                {
                    return ex.Message;
                }

                // complete File object
                Models.File f = new Models.File();
                f.Timestamp = (System.DateTime)json["Timestamp"];
                f.Name = obj.Ffile.FileName;
                OkObjectResult fileInsertedOk = Ok(await _fileRepository.InsertFile(f));

                if (fileInsertedOk.Equals(false)) {
                    return "Problems at insert File ";
                }

                // complete Countrie object
                JToken countriesRows = json["Rows"];
                Country c = new Country();
                for (int i = 0; i < countriesRows.Count(); i++)
                {
                    c.Name = (string)countriesRows[i]["Name"];
                    c.Value = (int)countriesRows[i]["Value"];
                    c.Color = (string)countriesRows[i]["Color"];
                    Ok(await _fileRepository.InsertCountry(c));
                }

                return "File in JSON format successfully processed";
            }
            return "File is null";
        }
        
        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetFile(int id)
        {
            return Ok(await _fileRepository.GetFile(id));
        }
        
        //[HttpPost]
        //public void SaveNewValue([FromBody] string value)
        //{
        //}

        //[HttpPut]
        //public void UpdateValue(int id, [FromBody] string value)
        //{
        //}

        [HttpDelete]
        public async Task<IActionResult> DeleteFile(int id)
        {
            return Ok(await _fileRepository.DeleteFile(id));
        }

    }
}
