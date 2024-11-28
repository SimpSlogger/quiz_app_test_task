namespace TestsManager.Core.Models;

/// <summary>
/// Настройки теста
/// </summary>
public class TestConfig
{
    /// <summary>
    /// Вступление. Отображается перед началом тестирования
    /// </summary>
    public string? Introduction { get; set; }
    
    /// <summary>
    /// Заключение. Отображается после прохождения тестирования
    /// </summary>
    public string? Conclusion { get; set; }
    
    /// <summary>
    /// Процент либо кол-во очков, нужных для успешного прохождения теста
    /// <remarks>Если меньше 1, то это процент. Если больше 1, то это кол-во баллов</remarks> 
    /// </summary>
    public decimal? PassScore { get; set; }
    
    /// <summary>
    /// Сообщение после успешного прохождения теста
    /// </summary>
    public string? PassMessage { get; set; }
    
    /// <summary>
    /// Сообщение после неудачного прохождения теста
    /// </summary>
    public string? FailMessage { get; set; }
    
    /// <summary>
    /// Показывать по одному вопросу за раз
    /// </summary>
    public bool OnePerPage { get; set; }
    
    /// <summary>
    /// Выводить вопросы в случайном порядке
    /// </summary>
    public bool RandomizeQuestions { get; set; }
    
    /// <summary>
    /// Позволить не отвечать на вопросы
    /// </summary>
    public bool AllowEmpty { get; set; }
    
    /// <summary>
    /// Показывать какие ответы неверны
    /// </summary>
    public ShowResultType NegativeMarking { get; set; }
    
    /// <summary>
    /// Показывать правильный ответ
    /// </summary>
    public ShowResultType PositiveMarking { get; set; }
    
    /// <summary>
    /// Показывать объяснение правильного ответа
    /// </summary>
    public ShowResultType ShowExplanation { get; set; }
    
    /// <summary>
    /// Ограничение по времени в минутах
    /// </summary>
    public ushort? MinuteLimit { get; set; }
    
    /// <summary>
    /// Адрес электронной почты для отправки результата тестирования
    /// </summary>
    public string? SendResultTo { get; set; }
    
    /// <summary>
    /// Код доступа к прохождению теста
    /// </summary>
    public string? AccessCode { get; set; }
    
    /// <summary>
    /// Подпись поля для ввода/выбора идентификатора пользователя
    /// <example>"Введите ФИО", "Выберите адрес электронной почты"</example>
    /// </summary>
    public string? TakerNameInputLabel { get; set; }
    
    /// <summary>
    /// Список идентификаторов (имен, электронных адресов и т.д.) пользователей
    /// </summary>
    public List<string>? TakerNamesList { get; set; }
    
    /// <summary>
    /// Количество разрешенных попыток для пользователя
    /// </summary>
    public byte? TryLimit { get; set; }
}
