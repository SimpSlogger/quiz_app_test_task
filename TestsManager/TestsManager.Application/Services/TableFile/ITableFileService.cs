using TestsManager.Application.ViewModels;
using TestsManager.Core.Models;

namespace TestsManager.Application.Services.TableFile;

/// <summary>
/// Сервис считывания табличных файлов
/// </summary>
public interface ITableFileService
{
    /// <summary>
    /// Считать файл в формате xls и привести данные к модели <see cref="Test"/>
    /// </summary>
    /// <param name="file">Файл</param>
    /// <returns>Модель <see cref="Test"/> по считанному файлу</returns>
    public Task<Test> ParseTableFile(FileResult file);

    /// <summary>
    /// Преобразовать результаты прохождения теста в табличный файл и экспортировать его
    /// </summary>
    /// <param name="results">Модель представления результатов теста</param>
    /// <returns>Табличный файл с результатами тестирования</returns>
    public Task<FileResult> GetResultsTableFile(CompletedTestViewModel results);
}
