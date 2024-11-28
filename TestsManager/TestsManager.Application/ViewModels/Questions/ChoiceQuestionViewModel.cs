using TestsManager.Core.Models;

namespace TestsManager.Application.ViewModels.Questions;

/// <summary>
/// Модель представления вопроса с несколькими правильными ответами из списка вариантов
/// </summary>
public class ChoiceQuestionViewModel : QuestionViewModelBase
{
    /// <summary>
    /// Список вариантов ответа
    /// </summary>
    public List<Choice> ChoicesList { get; set; }

    /// <summary>
    /// Можно ли выбрать несколько вариантов ответа
    /// </summary>
    public bool IsMultiple { get; set; }

    /// <summary>
    /// Если правильных ответов несколько, разделить общее кол-во очков на кол-во правильных ответов.
    /// В противном случае - очки будут засчитаны, только если выбраны все правильные ответы
    /// </summary>
    public bool SpreadPointsIfMultiple { get; set; }
    
    /// <inheritdoc/>
    public override string QuestionType => nameof(ChoiceQuestionViewModel);
}
