CREATE TABLE [dbo].[Employee] (
    [Id]           INT       identity    NOT NULL,
    [EmployeeID]   INT           NOT NULL,
    [FirstName]    VARCHAR (50)  NOT NULL,
    [LastName]     VARCHAR (50)  NOT NULL,
    [EmailAddress] VARCHAR (100) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    UNIQUE NONCLUSTERED ([Id] ASC)
);

