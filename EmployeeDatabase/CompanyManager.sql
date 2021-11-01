CREATE TABLE [dbo].[CompanyManager]
(
	[EmployeeId] INT NOT NULL, 
    [CompanyId] INT NOT NULL,
	PRIMARY KEY ( EmployeeId,CompanyId),
	FOREIGN KEY (EmployeeId) References Employee(EmployeeId),
	FOREIGN KEY (CompanyId) References Company(CompanyId)
)
