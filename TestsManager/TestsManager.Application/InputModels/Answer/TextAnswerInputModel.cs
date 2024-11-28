namespace TestsManager.Application.InputModels.Answer;

/// <summary>
/// Входная модель ответа пользователя на вопрос с вводом текста
/// </summary>
public class TextAnswerInputModel : AnswerInputModelBase
{
    /// <summary>
    /// Ответ пользователя в виде текста
    /// </summary>
    public string Answer { get; set; }
    
    /// <inheritdoc/>
    public override string Type => nameof(TextAnswerInputModel);
}
