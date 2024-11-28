namespace TestsManager.Application.ViewModels.Answer;

/// <summary>
/// Модель представления результата ответа на вопрос с выбором из вариантов
/// </summary>
public class ChoiceAnswerViewModel : AnswerViewModelBase
{
    /// <summary>
    /// Список порядковых номеров правильных ответов
    /// </summary>
    public List<byte>? RightChoicesIndexesList { get; set; }
    
    /// <inheritdoc/>
    public override string Type => nameof(ChoiceAnswerViewModel);
}
