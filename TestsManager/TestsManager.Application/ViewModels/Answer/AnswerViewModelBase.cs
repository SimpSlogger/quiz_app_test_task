namespace TestsManager.Application.ViewModels.Answer;

/// <summary>
/// Базовая модель представления результата ответа
/// </summary>
public class AnswerViewModelBase
{
    /// <summary>
    /// Порядковый номер вопроса в тесте
    /// </summary>
    public byte QuestionIndex { get; set; }

    /// <summary>
    /// Количество очков, полученных за ответ на данный вопрос
    /// </summary>
    public double Points { get; set; }

    /// <summary>
    /// Максимальное количество возможных очков за ответ на данный вопрос
    /// </summary>
    public byte PossiblePoints { get; set; }
    
    /// <summary>
    /// Объяснение правильного ответа от автора теста
    /// </summary>
    public string? Explanation { get; set; }
    
    /// <summary>
    /// Тип результата ответа
    /// </summary>
    public virtual string Type => nameof(AnswerViewModelBase);
}
