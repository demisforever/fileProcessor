using fileProcessor.models;
using fileProcessor.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Net;
using System.Text;

namespace fileProcessor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        //private IFileRepository _countryRepository;         
        //private static IWebHostEnvironment _webHostEnvironment;

        //public void CountryController(IFileRepository countryRepository)
        //{
            //this._countryRepository = countryRepository;
            //_webHostEnvironment = webHostEnvironment;
        //}

        // https://localhost:7041/api/file
        [HttpGet]
        public string Aaaa()
        {
            return "hola";
        }

        // https://localhost:7041/api/file/uploading
        [HttpPost]
        [Route("uploading")]
        public async Task<string> Uploadf([FromForm] UdFile obj)
        {
            if (obj.ffile.Length > 0)
            {
                var result = new StringBuilder();
                using (var reader = new StreamReader(obj.ffile.OpenReadStream()))
                {
                    while (reader.Peek() >= 0)
                        result.AppendLine(await reader.ReadLineAsync());
                }
                //convert string to Json
                JObject json = JObject.Parse(result.ToString());
                return result.ToString();
            }
            return "File noooo estaa!";
        }

        //[HttpPost]
        //public void SaveNewValue([FromBody] string value)
        //{
        //}

        //[HttpPut]
        //public void UpdateValue(int id, [FromBody] string value)
        //{
        //}

        //[HttpDelete]
        //public void RemoveValue(int id)
        //{
        //}

    }
}
