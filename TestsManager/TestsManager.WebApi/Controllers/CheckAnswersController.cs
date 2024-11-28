using Microsoft.AspNetCore.Mvc;
using TestsManager.Application.InputModels;
using TestsManager.Application.InputModels.Answer;
using TestsManager.Application.Services.AnswersCheck;
using TestsManager.Application.ViewModels;
using TestsManager.Application.ViewModels.Answer;

namespace TestsManager.WebApi.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class CheckAnswersController : Controller
{
    private readonly IAnswersCheckService _answersCheckService;

    public CheckAnswersController(IAnswersCheckService answersCheckService)
    {
        _answersCheckService = answersCheckService;
    }

    // TODO Добавить позже
    // [HttpGet("check/answer")]
    // [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AnswerViewModelBase))]
    // public async Task<AnswerViewModelBase> CheckAnswer(AnswerInputModelBase inputModel) =>
    //     await _answersCheckService.CheckAnswer(inputModel);

    [HttpPost("check/test")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CompletedTestViewModel))]
    public async Task<CompletedTestViewModel> CheckCompletedTest(CompletedTestInputModel inputModel) =>
        await _answersCheckService.CheckCompletedTest(inputModel);
}
