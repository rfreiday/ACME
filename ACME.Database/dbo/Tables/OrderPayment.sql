CREATE TABLE [dbo].[OrderPayment] (
    [Id]          INT          IDENTITY (1, 1) NOT NULL,
    [OrderId]     INT          NOT NULL,
    [PaymentType] VARCHAR (50) NOT NULL,
    [Amount]      MONEY        NOT NULL,
    CONSTRAINT [PK_OrderPayment] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_OrderPayment_Order] FOREIGN KEY ([OrderId]) REFERENCES [dbo].[Order] ([Id])
);

