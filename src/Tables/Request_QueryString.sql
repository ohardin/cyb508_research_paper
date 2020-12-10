CREATE TABLE [dbo].[Request_QueryString] (
    [id]         INT           IDENTITY (1, 1) NOT NULL,
    [request_id] INT           NULL,
    [key]        VARCHAR (MAX) NULL,
    [value]      VARCHAR (MAX) NULL,
    CONSTRAINT [FK_Request_QueryString_Request] FOREIGN KEY ([request_id]) REFERENCES [dbo].[Request] ([id])
);

