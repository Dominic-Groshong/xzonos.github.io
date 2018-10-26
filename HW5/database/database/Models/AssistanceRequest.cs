using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace database.Models
{

  public class AssistanceRequest
  {
    [Key]
    public int ID { get; set; }

    [Display(Name = "First Name")]
    [Required]
    public string FirstName { get; set; }

    [Display(Name = "Last Name")]
    [Required]
    public string LastName { get; set; }

    [Phone]
    [Required]
    public string Phone { get; set; }

    [Required]
    public string Building { get; set; }

    [Required]
    public int Suite { get; set; }

    [Required]
    public string Comments { get; set; }

    [Display(Name = "Select here to give permission for the landlord or representitive to enter your suite to preform the requested maintenance. We wil call first.")]
    public bool Access { get; set; }

    private DateTime date = DateTime.Now;
    [Display(Name = "Time Requested")]
    public DateTime RequestAt
    {
      get { return date; }
      set { date = value; }
    }
  }
}
