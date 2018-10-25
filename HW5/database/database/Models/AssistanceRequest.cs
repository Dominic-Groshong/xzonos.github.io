using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace database.Models
{

  public class AssistanceRequest
  {
    [Display(Name = "First Name")]
    public string FirstName { get; set; }

    [Display(Name = "Last Name")]
    public string LastName { get; set; }

    [Phone]
    public string Phone { get; set; }

    public string Building { get; set; }

    public int Suite { get; set; }

    public string Message { get; set; }

    [Display(Name = "Select here to give permission for the landlord or representitive to enter your suite to preform the requested maintenance. We wil call first.")]
    public bool Access { get; set; }
  }
}
