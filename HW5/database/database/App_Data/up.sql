CREATE TABLE [dbo].[AssistanceRequests]
(
	[ID]		      INT IDENTITY (1,1)	NOT NULL,
	[FirstName]	  NVARCHAR(50)		NOT NULL,
	[LastName]	  NVARCHAR(50)	 	NOT NULL,
  [Phone]       NVARCHAR(10)     NOT NULL,
  [Building]    NVARCHAR(20)     NOT NULL,
  [Suite]       INT              NOT NULL,
  [Comments]  NVARCHAR(500) NOT NULL,
  [Access]      BIT            NOT NULL,
	[RequestAt]		DateTime			NOT NULL,
	CONSTRAINT [PK_dbo.AssistanceRequests] PRIMARY KEY CLUSTERED ([ID] ASC)
);

INSERT INTO [dbo].[AssistanceRequests] (FirstName, LastName, Phone, Building, Suite, Comments, Access, RequestAt) VALUES
	('Link','He-Ro','5554443333', 'Kokiri Gardens',101, 'All the pots in my kitchen are broken.', 1, '2018-08-22 11:00:00'),
  ('Impa','Gardus','6554253443', 'Hyrule Hills',211, 'My cat got into the ducting and ripped out a bunch of insulation', 1, '2018-09-11 12:00:00'),
  ('Zelda','Perinsess','5554443333', 'Hyrule Hills',312, 'I dropped my favorate instrament in the toliet and its stuck in the drain', 1, '2018-10-01 16:00:00'),
  ('Gannon','Daklord','5554443333', 'Gerudo Highrise',108, 'Need some carpet stains removed', 1, '2018-10-15 14:00:00'),
  ('Navi','Fari','5554443333', 'Kokiri Gardens',312, 'HEY. LOOK. LISTEN. ITS BEEN TWO WEEKS WITHOUT WATER GET UP HERE NOW!!!', 1, '2018-10-26 00:00:00')

GO      
