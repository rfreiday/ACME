CREATE TABLE [dbo].[Order] (
    [Id]         INT      IDENTITY (1, 1) NOT NULL,
    [DateTime]   DATETIME NOT NULL,
    [CustomerId] INT      NOT NULL,
    [Subtotal]   MONEY    NOT NULL,
    [Taxes]      MONEY    NOT NULL,
    [Total]      MONEY    NOT NULL,
    [Shipped]    BIT      NOT NULL,
    CONSTRAINT [PK_Order] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Order_Customer] FOREIGN KEY ([CustomerId]) REFERENCES [dbo].[Customer] ([Id])
);

