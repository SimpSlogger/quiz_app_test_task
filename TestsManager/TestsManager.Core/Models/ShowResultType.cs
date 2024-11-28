namespace TestsManager.Core.Models;

/// <summary>
/// Тип отображения результатов прохождения теста
/// </summary>
public enum ShowResultType
{
    /// <summary>
    /// Не показывать
    /// </summary>
    None,

    /// <summary>
    /// Сразу после ответа
    /// </summary>
    Immediately,

    /// <summary>
    /// После завершения теста
    /// </summary>
    AfterCompletion
}
