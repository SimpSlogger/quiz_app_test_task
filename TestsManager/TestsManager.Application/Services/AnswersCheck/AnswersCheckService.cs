using TestsManager.Application.InputModels;
using TestsManager.Application.InputModels.Answer;
using TestsManager.Application.ViewModels;
using TestsManager.Application.ViewModels.Answer;
using TestsManager.Core.Adapters.Repositories;
using TestsManager.Core.Models;

namespace TestsManager.Application.Services.AnswersCheck;

public class AnswersCheckService : IAnswersCheckService
{
    private readonly ITestRepository _testRepository;

    public AnswersCheckService(ITestRepository testRepository)
    {
        _testRepository = testRepository;
    }

    public async Task<AnswerViewModelBase> CheckAnswer(AnswerInputModelBase inputModel)
    {
        throw new NotImplementedException();
    }

    public async Task<CompletedTestViewModel> CheckCompletedTest(CompletedTestInputModel inputModel)
    {
        var test = await _testRepository.GetByIdAsync(inputModel.TestId);
        if (inputModel.AnswersList.Count != test.QuestionsList.Count && !test.Config.AllowEmpty)
        {
            throw new Exception($"Даны ответы не на все вопросы. " +
                                $"Кол-во вопросов: {test.QuestionsList.Count}. " +
                                $"Кол-во ответов пользователя: {inputModel.AnswersList.Count}. " +
                                $"Позволить пользователю отвечать не на все вопросы можно в настройках теста.");
        }

        double testPoints = 0;
        var answerViewModels = new List<AnswerViewModelBase>();

        foreach (var answer in inputModel.AnswersList)
        {
            double questionPoints = 0;

            var question = test.QuestionsList.Find(q => q.Index == answer.QuestionIndex);
            if (question == null)
            {
                throw new Exception($"Не найден вопрос с порядковым номером {answer.QuestionIndex}. " +
                                    $"Всего вопросов в тесте: {test.QuestionsList.Count}");
            }

            // Обработка вопросов и ответов с типом "Выбор ответа из вариантов"

            #region ChoiceQuestion

            if (answer.Type == nameof(ChoiceAnswerInputModel))
            {
                var choiceAnswer = answer as ChoiceAnswerInputModel;
                if (choiceAnswer == null)
                {
                    throw new Exception($"Не удалось привести ответ к типу {nameof(ChoiceAnswerInputModel)}");
                }

                var choiceQuestion = question as ChoiceQuestion;
                if (choiceQuestion == null)
                {
                    throw new Exception($"Не удалось привести вопрос к типу {nameof(ChoiceQuestion)}. " +
                                        $"Возможно тип ответа {answer.Type} не соответствует типу вопроса {question.QuestionType}");
                }

                // Делим кол-во возможных очков на все ответы пользователя, а не только верные,
                // чтобы получить цену одного правильного ответа. Тогда будет менее выгодно отмечать все ответы подряд 
                double pointsForOne = Convert.ToDouble(choiceQuestion.Points) / Convert.ToDouble(choiceAnswer.ChoiceIndexesList.Count);
                foreach (var choiceIndex in choiceAnswer.ChoiceIndexesList)
                {
                    if (choiceQuestion.CorrectAnswersIndexes.Contains(choiceIndex))
                    {
                        questionPoints += pointsForOne;
                    }
                }

                // Если не требуется распределить баллы по нескольким правильным ответам,
                // то в случае хотя бы одной ошибки обнулять баллы
                if (!choiceQuestion.SpreadPointsIfMultiple && questionPoints != choiceQuestion.Points)
                {
                    questionPoints = 0;
                }

                testPoints += questionPoints;
                var choiceAnswerViewModel = new ChoiceAnswerViewModel()
                {
                    QuestionIndex = question.Index,
                    Points = questionPoints,
                    PossiblePoints = question.Points,
                    // TODO передавать null, если есть соответствующая настройка
                    Explanation = question.Explanation,
                    // TODO передавать null, если есть соответствующая настройка
                    RightChoicesIndexesList = choiceQuestion.CorrectAnswersIndexes
                };
                answerViewModels.Add(choiceAnswerViewModel);
            }

            #endregion
        }

        // TODO Организовать запись ответов пользователя в базу (файл в нашем случае)
        var results = new CompletedTestViewModel()
        {
            TestId = inputModel.TestId,
            AnswersList = answerViewModels,
            StartAt = inputModel.StartAt,
            CompletedAt = inputModel.CompletedAt,
            Points = testPoints,
            PossiblePoints = test.QuestionsList.Count, // TODO Поменять когда появится настройка очков за каждый ответ 
            PassScore = null, // TODO Добавить когда появится
            TakerName = inputModel.TakerName
        };
        return results;
    }
}
