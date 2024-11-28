namespace TestsManager.Application.InputModels.Answer;

/// <summary>
/// Базовая входная модель ответа пользователя
/// </summary>
public class AnswerInputModelBase
{
    /// <summary>
    /// Порядковый номер вопроса в тесте
    /// </summary>
    public byte QuestionIndex { get; set; }
    
    /// <summary>
    /// Тип ответа
    /// </summary>
    public virtual string Type => nameof(AnswerInputModelBase);
}
