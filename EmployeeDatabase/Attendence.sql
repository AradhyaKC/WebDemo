CREATE TABLE [dbo].[Attendence]
(
	[EmployeeId] INT NOT NULL, 
    [Date] DATE NOT NULL, 
    [CheckInTime] DATETIME NULL, 
    [CheckOutTime] DATETIME NULL
    PRIMARY KEY (EmployeeId, Date),
    FOREIGN KEY (EmployeeId) References Employee(EmployeeId) 
)
