CREATE TABLE [dbo].[Project]
(
	[ProjectId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [ProjectName] VARCHAR(50) NOT NULL, 
    [ProjectLeaderId] INT NOT NULL, 
    [Description] VARCHAR(100) NOT NULL, 
    [CompanyId] INT NOT NULL,
    FOREIGN KEY (CompanyId) References Company(CompanyId) ,
    FOREIGN KEY (ProjectLeaderId) References Employee(EmployeeId) 
)
