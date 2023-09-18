CREATE TABLE [dbo].[Product] (
    [Id]          INT          IDENTITY (1, 1) NOT NULL,
    [Description] VARCHAR (50) NOT NULL,
    [UnitCost]    MONEY        NOT NULL,
    [Price]       MONEY        NOT NULL,
    [Taxable]     BIT          NOT NULL,
    CONSTRAINT [PK_Product] PRIMARY KEY CLUSTERED ([Id] ASC)
);

