using TestsManager.Application.ViewModels.Questions;

namespace TestsManager.Application.ViewModels;

public class TestViewModel
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
    /// Последнее обновление
    /// </summary>
    public DateTimeOffset UpdatedAt { get; set; }
    
    /// <summary>
    /// Список ключевых слов (тегов)
    /// </summary>
    public List<string> TagsList { get; set; }
    
    /// <summary>
    /// Список вопросов
    /// </summary>
    public List<QuestionViewModelBase> QuestionsList { get; set; }
}
