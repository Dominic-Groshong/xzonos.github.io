
ALTER TABLE [dbo].Bids  DROP CONSTRAINT  [FK_Bids_Items];
ALTER TABLE [dbo].Bids  DROP CONSTRAINT  [FK_Bids_Buyers];
ALTER TABLE [dbo].Items DROP CONSTRAINT [FK_Items_Seller];

DROP TABLE [dbo].Buyers;
DROP TABLE [dbo].Sellers;
DROP TABLE [dbo].Items;
DROP TABLE [dbo].Bids;

