CREATE TABLE [dbo].[CompanyManager]
(
	[EmployeeId] INT NOT NULL, 
    [CompanyName] VARCHAR(50) NOT NULL,
	PRIMARY KEY ( EmployeeId),
	FOREIGN KEY (EmployeeId) References Employee(EmployeeId),
	FOREIGN KEY (CompanyName) References Company(CompanyName)
)
