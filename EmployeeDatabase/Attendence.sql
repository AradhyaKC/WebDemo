CREATE TABLE [dbo].[Attendence]
(
	[EmployeeId] INT NOT NULL, 
    [Date] DATE NOT NULL, 
    [CheckInTime] TIME NOT NULL, 
    [CheckOutTime] TIME NOT NULL
    PRIMARY KEY (EmployeeId, Date),
    FOREIGN KEY (EmployeeId) References Employee(EmployeeId) 
)
