namespace TestsManager.Core.Models;

/// <summary>
/// Сокращенная информация о тесте для хранения в списке
/// </summary>
public class TestShortInfo
{
    /// <summary>
    /// Идентификатор теста
    /// </summary>
    public Guid TestId { get; set; }
    
    /// <summary>
    /// Заголовок теста (название)
    /// </summary>
    public string Title { get; set; }
    
    /// <summary>
    /// Описание теста
    /// </summary>
    public string? Description { get; set; }
    
    /// <summary>
    /// Имя автора
    /// </summary>
    public string AuthorName { get; set; }

    /// <summary>
    /// Последнее обновление
    /// </summary>
    public DateTimeOffset UpdatedAt { get; set; }
    
    /// <summary>
    /// Список ключевых слов (тегов)
    /// </summary>
    public List<string> TagsList { get; set; }

    /// <summary>
    /// Ограничение по времени в минутах
    /// </summary>
    public ushort? MinuteLimit { get; set; }
    
    /// <summary>
    /// Количество разрешенных попыток для пользователя
    /// </summary>
    public byte? TryLimit { get; set; }
}
