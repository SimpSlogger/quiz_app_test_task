namespace TestsManager.Core.Models;

/// <summary>
/// Базовая модель вопроса
/// </summary>
public abstract class QuestionBase
{
    /// <summary>
    /// Порядковый номер вопроса в тесте
    /// </summary>
    public byte Index { get; set; }

    /// <summary>
    /// Заголовок вопроса (сам вопрос)
    /// </summary>
    public string Title { get; set; }

    /// <summary>
    /// Изображение, дополняющее вопрос
    /// </summary>
    public byte[]? Image { get; set; }

    /// <summary>
    /// Количество очков за правильный ответ
    /// </summary>
    public byte Points { get; set; }

    /// <summary>
    /// Объяснение ответа
    /// </summary>
    public string? Explanation { get; set; }

    /// <summary>
    /// Тип вопроса
    /// </summary>
    public virtual string QuestionType => nameof(QuestionBase);
}
