using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WorldWideImporters.Models.ViewModels
{
  public class FullDetailsVM
  {
    public IEnumerable<PersonVM> Person { get; set; }
    public IEnumerable<CustomerVM> Company { get; set; }
    public IEnumerable<InvoiceLineVM> Invoice { get; set; }
    public int TotalOrders { get; set; }
    public decimal GrossSales { get; set; }
    public decimal TotalProfit { get; set; }
   }
}
