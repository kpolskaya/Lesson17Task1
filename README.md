# Lesson17Task1
Перед первым запуском необходимо создать БД CustomersDB и преобразовать ее в *contained database*.
На сервере должен быть разрешен режим *contained database authentification*. Пример скрипта, который
создает БД и пользователя __user1__, пароль __1234__ (есть в папке проекта DB\SQL\SQLQuery1.sql):
```TSQL 
sp_configure 'show advanced options', 1
GO
RECONFIGURE
GO
sp_configure 'contained database authentication', 1
GO
RECONFIGURE
GO
sp_configure 'show advanced options', 0 
GO
RECONFIGURE
GO

USE MASTER
GO

CREATE DATABASE CustomersDB
GO

USE MASTER
GO

ALTER DATABASE CustomersDB
SET containment=partial;

USE CustomersDB
GO

CREATE USER [user1]
WITH PASSWORD='1234',
DEFAULT_SCHEMA=[dbo]
GO

SELECT containment_desc FROM sys.databases
WHERE name='CustomersDB'
```
БД в формате Access находится в папке DB\OLE\ 
логин: __user1__ пароль: __1234__
