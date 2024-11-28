using TestsManager.Application.InputModels.Answer;

namespace TestsManager.Application.InputModels;

/// <summary>
/// Входная модель завершенного пользователем теста
/// </summary>
public class CompletedTestInputModel
{
    /// <summary>
    /// Идентификатор завершенного теста
    /// </summary>
    public Guid TestId { get; set; }
    
    /// <summary>
    /// Имя пользователя, который проходил тест
    /// </summary>
    public string TakerName { get; set; }

    /// <summary>
    /// Время начала прохождения теста
    /// </summary>
    public DateTimeOffset StartAt { get; set; }
    
    /// <summary>
    /// Время завершения прохождения теста
    /// </summary>
    public DateTimeOffset CompletedAt { get; set; }
    
    /// <summary>
    /// Список ответов на вопросы
    /// </summary>
    public List<AnswerInputModelBase> AnswersList { get; set; }
}
