using TestsManager.Application.InputModels;
using TestsManager.Application.InputModels.Answer;
using TestsManager.Application.ViewModels;
using TestsManager.Application.ViewModels.Answer;

namespace TestsManager.Application.Services.AnswersCheck;

/// <summary>
/// Сервис обработки результатов тестирования
/// </summary>
public interface IAnswersCheckService
{
    /// <summary>
    /// Проверить на правильность ответ. Нужно в случае, если ответы проверяются сразу по одному
    /// </summary>
    /// <param name="inputModel">Входная модель ответа</param>
    /// <returns>Модель представления результата ответа</returns>
    public Task<AnswerViewModelBase> CheckAnswer(AnswerInputModelBase inputModel);

    /// <summary>
    /// Проверить правильность прохождения теста. Используется когда ответы проверяются после завершения всего теста
    /// </summary>
    /// <param name="inputModel">Входная модель пройденного теста</param>
    /// <returns>Модель представления результатов прохождения тестирования</returns>
    public Task<CompletedTestViewModel> CheckCompletedTest(CompletedTestInputModel inputModel);
}
