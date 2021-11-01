CREATE TABLE [dbo].[Leave]
(
	[EmployeeId] INT NOT NULL, 
    [StartDate] DATE NOT NULL, 
    [EndDate] DATE NOT NULL, 
    [Reason] VARCHAR(200) NOT NULL,
    PRIMARY KEY (EmployeeId, StartDate),
    FOREIGN KEY (EmployeeId) References Employee(EmployeeId)
)
