CREATE TABLE [dbo].[Attendence]
(
	[EmployeeId] INT NOT NULL, 
    [Date] DATE NOT NULL, 
    [CheckInTime] TIME NULL, 
    [CheckOutTime] TIME NULL
    PRIMARY KEY (EmployeeId, Date),
    FOREIGN KEY (EmployeeId) References Employee(EmployeeId) 
)
