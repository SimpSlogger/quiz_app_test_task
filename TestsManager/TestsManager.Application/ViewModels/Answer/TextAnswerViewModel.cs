namespace TestsManager.Application.ViewModels.Answer;

/// <summary>
/// Модель представления результата ответа на вопрос с вводом текста
/// </summary>
public class TextAnswerViewModel : AnswerViewModelBase
{
    /// <summary>
    /// Текст правильного ответа
    /// </summary>
    public string? RightAnswer { get; set; }
    
    /// <inheritdoc/>
    public override string Type => nameof(TextAnswerViewModel);
}
