using fileProcessor.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Net;
using System.Text;
using System.IO;
using Newtonsoft.Json;

namespace fileProcessor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        // url to request https://localhost:7041/api/file
        [HttpGet]
        public string Aaaa()
        {
            return "hola, soy la API de file";
        }

        // url to request https://localhost:7041/api/file/uploading
        [HttpPost]
        [Route("uploading")]
        public async Task<string> Uploadf([FromForm] UdFile obj)
        {
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
                    JObject json = JObject.Parse(result.ToString());
                } catch (JsonReaderException jex)
                {
                    return "Incorrect JSON format: " + jex.Message;
                } catch (Exception ex)
                {
                    return ex.Message;
                }

                return "File in JSON format successfully processed.";
            }
            return "File is null";
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
