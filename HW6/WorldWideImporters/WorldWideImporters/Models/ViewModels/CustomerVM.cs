using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WorldWideImporters.Models.ViewModels
{
  public class CustomerVM
  {
    public int CustomerID { get; set; }
    public string CustomerName { get; set; }
    public string PhoneNumber { get; set; }
    public string FaxNumber { get; set; }
    public string WebsiteURL { get; set; }
    public DateTime MemberSince { get; set; }
  }
}
