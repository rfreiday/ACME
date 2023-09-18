CREATE TABLE [dbo].[OrderItem] (
    [Id]                 INT          IDENTITY (1, 1) NOT NULL,
    [OrderId]            INT          NOT NULL,
    [ProductId]          INT          NOT NULL,
    [ProductDescription] VARCHAR (50) NOT NULL,
    [Price]              MONEY        NOT NULL,
    [Quantity]           INT          NOT NULL,
    [Total]              MONEY        NOT NULL,
    CONSTRAINT [PK_OrderItem] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_OrderItem_Order] FOREIGN KEY ([OrderId]) REFERENCES [dbo].[Order] ([Id]),
    CONSTRAINT [FK_OrderItem_Product] FOREIGN KEY ([ProductId]) REFERENCES [dbo].[Product] ([Id])
);

