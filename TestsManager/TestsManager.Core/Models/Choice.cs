namespace TestsManager.Core.Models;

/// <summary>
/// Один из вариантов ответа
/// </summary>
public class Choice
{
    /// <summary>
    /// Порядковый номер варианта ответа
    /// </summary>
    public byte Index { get; set; }
    
    /// <summary>
    /// Заголовок варианта ответа
    /// </summary>
    public string Title { get; set; }
    
    /// <summary>
    /// Изображение
    /// </summary>
    public byte[]? Image { get; set; }
}
