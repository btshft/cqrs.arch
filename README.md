## CQRS Achitecture (w/o ES)

Пример архитектуры приложения на основе подхода разделения моделей чтения и записи (CQRS). Основная концепция выглядит следующим образом
![alt generic approach](https://raw.githubusercontent.com/callvirtual/cqrs.arch/master/resources/Generic.png)

Пользовательский запрос, поступая на контроллер, преобразуется в модель запроса или команды, после чего передается на вход обработчику запросов (dispatcher). 
Обработчик запросов обеспечивает сопоставление запросов и обработчиков и, по сути, реализует шаблон [Медиатор](https://sourcemaking.com/design_patterns/mediator).
Во время обработки запрос проходит через так называемую цепочку обработки (pipeline), которая состоит из самого обработчика и внешних декораторов. 
Декораторы позволяют инкапсулировать и переиспользовать инфраструктурный код (логирование, профилирование, авторизацию и т.д.). 

В общем случае существует три типа сообщений: команда, запрос и событие. 

**Команды** (Command) обеспечивают реализацию действий, приводящих к изменению состояния
системы (аналогично можно рассматривать запросы типа POST/DELETE/PATCH в спецификации HTTP). Команды также формируют модель записи системы. (write model)
![alt generic approach](https://raw.githubusercontent.com/callvirtual/cqrs.arch/master/resources/Write.png)

Команды представляют собой единицу действия и **не должны** порождать другие команды. В случае необходимости переиспользования конкретных бизнес-сценариев, их необходимо инкапсулировать внутри сервисов приложения (AppService) и вызывать сервисы внутри обработчиков команд.

Помимо изменения состояния системы, команды также могут создавать события. **События** (Event) позволяют уведомлять об изменении состояния системы (в частностни конкретного агрегата)
соответствующие обработчик. Обработка событий происходит после выполнения команды. Это позволяет избежать влияния ошибок обработки событий на общий результат выполнения команды.

Помимо команд и событий существуют запросы. **Запрос** (Query) - частный вид сообщения, предназначенные для получения данных. На основе запросов формируется
модель чтения (read model).
![alt generic approach](https://raw.githubusercontent.com/callvirtual/cqrs.arch/master/resources/Read.png)

Результатом выполнения запроса является некоторая проекция данных, сформированная на основе данных в бд. Запросы **не должны** инициировать события и команды.
Аналогом запроса в подходе CQRS является запрос с заголовком GET в спецификации HTTP.

## Особенности проекта
* Фреймворк: ASP.NET Core 2.2
* Библиотеки: ASP.NET Core WebAPI, [EntityFrameworkCore](https://github.com/aspnet/EntityFrameworkCore) (InMemory Provider), [MediatR](https://github.com/jbogard/MediatR), [Automapper](https://github.com/AutoMapper/AutoMapper), [Swagger](https://github.com/domaindrivendev/Swashbuckle).

## Ссылки
1. https://docs.microsoft.com/ru-ru/dotnet/standard/microservices-architecture/microservice-ddd-cqrs-patterns/microservice-application-layer-implementation-web-api#implement-the-command-process-pipeline-with-a-mediator-pattern-mediatr
2. https://vladikk.com/2017/03/20/tackling-complexity-in-cqrs/
3. https://martinfowler.com/bliki/CQRS.html
4. https://sourcemaking.com/design_patterns/decorator
5. https://sourcemaking.com/design_patterns/mediator
