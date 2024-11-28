using TestsManager.Application.ViewModels;

namespace TestsManager.Application.Services.Tests;

/// <summary>
/// Сервис получения моделей представления тестов
/// </summary>
public interface ITestsService
{
    /// <summary>
    /// Получить модель представления теста по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор теста</param>
    /// <returns>Модель представления теста</returns>
    public Task<TestViewModel> GetTestById(Guid id);

    /// <summary>
    /// Получить список моделей представления с краткой информацией о тестах
    /// </summary>
    /// <returns>Список моделей представления краткой информации о тестах</returns>
    public Task<List<TestShortInfoViewModel>> GetTestShortInfoList();
}
