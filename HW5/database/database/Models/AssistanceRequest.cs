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
    [Required(ErrorMessage = "Please enter your first name")]
    [MaxLength(50)]
    public string FirstName { get; set; }

    [Display(Name = "Last Name")]
    [Required(ErrorMessage = "Please enter your last name")]
    [MaxLength(50)]
    public string LastName { get; set; }

    [Phone]
    [Required(ErrorMessage = "You must provide a phone number")]
    [DataType(DataType.PhoneNumber)]
    [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid phone number")]
    [MaxLength(10)]
    public string Phone { get; set; }

    [Required(ErrorMessage = "Please select the building you live in")]
    [MaxLength(20)]
    public string Building { get; set; }
      
    [Required(ErrorMessage = "Please provide your room number")]
    public int Suite { get; set; }

    [Required(ErrorMessage = "Please state your problem")]
    [MaxLength(500)]
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
