# Квиз менеджер (серверная часть)
### [quiz_app_test_task](https://github.com/SimpSlogger/quiz_app_test_task)

Серверная часть приложения для добавления и прохождения простых опросов. 
Нужна для выполнения тестового задания на позицию junior frontend.

---
## Оглавление:
- [Начало работы](#начало-работы)
  - [Описание проекта](#описание-проекта)
  - [Зависимости проекта](#зависимости-проекта)
  - [Запуск](#запуск)
  - [Описание API](#описание-api)
- [Тестовое задание](#тестовое-задание)
  - [Задача](#задача)
  - [Требуемый функционал](#требуемый-функционал)
  - [Требуемые технологии](#требуемые-технологии)
    - [Общее](#общее)
    - [Связь с сервером](#связь-с-сервером)
---

## Начало работы
### Описание проекта
Пользователь может отредактировать шаблон `TestTableTemplate_v0.xlsx` соответствующими инструментами 
(Excel, OpenOffice...), добавив свои вопросы, варианты ответов и указав какие варианты
являются верными.

Отредактированный шаблон может быть загружен в систему. Опрос по
этому шаблону теперь доступен для прохождения.

Можно запросить список всех загруженных ранее опросов, запросить список вопросов
с вариантами ответов для каждого теста, отправить ответы пользователя обратно на сервер и получить 
результат тестирования.

### Зависимости проекта
Проект реализован на .NET Core.
Поэтому для запуска потребуется установить следующие зависимости:
- [NET 6 SDK](https://dotnet.microsoft.com/ru-ru/download/dotnet/6.0)

### Запуск
Запустите проект `TestManager.WebApi` в режиме Development, чтобы получить доступ 
к Swagger, с помощью которого можно посмотреть и протестировать API.

Проще всего это сделать в среде разработки.

По умолчанию Swagger доступен на: https://localhost:7115/swagger/index.html

### Описание API
Доступные методы будут отображены на странице swagger. На данный момент они такие:
- CheckAnswers
  - `[Post]` `/api/v1/CheckAnswers/check/test`
    - Принимает ФИО опрашиваемого пользователя, время начала и окончания тестирования и
    список ответов пользователя.
    - Возвращает кол-во баллов и список с правильными ответами.
- TableFile
  - `[Post]` `/api/v1/TableFile/import/tableFile`
    - Принимает файл с составленным тестом.
  - `[Get]` `/api/v1/TableFile/export/result`
    - Принимает результаты тестирования, преобразует в `.xlsx` файл.
    - Возвращает файл с результатами тестирования.
- Tests
  - `[Get]` `/api/v1/Tests`
    - Возвращает список доступных тестов.
  - `[Get]` `/api/v1/Tests/{id}`
    - Возвращает общую информацию и список вопросов для конкретного теста по идентификатору.
  
Более подробно входные и выходные модели можно рассмотреть в методах в папке `Controllers` проекта `TestsManager.WebApi`
или после генерации клиента с помощью NSwagStudio (об этом ниже).

---

## Тестовое задание
### Задача
Реализовать frontend часть проекта Квиз менеджера.

### Требуемый функционал
- Возможность скачать шаблон опроса `TestTableTemplate_v0.xlsx`.
- Возможность загрузить отредактированный шаблон.
  - После успешной загрузки общий список тестов должен пополниться без необходимости перезагрузить страницу.
- Вывод на экран списка всех доступных тестов.
- Возможность выбрать один из тестов для прохождения.
  - Вывод названия выбранного теста, времени начала прохождения и ФИО опрашиваемого (вводится пользователем перед началом прохождения теста).
  - Вывод списка вопросов выбранного теста.
    - Если вопрос может иметь несколько правильных ответов - варианты ответов выводить в виде checkbox.
    - Если вопрос имеет только один правильный ответ - варианты ответов выводить в виде radio button.
  - Дать возможность завершить тестирование.
    - Если пользователь ответил не на все вопросы - не завершать тестирование. 
    - Обработать данные ответы на сервере и вывести на экран результаты.
    - Дать возможность скачать файл с результатами тестирования.

### Требуемые технологии
#### Общее
- [react](https://react.dev/learn) + [typescript](https://www.typescriptlang.org/docs/) как основные технологии;
- [MUI](https://mui.com/material-ui/getting-started/) в качестве библиотеки элементов;
- [react-router-dom](https://reactrouter.com/) для переключения между страницами (например страницы со списком тестов и страница с вопросами по конкретному тесту)
#### Связь с сервером
- [NswagStudio](https://github.com/RicoSuter/NSwag/releases/tag/v14.1.0) для генерации клиента на typescript (берем 14.1.0, т.к. в последующих нет поддержки .NET 6);
  Для генерации используем конфигу `TestsManager.nswag`. Складываем в отдельный `.ts` файл, например назовем его `GeneratedClient.ts`.
- [axios](https://axios-http.com/ru/docs/intro) для создания axios instance, необходимой для создания экземпляров клиентов из `GeneratedClient.ts`;
  ```typescript
  // Пример
  
  // Создаем экземпляр
  export const axiosInstance: AxiosInstance = axios.create()
  
  // ICheckAnswersClient и CheckAnswersClient будут находиться в сгенерированном клиенте
  export const CheckAnswersClientApi: ICheckAnswersClient
    = new CheckAnswersClient("<адрес сервера>", axiosInstance)
  ```
- [react-query](https://tanstack.com/query/v5/docs/framework/react/overview) для упрощения работы с асинхронными данными, их обновления и управления состоянием;
  ```typescript
  // Пример
  
  // useMutation - хук из библиотеки ReactQuery
  // CompletedTestInputModel и прочие модели будут находиться в сгенерированном клиенте
  const checkTestMutation = useMutation((inputModel: CompletedTestInputModel) =>
        // CheckAnswersClientApi был создан в предыдущем пункте
        CheckAnswersClientApi.test(inputModel), {
        onSuccess: data => {
            // Обработка успешного запроса
        },
        onError: error => {
            // Обработка ошибки
        }
    })
  ```
