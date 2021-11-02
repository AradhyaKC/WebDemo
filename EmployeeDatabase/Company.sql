CREATE TABLE [dbo].[Company]
(
	[CompanyId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [CompanyName] VARCHAR(50) NOT NULL, 
    [Motto] VARCHAR(50) NOT NULL, 
    [Startdate] DATE NOT NULL 
)
