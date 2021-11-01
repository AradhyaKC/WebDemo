CREATE TABLE [dbo].[WorksOn]
(
	[EmployeeId] INT NOT NULL, 
    [ProjectId] INT NOT NULL, 
    [Role] VARCHAR(50) NOT NULL, 
    [ShiftStartTime] TIME NOT NULL, 
    [ShiftEndTime] TIME NOT NULL,
    PRIMARY KEY (EmployeeId, ProjectId),
    FOREIGN KEY (EmployeeId) References Employee(EmployeeId) ON UPDATE CASCADE ON DELETE CASCADE,
    FOREIGN KEY (ProjectId) References Project(ProjectId) ON UPDATE CASCADE ON DELETE CASCADE
)
