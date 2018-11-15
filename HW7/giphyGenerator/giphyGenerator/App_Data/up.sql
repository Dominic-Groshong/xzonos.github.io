CREATE TABLE [dbo].[Logs]
(
  [ID]        INT IDENTITY(0,1) NOT NULL,
  [Date]      DateTime          NOT NULL,
  [Word]      NVARCHAR(100)     NOT NULL,
  [URL]       NVARCHAR(100)     NOT NULL,
  [IP]        VARCHAR(100)      NOT NULL,
  [Browser]   VARCHAR(100)      NOT NULL
  CONSTRAINT [PK_dbo.Records] PRIMARY KEY CLUSTERED([ID] ASC)
);
