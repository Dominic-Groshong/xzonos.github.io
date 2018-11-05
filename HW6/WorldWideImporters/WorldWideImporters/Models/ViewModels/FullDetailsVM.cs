using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WorldWideImporters.Models.ViewModels
{
  /// <summary>
  /// This is the main view model, we store each of the other view models in here and then access them in the controller and on the view page.
  /// </summary>
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
