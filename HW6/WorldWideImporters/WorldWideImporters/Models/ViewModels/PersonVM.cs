using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WorldWideImporters.Models.viewModels
{
  /// <summary>
  /// This will be used to pull only the information we need from the Person model without all the extra stuff.
  /// </summary>
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
