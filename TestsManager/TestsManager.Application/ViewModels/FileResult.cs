namespace TestsManager.Application.ViewModels;

/// <summary>
/// Данные о файле для отправки на клиент
/// </summary>
public class FileResult
{
    /// <summary>
    /// Тип данных, расширение файла
    /// </summary>
    public string ContentType { get; set; }
    
    /// <summary>
    /// Наполнение файла в виде массива байт
    /// </summary>
    public byte[] Content { get; set; }
    
    /// <summary>
    /// Название файла
    /// </summary>
    public string FileName { get; set; }
}
