using TestsManager.Application.ViewModels;
using TestsManager.Application.ViewModels.Questions;
using TestsManager.Core.Adapters.Repositories;
using TestsManager.Core.Models;

namespace TestsManager.Application.Services.Tests;

public class TestsService : ITestsService
{
    private readonly ITestRepository _testRepository;

    public TestsService(ITestRepository testRepository)
    {
        _testRepository = testRepository;
    }

    public async Task<TestViewModel> GetTestById(Guid id)
    {
        var test = await _testRepository.GetByIdAsync(id);

        var questionViewModelsList = new List<QuestionViewModelBase>();
        foreach (var question in test.QuestionsList)
        {
            if (question.QuestionType == nameof(ChoiceQuestion))
            {
                var choiceQuestion = question as ChoiceQuestion;
                if (choiceQuestion == null)
                    throw new Exception($"Не удалось преобразовать модель вопроса к правильному типу");
                var questionViewModel = new ChoiceQuestionViewModel()
                {
                    Index = choiceQuestion.Index,
                    Title = choiceQuestion.Title,
                    Image = choiceQuestion.Image,
                    Points = choiceQuestion.Points,
                    ChoicesList = choiceQuestion.ChoicesList,
                    IsMultiple = choiceQuestion.IsMultiple,
                    SpreadPointsIfMultiple = choiceQuestion.SpreadPointsIfMultiple
                };
                questionViewModelsList.Add(questionViewModel);
            }
        }

        return new TestViewModel()
        {
            Id = test.Id,
            Title = test.Title,
            Description = test.Description,
            AuthorName = test.AuthorName,
            TagsList = test.TagsList,
            UpdatedAt = test.UpdatedAt,
            QuestionsList = questionViewModelsList
        };
    }

    public async Task<List<TestShortInfoViewModel>> GetTestShortInfoList()
    {
        // TODO Позднее добавить пагинацию
        var testsList = await _testRepository.GetShortInfoListAsync();
        var testsViewModelsList = new List<TestShortInfoViewModel>();
        foreach (var testShortInfo in testsList)
        {
            var testShortInfoViewModel = new TestShortInfoViewModel()
            {
                TestId = testShortInfo.TestId,
                Title = testShortInfo.Title,
                Description = testShortInfo.Description,
                AuthorName = testShortInfo.AuthorName,
                TagsList = testShortInfo.TagsList,
                UpdatedAt = testShortInfo.UpdatedAt,
                MinuteLimit = testShortInfo.MinuteLimit,
                TryLimit = testShortInfo.TryLimit
            };
            testsViewModelsList.Add(testShortInfoViewModel);
        }

        return testsViewModelsList;
    }
}
