using Microsoft.AspNetCore.Mvc;
using TestsManager.Application.Services.Tests;
using TestsManager.Application.ViewModels;
using TestsManager.Core.Models;

namespace TestsManager.WebApi.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class TestsController : Controller
{
    private readonly ITestsService _testsService;

    public TestsController(ITestsService testsService)
    {
        _testsService = testsService;
    }

    /// <summary>
    /// Получить список с краткой информацией о тестах
    /// </summary>
    /// <returns>Список с краткой информацией о тестах</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<TestShortInfo>))]
    public async Task<List<TestShortInfoViewModel>> GetTestShortInfoList() =>
        await _testsService.GetTestShortInfoList();

    /// <summary>
    /// Получить модель представления теста
    /// </summary>
    /// <param name="id">Идентификатор теста</param>
    /// <returns>Модель представления теста</returns>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TestViewModel))]
    public async Task<TestViewModel> GetTestById(Guid id) =>
        await _testsService.GetTestById(id);
}
