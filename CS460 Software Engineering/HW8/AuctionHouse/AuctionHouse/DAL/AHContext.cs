namespace AuctionHouse.Models
{
  using System;
  using System.Data.Entity;
  using System.ComponentModel.DataAnnotations.Schema;
  using System.Linq;

  public partial class AHContext : DbContext
  {
    public AHContext()
        : base("name=AuctionHouse")
    {
    }

    public virtual DbSet<Bid> Bids { get; set; }
    public virtual DbSet<Buyer> Buyers { get; set; }
    public virtual DbSet<Item> Items { get; set; }
    public virtual DbSet<Seller> Sellers { get; set; }

    protected override void OnModelCreating(DbModelBuilder modelBuilder)
    {
      modelBuilder.Entity<Bid>()
          .Property(e => e.Price)
          .HasPrecision(18, 0);

      modelBuilder.Entity<Buyer>()
          .HasMany(e => e.Bids)
          .WithRequired(e => e.Buyer)
          .HasForeignKey(e => e.FKBuyerID)
          .WillCascadeOnDelete(false);

      modelBuilder.Entity<Item>()
          .Property(e => e.ItemName)
          .IsUnicode(false);

      modelBuilder.Entity<Item>()
          .Property(e => e.Discription)
          .IsUnicode(false);

      modelBuilder.Entity<Item>()
          .HasMany(e => e.Bids)
          .WithRequired(e => e.Item)
          .HasForeignKey(e => e.FKItemID)
          .WillCascadeOnDelete(false);

      modelBuilder.Entity<Seller>()
          .HasMany(e => e.Items)
          .WithRequired(e => e.Seller)
          .HasForeignKey(e => e.FKSellerID)
          .WillCascadeOnDelete(false);
    }
  }
}
