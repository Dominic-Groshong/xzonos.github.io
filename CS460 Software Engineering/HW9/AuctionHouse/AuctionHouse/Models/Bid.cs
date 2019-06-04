namespace AuctionHouse.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Bid
    {
        public int BidID { get; set; }

        public decimal Price { get; set; }

        [Display(Name = "Item Name")]
        public int FKItemID { get; set; }


        [Display(Name = "Buyer Name")]
        public int FKBuyerID { get; set; }

        public virtual Buyer Buyer { get; set; }

        public virtual Item Item { get; set; }

        private DateTime date = DateTime.Now;
        [Display(Name = "Time Requested")]
        public DateTime TimePlaced
    {
          get { return date; }
          set { date = value; }
        }

  }
}
