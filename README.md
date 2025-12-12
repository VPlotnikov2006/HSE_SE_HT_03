# ДЗ №3 КПО Синхронное межсервисное взаимодействие

Автор: Плотников Владимир Денисович

Группа: БПИ 246

---

Структура решения:
- `API.Gateway`
  - `Gateway.Application` - UseCase-ы, интерфейсы для подключения к микросервисам, DTO-шки
  - `Gateway.Infrastructure` - реализация интерфейсов клиентов, расширение для `ServiceCollection`
  - `Gateway.API` - слой взаимодействия с пользователем: контроллеры и `Main`
- `FileAnalysisService`
  - `Domain` - описание базовых сущностей (отчет о плагиате и совпадение (Match)), интерфейс алгоритма определения плагиата
  - `Application` - UseCase-ы, интерфейсы для `WordCloudApi` и `FileStorageClient`, интерфейс репозитория отчетов о плагиате, DTO для маппинга с `FileStorage`
  - `Infrastructure` - реализация клиентов, EFCore, алгоритм определения плагиата на основе расстояния Левенштейна
  - `API` - api
- `FileStorageService`
  - `Domain` - базовые сущности (метаданные файлов)
  - `Application` - Интерфейсы репозитория и класса хранения/загрузки файла, UseCase-ы, утилита для подсчета контрольной суммы файла
  - `Infrastructure` - Реализация `FileProvider` (локальное хранение), EFCore
  - `API` - api

---
`appsettings.json` - хз на сколько они актуальные, но наверное если запускать без `Docker` могут сработать

---
## Endpoint-ы

Все endpoint-ы можно посмотреть в /swagger/all/ui у сервиса API.Gateway.

Локально у каждого сервиса можно смотреть в /swagger

1. Gateway
   - POST /api/gateway/files - загрузка файла
   - GET /api/gateway/reports/{workId} - получение отчетов по всем файлам входящим в КР
   - GET /api/gateway/wordcloud/{fileId} - получение картинки облака слов файла
   - GET /swagger/all - для получения OpenAPI-схем сервисов
   - GET /swagger/all/remote/{service}/swagger.json - для получения схемы конкретного сервиса
2. FileAnalysis
   - POST /internal/analysis - анализ нового файла на плагиат
   - GET /api/reports/by-work/{workId} - получение отчетов по работе
   - GET /api/wordcloud/{fileId} - построение облака слов файла
   - GET /internal/swagger/v1/swagger.json - попытка борьбы с CORS-ами для составления общего swagger-а
3. FileStorage
   - POST /api/files - загрузка файла
   - GET /internal/files/by_work/{workId} - получение Id всех файлов, относящихся к КР
   - GET /internal/files/{fileId} - загрузка файла
   - GET /internal/swagger/v1/swagger.json - попытка борьбы с CORS-ами для составления общего swagger-а
---

## Взаимодействие микросервисов
1. Загрузка файла пользователем:
   1. Gateway (POST /api/gateway/files) -> FileStorage (POST /api/files)
   2. FileStorage (POST /api/files) -> сохраняет файл -> Response Gateway
   3. Gateway -> FileAnalysis (POST /internal/analysis)
   4. FileAnalysis (POST /internal/analysis) -> запускает анализ -> FileStorage (GET /internal/files/by-work/{workId})
   5. FileStorage (GET /internal/files/by-work/{workId}) -> Собирает Id всех файлов, относящихся к данной работе -> Response FileAnalysis
   6. FileAnalysis - загружает новый файл, загружает все остальные файлы по КР, анализирует
2. Получение отчетов по работе:
   1. Gateway (GET /api/gateway/reports/{workId}) -> FileAnalysis (GET /api/reports/by-work/{workId})
3. Построение облака слов:
   1. Gateway (GET /api/gateway/wordcloud/{fileId}) -> FileAnalysis (GET /api/wordcloud/{fileId})
   2. FileAnalysis (GET /api/wordcloud/{fileId}) -> quickchart.io/wordcloud

---

## Хранение файлов

Файлы хранятся локально (/app/data внутри Docker, volume наружу в ./filestorage-data)

Для упрощения навигации файлы распределяются по году, месяцу и дате загрузки

Формат названия файла {fileId}_{originalName}.{originalExt}

---

## Определение плагиата

1. Файл присланный раньше считается оригинальным, соответственно у него меток о плагиате никогда не появится
2. score - метрика похожести файлов (определяется при помощи расстояния Левенштейна, описание алгоритма: FileAnalysisService\FileAnalysis.Infrastructure\SimilarityAlgorithms\LevenshteinDistanceAlgorithm.cs )
3. Степень `p` берется равной 2 (можно поменять в FileAnalysisService\FileAnalysis.API\appsettings.json)
4. Файлы со score > 0.5 считаются плагиатом 
5. В отчете о плагиате хранится список всех файлов, с которыми обнаружен плагиат (в БД хранение через связь 1:N)
6. "Флаг плагиата" из условия заменен максимальным значением score, которое набрала работа

---

## Docker

Файлы `Dockerfile` и `docker-compose.yaml`

Файл `postgres\init-dbs.sql` - настройка БД для создания нужных баз (запускается один образ внутри которого две базы)

`postgres-data` - папочка с volume для postgres 

1. Gateway - на порте 5000 (http://localhost:5000/swagger/index.html)
2. FileStorage - на порте 5001 (http://localhost:5001/swagger/index.html)
3. FileAnalysis - на порте 5002 (http://localhost:5001/swagger/index.html)

У меня возникла проблема, что http://localhost:5000/swagger/all/ui не хотел нормально работать в обычном гугле. В режиме инкогнито все работало нормально (скорее всего он что-то не так закешировал, но очистка локальных данных не помогла)
