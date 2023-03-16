CREATE TABLE [dbo].[Users] (
    [Id]        INT        IDENTITY (1, 1) NOT NULL,
    [UserName]  NCHAR (16) NOT NULL,
	[Password]  NCHAR (16) NOT NULL,
    [FirstName] NCHAR (50) NOT NULL,
    [LastName]  NCHAR (50) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

