using TestsManager.Application.ViewModels.Answer;

namespace TestsManager.Application.ViewModels;

/// <summary>
/// Модель представления завершенного пользователем теста
/// </summary>
public class CompletedTestViewModel
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
    /// Количество заработанных очков за прохождение теста
    /// </summary>
    public double Points { get; set; }

    /// <summary>
    /// Максимальное количество возможных очков за прохождение теста
    /// </summary>
    public int PossiblePoints { get; set; }

    /// <summary>
    /// Процент либо кол-во очков, нужных для успешного прохождения теста
    /// <remarks>Если меньше 1, то это процент. Если больше 1, то это кол-во баллов</remarks> 
    /// </summary>
    public decimal? PassScore { get; set; }

    /// <summary>
    /// Список результатов ответов
    /// </summary>
    public List<AnswerViewModelBase> AnswersList { get; set; }
}
