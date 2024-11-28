namespace TestsManager.Common;

/// <summary>
/// Исключение выбрасывается при ошибке считывания данных из табличного файла
/// </summary>
public class TableParsException : Exception
{
    public TableParsException(string message, Exception innerException) : base(message, innerException)
    {
    }

    public TableParsException(string message) : base(message)
    {
    }
}
