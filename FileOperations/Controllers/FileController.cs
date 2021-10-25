using FileOperations.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FileOperations.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FileController : ControllerBase
    {
        private ApplicationDbContext _db;

        private readonly ILogger<FileController> _logger;

        public FileController(ILogger<FileController> logger, ApplicationDbContext db)
        {
            _logger = logger;
            _db = db;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_db.Files);
        }
        [HttpPost]
        [RequestFormLimits(ValueLengthLimit = int.MaxValue, MultipartBodyLengthLimit = int.MaxValue)]
        public async Task<IActionResult> Post()
        {
            var files = Request.Form.Files;
            foreach (var file in files)
            {
                if (file.Length > 0)
                {
                    using (var ms = new MemoryStream())
                    {
                        file.CopyTo(ms);
                        var fileBytes = ms.ToArray();
                        var record = new Model.File()
                        {
                            Content = fileBytes,
                            CreationDate = DateTime.Now,
                            FileName = Path.GetFileName(file.FileName),
                            FileExtension = Path.GetExtension(file.FileName)
                        };
                        await _db.Files.AddAsync(record);
                        await _db.SaveChangesAsync();
                    }
                }
            }

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var rec = _db.Files.Find(id);
            if (rec != null)
            {
                _db.Files.Remove(rec);
                _db.SaveChanges();
            }

            return Ok();
        }
    }
}
