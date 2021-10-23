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
