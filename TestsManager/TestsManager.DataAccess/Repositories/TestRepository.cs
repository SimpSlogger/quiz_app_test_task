using Newtonsoft.Json;
using TestsManager.Core.Adapters.Repositories;
using TestsManager.Core.Models;
using TestsManager.DataAccess.Converters;

namespace TestsManager.DataAccess.Repositories;

public class TestRepository : ITestRepository
{
    private const string FolderName = "Data";
    private const string ListFileName = "TestsList";

    public async Task<Test> GetByIdAsync(Guid id)
    {
        if (!File.Exists($"{FolderName}/{id}"))
        {
            throw new Exception("Нет записи с таким идентификатором");
        }

        var str = await File.ReadAllTextAsync($"{FolderName}/{id}");
        var test = JsonConvert.DeserializeObject<Test>(str, new QuestionJsonConverter());
        if (test == null)
        {
            throw new Exception($"Не удалось считать запись {id}. Данные повреждены");
        }

        return test;
    }

    public async Task AddAsync(Test test)
    {
        // Создание отдельной записи
        var text = JsonConvert.SerializeObject(test);
        if (!Directory.Exists(FolderName)) Directory.CreateDirectory(FolderName);
        await File.WriteAllTextAsync($"{FolderName}/{test.Id}", text);

        // Добавление краткой записи в список
        var list = await GetShortInfoListAsync();
        var shortInfo = FullToShortInfo(test);
        list.Add(shortInfo);
        await UpdateShortInfoList(list);
    }

    public async Task UpdateAsync(Test test)
    {
        // Обновление отдельной записи
        await RemoveAsync(test.Id);
        await AddAsync(test);

        // Обновление краткой записи из списка
        var list = await GetShortInfoListAsync();
        var shortInfo = FullToShortInfo(test);
        var old = list.Find(i => i.TestId == test.Id);
        if (old != null)
            list.Remove(old);
        list.Add(shortInfo);
        await UpdateShortInfoList(list);
    }

    public async Task RemoveAsync(Guid id)
    {
        // Удаление отдельной записи
        if (!File.Exists($"{FolderName}/{id}"))
        {
            throw new Exception($"Нет записи {id}");
        }
        File.Delete($"{FolderName}/{id}");

        // Удаление краткой записи из списка
        var list = await GetShortInfoListAsync();
        var old = list.Find(i => i.TestId == id);
        if (old != null)
            list.Remove(old);
        await UpdateShortInfoList(list);
    }

    public async Task<List<TestShortInfo>> GetShortInfoListAsync()
    {
        var emptyList = new List<TestShortInfo>();
        if (!File.Exists($"{FolderName}/{ListFileName}")) return emptyList;

        var str = await File.ReadAllTextAsync($"{FolderName}/{ListFileName}");
        var deserialized = JsonConvert.DeserializeObject<List<TestShortInfo>>(str);
        return deserialized ?? emptyList;
    }

    /// <summary>
    /// Обновить список с краткой информацией
    /// </summary>
    /// <param name="list">Новый список</param>
    private async Task UpdateShortInfoList(List<TestShortInfo> list)
    {
        var text = JsonConvert.SerializeObject(list);

        if (!Directory.Exists(FolderName)) Directory.CreateDirectory(FolderName);
        if (!File.Exists($"{FolderName}/{ListFileName}")) File.Delete($"{FolderName}/{ListFileName}");

        await File.WriteAllTextAsync($"{FolderName}/{ListFileName}", text);
    }

    /// <summary>
    /// Сделать модель с краткой информацией на основе полной информации
    /// </summary>
    /// <param name="test">Полная модель</param>
    /// <returns>Краткая модель</returns>
    private TestShortInfo FullToShortInfo(Test test)
    {
        return new TestShortInfo()
        {
            TestId = test.Id,
            Title = test.Title,
            // TODO Добавить позже
            // Description = test.Description,
            // AuthorName = test.AuthorName,
            // MinuteLimit = test.Config.MinuteLimit,
            // TagsList = test.TagsList,
            // TryLimit = test.Config.TryLimit,
            UpdatedAt = test.UpdatedAt
        };
    }
}
