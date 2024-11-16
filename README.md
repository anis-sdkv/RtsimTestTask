# RtsimTestTask

Для запуска контейнера с Postgres: 
```bash
docker-compose up -d
```
Для инициализации бд: 
```bash
cd .\src\RtsimTestTask.Infrastructure.Persistence\
dotnet ef migrations add Init
dotnet ef database update
```
