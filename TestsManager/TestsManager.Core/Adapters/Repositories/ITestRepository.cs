using TestsManager.Core.Models;

namespace TestsManager.Core.Adapters.Repositories;

/// <summary>
/// Репозиторий тестов
/// </summary>
public interface ITestRepository
{
    /// <summary>
    /// Получить модель теста по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор теста</param>
    /// <returns>Модель теста</returns>
    public Task<Test> GetByIdAsync(Guid id);

    /// <summary>
    /// Получить список с краткой информацией о тестах
    /// </summary>
    /// <returns>Список с краткой информацией о тестах</returns>
    public Task<List<TestShortInfo>> GetShortInfoListAsync();

    /// <summary>
    /// Записать модель теста
    /// </summary>
    /// <param name="test">Модель теста для записи</param>
    public Task AddAsync(Test test);

    /// <summary>
    /// Обновить запись о тесте
    /// </summary>
    /// <param name="test">Новая запись теста</param>
    public Task UpdateAsync(Test test);

    /// <summary>
    /// Удалить запись о тесте
    /// </summary>
    /// <param name="id">Идентификатор записи теста</param>
    public Task RemoveAsync(Guid id);
}
