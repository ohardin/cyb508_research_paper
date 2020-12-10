CREATE TABLE [dbo].[Request] (
    [id]              INT           IDENTITY (1, 1) NOT NULL,
    [os]              VARCHAR (50)  NULL,
    [webserver]       VARCHAR (50)  NULL,
    [runningLdap]     VARCHAR (50)  NULL,
    [runningSqlDb]    VARCHAR (50)  NULL,
    [runningXpath]    VARCHAR (50)  NULL,
    [class_type]      VARCHAR (50)  NULL,
    [class_incontext] VARCHAR (50)  NULL,
    [method]          VARCHAR (50)  NULL,
    [protocol]        VARCHAR (50)  NULL,
    [uri]             VARCHAR (MAX) NULL,
    [query]           VARCHAR (MAX) NULL,
    [header]          VARCHAR (MAX) NULL,
    CONSTRAINT [PK_Request] PRIMARY KEY CLUSTERED ([id] ASC)
);

