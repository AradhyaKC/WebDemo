CREATE TABLE [dbo].[Employee]
(
	[Id] INT NOT NULL PRIMARY KEY unique,
	[EmployeeID] INT not null ,
	[FirstName] VARCHAR(50)  not null ,
	[LastName] varchar(50) not null,
	[EmailAddress] varchar(100) not null
)
