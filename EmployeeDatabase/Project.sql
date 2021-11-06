CREATE TABLE [dbo].[Project]
(
	[ProjectId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [ProjectName] VARCHAR(50) NOT NULL, 
    [ProjectLeaderId] INT NOT NULL, 
    [Description] VARCHAR(100) NOT NULL, 
    [CompanyName] VARCHAR(50) NOT NULL,
    FOREIGN KEY (CompanyName) References Company(CompanyName) ,
    FOREIGN KEY (ProjectLeaderId) References Employee(EmployeeId) 
)
