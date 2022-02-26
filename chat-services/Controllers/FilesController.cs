using System.Collections.Generic;
using System.IO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using System;
using Microsoft.AspNetCore.Http;
using chat_service.Contracts;
using chat_service.Db;
using chat_service.Services;
using chat_service.Common;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace chat_service.Controllers
{
    [ApiController]
    [Route("files")]
    public class FilesController : ControllerBase
    {
        private readonly IFileService _fileService;

        public FilesController(IFileService fileService)
        {
            _fileService = fileService;
        }

        // GET: api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetFile([FromRoute] Guid id)
        {
            var file = await _fileService.StreamFileAsync(id);

            return File(file.Content, file.ContentType, file.FileName);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromForm] IFormFile file)
        {
            if(file == null)
            {
                return BadRequest("Please select file.");
            }
            var fileId = await _fileService.StoreFileAsync(file);
            var response = new ResponseMessage
            {
                Data = fileId
            };

            return Ok(response);
        }
    }
}
