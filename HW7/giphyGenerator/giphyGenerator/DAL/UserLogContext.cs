using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using giphyGenerator.Models;

namespace giphyGenerator.DAL
{
  public class UserLogContext : DbContext
  {
    public UserLogContext() : base("name=UserLog")
    {

    }

    public virtual DbSet<Log> Logs { get; set; }
  }
}



