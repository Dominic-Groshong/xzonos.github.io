using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace giphyGenerator.Models
{
  public class Log
  {
    [Key]
    public int ID {get; set;}
    public DateTime Date { get; set; }
    public string Word { get; set; }
    public string URL { get; set; }
    public string IP { get; set; }
    public string Browser { get; set; }
  }
}
