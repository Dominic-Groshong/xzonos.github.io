using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WorldWideImporters.Models.ViewModels
{
  /// <summary>
  /// This will be used to pull only the information we need from the Person model without all the extra stuff.
  /// </summary>
  public class PersonVM
  {
    public int PersonID { get; set; }
    public string FullName { get; set; }
    public string PreferredName { get; set; }
    public string PhoneNumber { get; set; }
    public string FaxNumber { get; set; }
    public string EmailAddress { get; set; }
    public DateTime MemberSince { get; set; }
    public IEnumerable<Customer> Customer { get; set; }
  }
}
