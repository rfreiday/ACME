CREATE TABLE [dbo].[Customer] (
    [Id]         INT           IDENTITY (1, 1) NOT NULL,
    [Name]       VARCHAR (50)  NOT NULL,
    [Email]      VARCHAR (250) NOT NULL,
    [Phone1]     VARCHAR (20)  NULL,
    [Phone2]     VARCHAR (20)  NULL,
    [Address1]   VARCHAR (50)  NULL,
    [Address2]   VARCHAR (50)  NULL,
    [City]       VARCHAR (25)  NULL,
    [State]      VARCHAR (2)   NULL,
    [PostalCode] VARCHAR (10)  NULL,
    [Comments]   TEXT          NULL,
    CONSTRAINT [PK_Customer] PRIMARY KEY CLUSTERED ([Id] ASC)
);

