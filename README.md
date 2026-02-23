  Обеспечение запуска:
  1.Создайте образ базы данных PostgreSQL с помощью Docker:
docker run --name order-management-postgres -e POSTGRES_USER=postgres -e POSTGRES_PASSWORD=passwordNik -e POSTGRES_DB=OrderManagementDb -p 5432:5432 -d postgres:latest
  2. Примените миграции:
dotnet ef database update
  Пользователи:
логин: admin пароль: admin123 id: 1
логин: user1 пароль: user123 id: 2
логин: user2 пароль: user123 id: 3
  Работа с ПО:
  1. Авторизация в Swagger:
Авторизуйтесь через метод Auth, скопируйте JWT-токен
Нажмите кнопку Authorize в правом верхнем углу
Введите ваш JWT-токен
Нажмите Authorize
  2. В блоке Order, в методе GetOrders в сортировку по цене вводится: ничего, price(сортировка по возрастанию), priceDesc(сортировка по убыванию)

