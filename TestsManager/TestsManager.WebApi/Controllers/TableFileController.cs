using Microsoft.AspNetCore.Mvc;
using TestsManager.Application.Services.TableFile;
using TestsManager.Application.ViewModels;
using TestsManager.Core.Models;
using FileResult = TestsManager.Application.ViewModels.FileResult;

namespace TestsManager.WebApi.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class TableFileController : Controller
{
    private readonly ITableFileService _tableFileService;

    public TableFileController(ITableFileService tableFileService)
    {
        _tableFileService = tableFileService;
    }

    [HttpPost("import/tableFile")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
    public async Task<Test> ImportTableFile(IFormFile file)
    {
        byte[] content;
        using (var stream = file.OpenReadStream())
        {
            content = new byte[stream.Length];
            await stream.ReadAsync(content, 0, content.Length);
        }
        
        var fileInfo = new FileResult()
        {
            FileName = file.FileName,
            Content = content,
            ContentType = file.ContentType
        };
        return await _tableFileService.ParseTableFile(fileInfo);
    }
        
    [HttpGet("export/result")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(FileContentResult))]
    public async Task<IActionResult> GetTestResultTableFile([FromQuery] CompletedTestViewModel results)
    {
        var wb = await _tableFileService.GetResultsTableFile(results);

        return new FileContentResult(wb.Content, wb.ContentType)
        {
            FileDownloadName = wb.FileName
        };
    }
}
