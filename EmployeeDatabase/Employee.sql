﻿
CREATE TABLE [dbo].[Employee]
(
	[EmployeeId] INT NOT NULL IDENTITY, 
    [FirstName] VARCHAR(50) NOT NULL, 
    [LastName] VARCHAR(50) NOT NULL, 
    [EmailAddress] VARCHAR(50) NOT NULL UNIQUE, 
    [PhoneNo] VARCHAR(15) NOT NULL UNIQUE, 
    [DateOfBirth] DATE NOT NULL, 
    [Salary] BIGINT NOT NULL, 
    [Password] VARCHAR(50) NOT NULL, 
    [LeavesAvailable] INT NOT NULL, 
    [Credits] INT NOT NULL, 
    [CompanyName] VARCHAR(50) NOT NULL,
    [Department] VARCHAR(50) NOT NULL, 
    PRIMARY KEY (EmployeeId),
    FOREIGN KEY (CompanyName) References Company(CompanyName)
)
