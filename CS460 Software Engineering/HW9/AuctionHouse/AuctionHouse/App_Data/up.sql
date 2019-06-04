CREATE TABLE [dbo].[Buyers]
(
	[BidderID]      INT IDENTITY(0,1)      NOT NULL, 
  [FullName]      NVARCHAR(50)           NOT NULL,

  CONSTRAINT [PK_BidderID] PRIMARY KEY ( [BidderID] )
)

CREATE TABLE [dbo].[Sellers]
(
	[SellerID]      INT IDENTITY(0,1)      NOT NULL, 
  [FullName]      NVARCHAR(50)           NOT NULL,

  CONSTRAINT [PK_SellerID] PRIMARY KEY ( [SellerID] )
)

CREATE TABLE [dbo].[Items] (
    [ItemID]          INT IDENTITY(1001,1)      NOT NULL,
    [ItemName]        VARCHAR(100)              NOT NULL,
    [Discription]     VARCHAR(500)              NOT NULL,
    [FKSellerID]      INT                       NOT NULL,

    CONSTRAINT [PK_ItemID] PRIMARY KEY ( [ItemID] ),
    CONSTRAINT [FK_Items_Seller]  FOREIGN KEY ([FKSellerID]) REFERENCES [Sellers]([SellerID]) 
)

CREATE TABLE [dbo].[Bids] (
    [BidID]           INT IDENTITY(0,1)       NOT NULL,
    [Price]           DECIMAL                 NOT NULL,
    [TimePlaced]      DateTime                NOT NULL,
    [FKItemID]          INT                   NOT NULL,
    [FKBuyerID]         INT                   NOT NULL,

    CONSTRAINT [PK_BidID] PRIMARY KEY ( [BidID] ),
    CONSTRAINT [FK_Bids_Items]    FOREIGN KEY ([FKItemID]) REFERENCES [Items]([ItemID]), 
    CONSTRAINT [FK_Bids_Buyers]   FOREIGN KEY ([FKBuyerID]) REFERENCES [Buyers]([BidderID]) 
);

INSERT INTO [dbo].[Buyers] (FullName)
VALUES  ('Jane Stone'),
        ('Tom McMasters'),
        ('Otto Vanderwall');

INSERT INTO [dbo].[Sellers] (FullName)
VALUES  ('Gayle Hardy'),
        ('Lyle Banks'),
        ('Pearl Greene');

INSERT INTO [dbo].[Items] (ItemName, Discription, FKSellerID)
VALUES  ('Abraham Lincoln Hammer', 'A bench mallet fashioned from a broken rail-splitting maul in 1829 and owned by Abraham Lincoln', 2),
        ('Albert Einsteins Telescope', 'A brass telescope owned by Albert Einstein in Germany, circa 1927', 0),
        ('Bob Dylan Love Poems', 'Five versions of an original unpublished, handwritten, love poem by Bob Dylan', 1);

INSERT INTO [dbo].[Bids] (FKItemID, Price, TimePlaced, FKBuyerID)
VALUES  (1001,250000,'12/04/2017 09:04:22', 2),
        (1003,95000 ,'12/04/2017 08:44:03', 0);

GO
