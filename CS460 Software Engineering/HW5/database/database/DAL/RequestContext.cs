using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using database.Models;

namespace database.DAL
{
  public class RequestContext : DbContext
  {
    public RequestContext() : base("name=AprtRequest")
    {

    }

    public virtual DbSet<AssistanceRequest> AssistanceRequests { get; set; }

  }
}
