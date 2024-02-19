### Картотека водителей ###
Тестовое задание.

Приложение реализует картотеку водителей в соответствии с условиями, указанными в файле Задача.docx в корне проекта.

Приложение состоит из следующих частей:
1. Проект ASP.NET MCV (папка Drivers) - принимает данные пользователя и посылает их на REST API.
2. Проект REST WebAPI (папка DriversServices) - принимает данные пользователя, передает их в репозитоий.
3. Проект Repository на Entity Framework 6 - принимает, созраняет и возвращает данные от REST.

В ASP.NET реализована валидция данных пользователя. Repository использует SQL Server Express LocalDB (установлена вместе с Visual Studio). При каждом запуске база данных очищается и заполняется начальными значениями.

### Drivers database ###
Test task.

The app implements drivers database according to conditions stated in Задача.docx which is located in the root of the repo.

App consists of the following parts:
1. ASP.NET MVC project (Drivers folder) - accepts user data and sends it to REST API.
2. REST WebAPI project (DriversServices) - accepts user data and sends them to Repository.
3. Repository project based on Entity Framework 6 - accepts, stores and sends data to REST.

ASP.NET features validation of user data. Repository uses SQL Server Express LocalDB which is installed with Visual Studio. Each time the app starts the database gets purged and seeded with initial values.
