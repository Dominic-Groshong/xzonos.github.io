using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace database.Models
{

  public class AssistanceRequest
  {
    public string Firstname { get; set; }
    public string LastName { get; set; }
    [Phone]
    public string Phone { get; set; }
    public string Building { get; set; }
    public int Suite { get; set; }
    public string Message { get; set; }
    public bool Access { get; set; }
  }
}
