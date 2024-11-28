namespace TestsManager.Core.Models;

/// <summary>
/// Модель теста
/// </summary>
public class Test
{
    /// <summary>
    /// Идентификатор теста
    /// </summary>
    public Guid Id { get; set; }
    
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
    /// Пароль для доступа к редактированию теста
    /// </summary>
    public string Password { get; set; }
    
    /// <summary>
    /// Последнее обновление
    /// </summary>
    public DateTimeOffset UpdatedAt { get; set; }
    
    /// <summary>
    /// Список ключевых слов (тегов)
    /// </summary>
    public List<string> TagsList { get; set; }
    
    /// <summary>
    /// Версия ПО, для которого актуальна запись об этом тесте 
    /// </summary>
    public ushort Version { get; set; }
    
    /// <summary>
    /// Настройки теста
    /// </summary>
    public TestConfig Config { get; set; }
    
    /// <summary>
    /// Список вопросов
    /// </summary>
    public List<QuestionBase> QuestionsList { get; set; }
}
