namespace TestsManager.Application.InputModels.Answer;

/// <summary>
/// Входная модель ответа пользователя на вопрос с выбором из вариантов
/// </summary>
public class ChoiceAnswerInputModel : AnswerInputModelBase
{
    /// <summary>
    /// Список порядковых номеров ответов, выбранных пользователем
    /// </summary>
    public List<byte> ChoiceIndexesList { get; set; }

    /// <inheritdoc/>
    public override string Type => nameof(ChoiceAnswerInputModel);
}
