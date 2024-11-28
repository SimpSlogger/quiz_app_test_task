using OfficeOpenXml;
using TestsManager.Application.ViewModels;
using TestsManager.Common;
using TestsManager.Core.Adapters.Repositories;
using TestsManager.Core.Models;

namespace TestsManager.Application.Services.TableFile;

public class TableFileService : ITableFileService
{
    private const byte QuestionsLimit = 100;
    private const byte QuestionsXOffset = 1;
    private const byte QuestionsYOffset = 2;

    private const byte ChoicesOffset = 5;
    private const byte ChoicesLimit = 8;

    private readonly ITestRepository _testRepository;

    public TableFileService(ITestRepository testRepository)
    {
        _testRepository = testRepository;
    }

    // TODO Нужно переименовать на парсТабличныйФайл например, т.к. форматы не стабильны
    public async Task<Test> ParseTableFile(FileResult file)
    {
        var stream = new MemoryStream(file.Content);
        using var package = new ExcelPackage(stream);
        var questionsSheet = package.Workbook.Worksheets[0];
        // TODO Сделать проверку получилось ли достать этот лист. С предыдущим можно так-же по идее
        // var configSheet = package.Workbook.Worksheets[1];

        // var testConfiguration = ParseTestConfig(configSheet, out var description, out var authorName,
        //     out var tagsList, out var version);
        var questionsList = ParseQuestions(questionsSheet);

        ushort version;
        try
        {
             version = Convert.ToUInt16(questionsSheet.Cells[2, 1].Value?.ToString().Remove(0,1));
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new TableParsException("Не удалось распознать версию шаблона теста");
        }

        var test = new Test()
        {
            Id = Guid.NewGuid(),
            Title = file.FileName.Split('.')[0],
            // Description = description,
            // AuthorName = authorName,
            // TagsList = tagsList,
            // Config = testConfiguration,
            QuestionsList = questionsList,
            UpdatedAt = DateTimeOffset.Now,
            Password = "", // TODO Добавить позже
            Version = version
        };
        await _testRepository.AddAsync(test);
        return test;
    }

    public async Task<FileResult> GetResultsTableFile(CompletedTestViewModel results)
    {
        var package = new ExcelPackage();
        var sheet = package.Workbook.Worksheets.Add(results.TakerName);

        sheet.Cells[1,1].Value = "Имя пользователя"; 
        sheet.Cells[2,1].Value = "Время начала теста"; 
        sheet.Cells[3,1].Value = "Время завершения теста"; 
        sheet.Cells[5,1].Value = "Количество баллов";
        
        sheet.Cells[1,2].Value = results.TakerName; 
        sheet.Cells[2,2].Value = results.StartAt.ToLocalTime(); 
        sheet.Cells[3,2].Value = results.CompletedAt.ToLocalTime(); 
        sheet.Cells[5,2].Value = $"{results.Points} / {results.PossiblePoints}";

        sheet.Column(1).Width = 35;
        sheet.Column(2).Width = 35;
        
        return new FileResult()
        {
            ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
            Content = package.GetAsByteArray(),
            FileName = $"Результаты тестирования {results.TakerName}"
        };
    }

    private TestConfig ParseTestConfig(ExcelWorksheet sheet, out string description, out string authorName,
        out List<string> tagsList, out byte version)
    {
        description = "";
        authorName = "";
        tagsList = new List<string>();
        version = 0;

        var config = new TestConfig()
        {
            // TODO Сделать
        };

        return config;
    }

    private List<QuestionBase> ParseQuestions(ExcelWorksheet sheet)
    {
        var questionsList = new List<QuestionBase>();
        for (byte rowIndex = 1 + QuestionsYOffset; rowIndex < QuestionsLimit + QuestionsYOffset; rowIndex++)
        {
            var questionIndex = rowIndex - QuestionsYOffset;
            var questionTitleStr = sheet.Cells[rowIndex, 1 + QuestionsXOffset].Value?.ToString();
            if (string.IsNullOrEmpty(questionTitleStr)) break;
            var questionRightAnswerStr = sheet.Cells[rowIndex, 2 + QuestionsXOffset].Value?.ToString();
            var questionExplanationStr = sheet.Cells[rowIndex, 3 + QuestionsXOffset].Value?.ToString();
            var questionPointsStr = sheet.Cells[rowIndex, 4 + QuestionsXOffset].Value?.ToString() ?? "1";

            var choices = new List<Choice>();
            for (byte choicesIndex = 1 + ChoicesOffset; choicesIndex < ChoicesLimit + ChoicesOffset; choicesIndex++)
            {
                var choiceStr = sheet.Cells[rowIndex, choicesIndex].Value?.ToString();
                if (string.IsNullOrEmpty(choiceStr)) break;
                var choiceModel = new Choice()
                {
                    Index = Convert.ToByte(choicesIndex - ChoicesOffset),
                    Title = choiceStr
                };
                choices.Add(choiceModel);
            }

            if (choices.Count == 1)
            {
                // TODO Можно добавить особый эксепшен
                // TODO Можно собрать ошибки в кучу и выдать одним сообщением
                // TODO Не выводить название вопроса, только индекс ячейки
                // TODO Вернуть прежнее сообщение, когда появится возможность ввода ответа текстом
                // throw new Exception(
                //     $"Ошибка: Количество вариантов ответов должно быть либо больше 1, либо равно 0," +
                //     $" если это не вопрос с выбором вариантов. В вопросе: \"{questionTitleStr}\". В строке: {rowIndex}.");
                throw new TableParsException(
                    $"Ошибка: Количество вариантов ответов должно быть больше 0. В вопросе: \"{questionTitleStr}\". В строке: {rowIndex}.");
            }

            if (choices.Count == 0)
            {
                // TODO Вопрос с вводом текста
            }

            // Вопрос с одним или несколькими ответами
            if (string.IsNullOrEmpty(questionRightAnswerStr))
            {
                throw new TableParsException(
                    $"Ошибка: Для вопроса с выбором ответов должен быть указан хотя бы один правильный ответ " +
                    $"в виде соответствующей ему буквы. В вопросе: \"{questionTitleStr}\". В строке: {rowIndex}.");
            }

            var rightAnswersIndexes = StringToByteIndex(questionRightAnswerStr,
                Convert.ToByte(choices.Count), questionTitleStr, rowIndex);

            var questionModel = new ChoiceQuestion()
            {
                Title = questionTitleStr,
                Explanation = questionExplanationStr,
                Index = Convert.ToByte(questionIndex),
                Points = Convert.ToByte(questionPointsStr),
                IsMultiple = rightAnswersIndexes.Count > 1,
                SpreadPointsIfMultiple = false,
                ChoicesList = choices,
                CorrectAnswersIndexes = rightAnswersIndexes
            };
            questionsList.Add(questionModel);
        }

        return questionsList;
    }

    private List<byte> StringToByteIndex(string str, byte choicesCount, string questionTitle, byte rowIndex)
    {
        var splitStrArray = str.Split(',');
        var indexes = new List<byte>();
        foreach (var splitStr in splitStrArray)
        {
            var trimmed = splitStr.Replace(" ","");
            if (trimmed.Length > 1)
            {
                throw new TableParsException(
                    $"Ошибка: В качестве ответа нужно указать букву, соответствующую одному из указанных вариантов ответа, а не слово. В вопросе: \"{questionTitle}\". В строке {rowIndex}.");
            }

            var literal = trimmed.ToUpper().ToCharArray().First();
            int i = literal - 'А' + 1;
            if (i < 1 || i > 8)
            {
                throw new TableParsException(
                    $"Ошибка: Символ '{literal}' не может обозначать правильный вариант ответа. В вопросе: \"{questionTitle}\". В строке {rowIndex}.");
            }

            if (i > choicesCount)
            {
                throw new TableParsException(
                    $"Ошибка: Для символа '{literal}' нет соответствующего варианта ответа. В вопросе: \"{questionTitle}\". В строке {rowIndex}.");
            }

            indexes.Add(Convert.ToByte(i));
        }

        return indexes;
    }
}
