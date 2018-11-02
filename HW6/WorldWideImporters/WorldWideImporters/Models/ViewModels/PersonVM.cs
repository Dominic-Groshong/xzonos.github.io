using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WorldWideImporters.Models.viewModels
{
  public class PersonVM
  {
    public string FullName { get; set; }
    public string PreferredName { get; set; }
    public string PhoneNumber { get; set; }
    public string FaxNumber { get; set; }
    public string EmailAddress { get; set; }
    public DateTime ValidFrom { get; set; }
    public byte[] Photo { get; set; }
  }
}
