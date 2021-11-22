CREATE TABLE [dbo].[Company]
( 
    [CompanyName] VARCHAR(50) NOT NULL PRIMARY KEY, 
    [Motto] VARCHAR(50) NOT NULL, 
    [StartDate] DATE NOT NULL, 
    [Address] VARCHAR(200) NOT NULL, 
    [PhoneNo] VARCHAR(15) NOT NULL, 
    [EmailAddress] VARCHAR(50) NOT NULL 
)
