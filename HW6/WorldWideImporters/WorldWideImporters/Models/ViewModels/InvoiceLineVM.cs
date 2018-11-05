using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WorldWideImporters.Models.ViewModels
{
  public class InvoiceLineVM
  {
    public int StockID { get; set; }
    public string Description { get; set; }
    public decimal Profit { get; set; }
    public string SalesPerson { get; set; }
  }
}
