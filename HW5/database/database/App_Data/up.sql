CREATE TABLE [dbo].[AssistanceRequests]
(
	[ID]		      INT IDENTITY (1,1)	NOT NULL,
	[FirstName]	  NVARCHAR(50)		NOT NULL,
	[LastName]	  NVARCHAR(50)	 	NOT NULL,
  [Phone]       NVARCHAR(10)     NOT NULL,
  [Building]    NVARCHAR(15)     NOT NULL,
  [Suite]       INT              NOT NULL,
  [Comments]  NVARCHAR(500) NOT NULL,
  [Access]      BIT            NOT NULL,
	[RequestAt]		DateTime			NOT NULL,
	CONSTRAINT [PK_dbo.AssistanceRequests] PRIMARY KEY CLUSTERED ([ID] ASC)
);

INSERT INTO [dbo].[AssistanceRequests] (FirstName, LastName, Phone, Building, Suite, Comments, Access, RequestAt) VALUES
	('Link','He-Ro','5554443333', 'Kokiri Gardens',1, 'All the pots in my kitchen are broken.', 1, '2000-08-22 00:00:00'),
  ('Impa','Gardus','6554253443', 'Hyrule Hills',7, 'My cat got into the ducting and ripped out a bunch of insulation', 1, '2000-08-22 00:00:00'),
  ('Zelda','Perinsess','5554443333', 'Kokiri Gardens',3, 'I dropped my favorate instrament in the toliet and its stuck in the drain', 1, '2000-08-22 00:00:00'),
  ('Gannon','Daklord','5554443333', 'Gerudo Highrise',8, 'Need some carpet stains removed', 1, '2000-08-22 00:00:00')

GO      
